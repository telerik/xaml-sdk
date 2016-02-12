using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.RichTextBoxUI.Dialogs;
using Telerik.Windows.Documents.Layout;
using Telerik.Windows.Documents.Model;
using Telerik.Windows.Documents.Model.Styles;
using Telerik.Windows.Documents.UI.Extensibility;
using Telerik.Windows.Controls.RichTextBoxUI;
using Telerik.Windows.Controls.RichTextBoxUI.ColorPickers;

#if SILVERLIGHT
using SelectionChangedEventArgs = Telerik.Windows.Controls.SelectionChangedEventArgs;
#endif

namespace CustomParagraphPropertiesDialogDemo
{
    /// <summary>
    /// Interaction logic for CustomParagraphPropertiesDialog.xaml
    /// </summary>
    [CustomParagraphPropertiesDialog]
    public partial class CustomParagraphPropertiesDialog : RadRichTextBoxWindow, IParagraphPropertiesDialog
    {
        internal enum LineSpacingDialogTypes
        {
            Single,
            LineAndAHalf,
            Double,
            AtLeast,
            Exactly,
            Multiple
        }

        internal enum FirstLineIndentDialogTypes
        {
            None,
            FirstLine,
            Hanging
        }

        private const double DefaultFirstLineIndent = 48; // 48 in pt = 0.5 in inch = 1.27 in cm

        #region DependencyProperties

        public double NumericWidth
        {
            get { return (double)GetValue(NumericWidthProperty); }
            set { SetValue(NumericWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NumericWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NumericWidthProperty =
            DependencyProperty.Register("NumericWidth", typeof(double), typeof(RadParagraphPropertiesDialog), null);

        #endregion

        #region Fields

        private Action<StyleDefinition> applyCallback;

        private readonly Dictionary<LineSpacingDialogTypes, string> lineSpacingTypes;
        private readonly Dictionary<FirstLineIndentDialogTypes, string> firstLineIndentTypes;

        private ParagraphPropertiesDialogContext context;
        private ParagraphPropertiesDialogInfo initialProperties;
        private string initialLineSpacingDialogType;
        private string oldFirstLineIndentType;
        private bool isIndentationChanged;
        private bool isBackgroundChanged;
        private bool isUpdatingUI;

        #endregion

        #region Constructors

        public CustomParagraphPropertiesDialog()
        {
            InitializeComponent();

            this.lineSpacingTypes = new Dictionary<LineSpacingDialogTypes, string>();
            this.lineSpacingTypes.Add(LineSpacingDialogTypes.Single, LocalizationManager.GetString("Documents_ParagraphPropertiesDialog_LineSpacing_Single"));
            this.lineSpacingTypes.Add(LineSpacingDialogTypes.LineAndAHalf, LocalizationManager.GetString("Documents_ParagraphPropertiesDialog_LineSpacing_LineAndAHalf"));
            this.lineSpacingTypes.Add(LineSpacingDialogTypes.Double, LocalizationManager.GetString("Documents_ParagraphPropertiesDialog_LineSpacing_Double"));
            this.lineSpacingTypes.Add(LineSpacingDialogTypes.AtLeast, LocalizationManager.GetString("Documents_ParagraphPropertiesDialog_LineSpacing_AtLeast"));
            this.lineSpacingTypes.Add(LineSpacingDialogTypes.Exactly, LocalizationManager.GetString("Documents_ParagraphPropertiesDialog_LineSpacing_Exactly"));
            this.lineSpacingTypes.Add(LineSpacingDialogTypes.Multiple, LocalizationManager.GetString("Documents_ParagraphPropertiesDialog_LineSpacing_Multiple"));

            this.firstLineIndentTypes = new Dictionary<FirstLineIndentDialogTypes, string>();
            this.firstLineIndentTypes.Add(FirstLineIndentDialogTypes.None, LocalizationManager.GetString("Documents_ParagraphPropertiesDialog_FirstLineIndentDialogTypes_None"));
            this.firstLineIndentTypes.Add(FirstLineIndentDialogTypes.FirstLine, LocalizationManager.GetString("Documents_ParagraphPropertiesDialog_FirstLineIndentDialogTypes_FirstLine"));
            this.firstLineIndentTypes.Add(FirstLineIndentDialogTypes.Hanging, LocalizationManager.GetString("Documents_ParagraphPropertiesDialog_FirstLineIndentDialogTypes_Hanging"));

            this.comboAligment.ItemsSource = new List<Tuple<RadTextAlignment, string>>()
            {
                new Tuple<RadTextAlignment, string>(RadTextAlignment.Left, LocalizationManager.GetString("Documents_ParagraphPropertiesDialog_TextAlignment_Left")),
                new Tuple<RadTextAlignment, string>(RadTextAlignment.Center, LocalizationManager.GetString("Documents_ParagraphPropertiesDialog_TextAlignment_Center")),
                new Tuple<RadTextAlignment, string>(RadTextAlignment.Right, LocalizationManager.GetString("Documents_ParagraphPropertiesDialog_TextAlignment_Right")),
                new Tuple<RadTextAlignment, string>(RadTextAlignment.Justify, LocalizationManager.GetString("Documents_ParagraphPropertiesDialog_TextAlignment_Justify")),
#if SILVERLIGHT
                new Tuple<RadTextAlignment, string>(RadTextAlignment.Distribute, LocalizationManager.GetString("Documents_ParagraphPropertiesDialog_TextAlignment_Distribute")),
#endif
            };

            this.comboLineSpacing.ItemsSource = this.lineSpacingTypes.Values;

            if (StyleManager.ApplicationTheme != null)
            {
                SetCustomNumericStyle();
            }

            var numberFormat = (NumberFormatInfo)CultureInfo.CurrentCulture.NumberFormat.Clone();
            numberFormat.NumberDecimalDigits = 1;

            this.radNumLeftIndent.NumberFormatInfo = numberFormat;
            this.radNumRightIndent.NumberFormatInfo = numberFormat;
            this.radNumSpacingBefore.NumberFormatInfo = numberFormat;
            this.radNumSpacingAfter.NumberFormatInfo = numberFormat;

            this.radNumLineSpacing.NumberFormatInfo = (NumberFormatInfo)CultureInfo.CurrentCulture.NumberFormat.Clone();

            this.comboFirstIndentType.ItemsSource = this.firstLineIndentTypes.Values;
        }

        #endregion

        #region Methods

        private void SetCustomNumericStyle()
        {
            if (StyleManager.ApplicationTheme.ToString().Equals("Windows8Touch"))
            {
                this.NumericWidth = 160;
            }
            else
            {
                this.NumericWidth = 85;
            }
        }

        public void ShowDialog(ParagraphPropertiesDialogContext context)
        {
            this.ShowDialogInternal(context);
        }

        private void ShowDialogInternal(ParagraphPropertiesDialogContext context)
        {
            this.context = context;

            this.SetInitialValues();
            this.UpdateUI(this.initialProperties);

            this.SetOwner(this.context.Owner);

            this.applyCallback = this.context.ApplyPropCallback;
            this.ShowDialog();
        }

        private void SetInitialValues()
        {
            StyleUIHelper helper = new StyleUIHelper(this.context.Owner);

            this.initialProperties = new ParagraphPropertiesDialogInfo();
            this.initialProperties.TextAlignment = helper.GetTextAlignmentOfParagraphStyle();

            this.initialProperties.AutomaticSpacingBefore = helper.GetAutomaticSpacingBeforeOfParagraphStyle();
            this.initialProperties.AutomaticSpacingAfter = helper.GetAutomaticSpacingAfterOfParagraphStyle();

            this.initialProperties.LineSpacing = helper.GetLineSpacingOfParagraphStyle();
            this.initialProperties.LineSpacingType = helper.GetLineSpacingTypeOfParagraphStyle();
            this.initialLineSpacingDialogType = this.GetLineSpacingDialogType(initialProperties.LineSpacingType, initialProperties.LineSpacing);
            this.initialProperties.Background = helper.GetBackgroundOfParagraphStyle();
            this.initialProperties.FlowDirection = helper.GetFlowDirectionOfParagraphStyle();

            double? spacingBefore = helper.GetSpacingBeforeOfParagraphStyle();
            if (spacingBefore.HasValue)
            {
                this.initialProperties.SpacingBefore = spacingBefore.Value;
            }

            double? spacingAfter = helper.GetSpacingAfterOfParagraphStyle();
            if (spacingAfter.HasValue)
            {
                this.initialProperties.SpacingAfter = spacingAfter.Value;
            }

            double? rightIndent = helper.GetRightIndentOfParagraphStyle();
            if (rightIndent.HasValue)
            {
                this.initialProperties.RightIndent = rightIndent.Value;
            }

            double? firstLineIndent = helper.GetFirstLineIndentOfParagraphStyle();
            if (firstLineIndent.HasValue)
            {
                this.initialProperties.FirstLineIndent = firstLineIndent.Value;
            }

            double? leftIndent = helper.GetLeftIndentOfParagraphStyle();
            if (leftIndent.HasValue)
            {
                this.initialProperties.LeftIndent = leftIndent.Value;
            }
        }

        private void UpdateUI(ParagraphPropertiesDialogInfo properties)
        {
            this.BeginUpdateUI();

            this.UpdateUICore(properties);

            this.EndUpdateUI();
        }

        private void UpdateUICore(ParagraphPropertiesDialogInfo properties)
        {
            this.comboAligment.SelectedValue = properties.TextAlignment;

            this.UpdateSpacingsUI(properties);
            this.UpdateLineSpacingUI(properties.LineSpacingType, properties.LineSpacing);
            this.UpdateBackgroundUI(properties);
            this.UpdateFlowDirectionUI(properties.FlowDirection);
            this.UpdateIndentsUI(properties);
        }

        private void UpdateBackgroundUI(ParagraphPropertiesDialogInfo properties)
        {
            if (properties.Background.HasValue)
            {
                this.paragraphBackgroundColorSelector.SelectedColor = properties.Background.Value;
            }
            else
            {
                this.paragraphBackgroundColorSelector.ClearValue(DropDownColorPicker.SelectedColorProperty);
            }
        }

        private void UpdateSpacingsUI(ParagraphPropertiesDialogInfo properties)
        {
            if (properties.SpacingBefore.HasValue)
            {
                this.radNumSpacingBefore.Value = Unit.DipToPoint(properties.SpacingBefore.Value);
            }
            else
            {
                this.radNumSpacingBefore.Value = null;
            }

            if (properties.SpacingAfter.HasValue)
            {
                this.radNumSpacingAfter.Value = Unit.DipToPoint(properties.SpacingAfter.Value);
            }
            else
            {
                this.radNumSpacingAfter.Value = null;
            }

            this.checkBoxAutomaticSpacingBefore.IsChecked = properties.AutomaticSpacingBefore;
            this.checkBoxAutomaticSpacingAfter.IsChecked = properties.AutomaticSpacingAfter;
        }

        private void UpdateIndentsUI(ParagraphPropertiesDialogInfo properties)
        {
            if (properties.RightIndent.HasValue)
            {
                this.radNumRightIndent.Value = Unit.DipToPoint(properties.RightIndent.Value);
            }
            else
            {
                this.radNumRightIndent.Value = null;
            }

            double? firstIndentValue = null;
            if (properties.FirstLineIndent.HasValue)
            {
                firstIndentValue = Unit.DipToPoint(properties.FirstLineIndent.Value);

                // < 0 is hanging; 
                // > 0 is first line; 
                // 0 is none;
                if (firstIndentValue > 0)
                {
                    this.radNumFirstIndent.Value = firstIndentValue;
                    this.comboFirstIndentType.SelectedItem = this.firstLineIndentTypes[FirstLineIndentDialogTypes.FirstLine];
                }
                else if (firstIndentValue < 0)
                {
                    this.comboFirstIndentType.SelectedItem = this.firstLineIndentTypes[FirstLineIndentDialogTypes.Hanging];
                    this.radNumFirstIndent.Value = -firstIndentValue;
                }
                else
                {
                    this.comboFirstIndentType.SelectedItem = this.firstLineIndentTypes[FirstLineIndentDialogTypes.None];
                }
            }
            else
            {
                this.radNumFirstIndent.Value = null;
            }

            if (properties.LeftIndent.HasValue)
            {
                double leftIndent = Unit.DipToPoint(properties.LeftIndent.Value);
                if (firstIndentValue.HasValue && firstIndentValue.Value < 0)
                {
                    leftIndent += firstIndentValue.Value;
                }

                this.radNumLeftIndent.Value = leftIndent;
            }
            else
            {
                this.radNumLeftIndent.Value = null;
            }
        }

        private void UpdateLineSpacingUI(LineSpacingType? lineSpacingType, double? lineSpacingValue)
        {
            this.comboLineSpacing.SelectedValue = this.GetLineSpacingDialogType(lineSpacingType, lineSpacingValue);
            this.radNumLineSpacing.Value = this.GetLineSpacingDialogValue(lineSpacingType, lineSpacingValue);
        }

        private void UpdateFlowDirectionUI(FlowDirection? flowDirection)
        {
            this.radioButtonLeftToRight.IsChecked = flowDirection == FlowDirection.LeftToRight;
            this.radioButtonRightToLeft.IsChecked = flowDirection == FlowDirection.RightToLeft;
        }

        private void BeginUpdateUI()
        {
            this.isUpdatingUI = true;
        }

        private void EndUpdateUI()
        {
            this.isUpdatingUI = false;
        }

        private void DefaultButton_Click(object sender, RoutedEventArgs e)
        {
            ParagraphProperties paragraphProperties = new StyleDefinition(StyleType.Paragraph).ParagraphProperties;

            ParagraphPropertiesDialogInfo propertiesInfo = new ParagraphPropertiesDialogInfo()
            {
                TextAlignment = paragraphProperties.TextAlignment,
                SpacingBefore = paragraphProperties.SpacingBefore,
                SpacingAfter = paragraphProperties.SpacingAfter,
                AutomaticSpacingBefore = paragraphProperties.AutomaticSpacingBefore,
                AutomaticSpacingAfter = paragraphProperties.AutomaticSpacingAfter,
                LineSpacingType = paragraphProperties.LineSpacingType,
                LineSpacing = paragraphProperties.LineSpacing,
                Background = paragraphProperties.Background,
                FlowDirection = paragraphProperties.FlowDirection,
                RightIndent = paragraphProperties.RightIndent,
                FirstLineIndent = paragraphProperties.FirstLineIndent,
                LeftIndent = paragraphProperties.LeftIndent
            };

            this.UpdateUICore(propertiesInfo);
            // When multiple paragraphs with different backgrounds are selected, 
            // the initial value is the default one and this prevents ColorChanged event to be fired. 
            // Thus, we need to set this flag manually.
            this.isBackgroundChanged = true;
        }

        private double? GetLineSpacingDialogValue(LineSpacingType? lineSpacingType, double? lineSpacingValue)
        {
            double? lineSpacingDialogValue = null;

            if (lineSpacingValue.HasValue)
            {
                if (lineSpacingType == LineSpacingType.Exact || lineSpacingType == LineSpacingType.AtLeast)
                {
                    lineSpacingDialogValue = Unit.DipToPoint(lineSpacingValue.Value);
                }
                else if (lineSpacingType == LineSpacingType.Auto)
                {
                    lineSpacingDialogValue = lineSpacingValue;
                }
            }

            return lineSpacingDialogValue;
        }

        private string GetLineSpacingDialogType(LineSpacingType? lineSpacingType, double? lineSpacingValue)
        {
            string lineSpacingDialogType = string.Empty;
            if (lineSpacingType == LineSpacingType.Exact)
            {
                lineSpacingDialogType = this.lineSpacingTypes[LineSpacingDialogTypes.Exactly];
            }
            else if (lineSpacingType == LineSpacingType.AtLeast)
            {
                lineSpacingDialogType = this.lineSpacingTypes[LineSpacingDialogTypes.AtLeast];
            }
            else if (lineSpacingType == LineSpacingType.Auto)
            {
                if (lineSpacingValue == 1.0)
                {
                    lineSpacingDialogType = this.lineSpacingTypes[LineSpacingDialogTypes.Single];
                }
                else if (lineSpacingValue == 1.5)
                {
                    lineSpacingDialogType = this.lineSpacingTypes[LineSpacingDialogTypes.LineAndAHalf];
                }
                else if (lineSpacingValue == 2.0)
                {
                    lineSpacingDialogType = this.lineSpacingTypes[LineSpacingDialogTypes.Double];
                }
                else
                {
                    lineSpacingDialogType = this.lineSpacingTypes[LineSpacingDialogTypes.Multiple];
                }
            }

            return lineSpacingDialogType;
        }
        public StyleDefinition GetParagraphStyleInfo()
        {
            StyleDefinition result = new StyleDefinition(StyleType.Paragraph);

            this.SetTextAlignment(result);
            this.SetFlowDirection(result);
            this.SetParagraphSpacings(result);
            this.SetLineSpacingInStyle(result);
            this.SetBackground(result);
            this.SetIndents(result);

            return result;
        }

        private void SetTextAlignment(StyleDefinition result)
        {
            if (comboAligment.SelectedValue != null && (RadTextAlignment)comboAligment.SelectedValue != this.initialProperties.TextAlignment)
            {
                result.SetPropertyValue(Paragraph.TextAlignmentProperty, (RadTextAlignment)comboAligment.SelectedValue);
            }
        }

        private void SetBackground(StyleDefinition result)
        {
            Color backgroundColor = paragraphBackgroundColorSelector.SelectedColor;
            if (backgroundColor != this.initialProperties.Background && this.isBackgroundChanged)
            {
                result.SetPropertyValue(Paragraph.BackgroundProperty, backgroundColor);
            }
        }

        private void SetFlowDirection(StyleDefinition result)
        {
            FlowDirection? flowDirection = this.GetFlowDirection();
            if (flowDirection.HasValue && flowDirection != this.initialProperties.FlowDirection)
            {
                result.SetPropertyValue(Paragraph.FlowDirectionProperty, flowDirection);
            }
        }

        private void SetIndents(StyleDefinition result)
        {
            if (this.radNumRightIndent.Value.HasValue)
            {
                double rightIndent = Unit.PointToDip(this.radNumRightIndent.Value.Value);
                if (rightIndent != this.initialProperties.RightIndent)
                {
                    result.SetPropertyValue(Paragraph.RightIndentProperty, rightIndent);
                }
            }

            if (this.isIndentationChanged)
            {
                double leftIndent = Unit.PointToDip(this.radNumLeftIndent.Value ?? 0);
                string firstIndentValue = this.comboFirstIndentType.SelectedValue.ToString();

                if (this.comboFirstIndentType.SelectedValue != null)
                {
                    double firstIndent = Unit.PointToDip(this.radNumFirstIndent.Value ?? this.radNumFirstIndent.Minimum);
                    if (firstIndentValue == this.firstLineIndentTypes[FirstLineIndentDialogTypes.FirstLine] && this.initialProperties.FirstLineIndent != firstIndent)
                    {
                        result.SetPropertyValue(Paragraph.FirstLineIndentProperty, firstIndent);
                    }
                    else if (firstIndentValue == this.firstLineIndentTypes[FirstLineIndentDialogTypes.Hanging])
                    {
                        // hanging indent is negative first indent
                        result.SetPropertyValue(Paragraph.FirstLineIndentProperty, -firstIndent);
                        leftIndent += firstIndent;
                    }
                    else
                    {
                        result.SetPropertyValue(Paragraph.FirstLineIndentProperty, 0);
                    }
                }

                if (leftIndent != this.initialProperties.LeftIndent)
                {
                    result.SetPropertyValue(Paragraph.LeftIndentProperty, leftIndent);
                }
            }
        }

        private void SetParagraphSpacings(StyleDefinition result)
        {
            if (this.radNumSpacingBefore.Value.HasValue)
            {
                double spacingBefore = Unit.PointToDip(this.radNumSpacingBefore.Value.Value);
                if (spacingBefore != this.initialProperties.SpacingBefore)
                {
                    result.SetPropertyValue(Paragraph.SpacingBeforeProperty, spacingBefore);
                }
            }

            if (this.radNumSpacingAfter.Value.HasValue)
            {
                double spacingAfter = Unit.PointToDip(this.radNumSpacingAfter.Value.Value);
                if (spacingAfter != this.initialProperties.SpacingAfter)
                {
                    result.SetPropertyValue(Paragraph.SpacingAfterProperty, spacingAfter);
                }
            }

            bool? automaticSpacingBefore = this.checkBoxAutomaticSpacingBefore.IsChecked;
            if (automaticSpacingBefore.HasValue && this.checkBoxAutomaticSpacingBefore.IsChecked != this.initialProperties.AutomaticSpacingBefore)
            {
                result.SetPropertyValue(Paragraph.AutomaticSpacingBeforeProperty, automaticSpacingBefore.Value);
            }

            bool? automaticSpacingAfter = this.checkBoxAutomaticSpacingAfter.IsChecked;
            if (automaticSpacingAfter.HasValue && automaticSpacingAfter != this.initialProperties.AutomaticSpacingAfter)
            {
                result.SetPropertyValue(Paragraph.AutomaticSpacingAfterProperty, automaticSpacingAfter);
            }
        }

        private void SetLineSpacingInStyle(StyleDefinition result)
        {
            if (this.comboLineSpacing.SelectedValue == null)
            {
                return;
            }

            string value = this.comboLineSpacing.SelectedValue.ToString();

            bool isLineSpacingChanged = this.radNumLineSpacing.Value != this.initialProperties.LineSpacing ||
                                        value != this.initialLineSpacingDialogType;

            if (isLineSpacingChanged)
            {
                double lineSpacing = Unit.PointToDip(this.radNumLineSpacing.Value ?? this.radNumLineSpacing.Minimum);

                if (value == this.lineSpacingTypes[LineSpacingDialogTypes.Single] && isLineSpacingChanged)
                {
                    result.SetPropertyValue(Paragraph.LineSpacingTypeProperty, LineSpacingType.Auto);
                    result.SetPropertyValue(Paragraph.LineSpacingProperty, 1.0);
                }
                else if (value == this.lineSpacingTypes[LineSpacingDialogTypes.LineAndAHalf] && isLineSpacingChanged)
                {
                    result.SetPropertyValue(Paragraph.LineSpacingTypeProperty, LineSpacingType.Auto);
                    result.SetPropertyValue(Paragraph.LineSpacingProperty, 1.5);
                }
                else if (value == this.lineSpacingTypes[LineSpacingDialogTypes.Double] && isLineSpacingChanged)
                {
                    result.SetPropertyValue(Paragraph.LineSpacingTypeProperty, LineSpacingType.Auto);
                    result.SetPropertyValue(Paragraph.LineSpacingProperty, 2.0);
                }
                else if (value == this.lineSpacingTypes[LineSpacingDialogTypes.Exactly] && isLineSpacingChanged)
                {
                    result.SetPropertyValue(Paragraph.LineSpacingTypeProperty, LineSpacingType.Exact);
                    result.SetPropertyValue(Paragraph.LineSpacingProperty, lineSpacing);
                }
                else if (value == this.lineSpacingTypes[LineSpacingDialogTypes.AtLeast] && isLineSpacingChanged)
                {
                    result.SetPropertyValue(Paragraph.LineSpacingTypeProperty, LineSpacingType.AtLeast);
                    result.SetPropertyValue(Paragraph.LineSpacingProperty, lineSpacing);
                }
                else if (value == this.lineSpacingTypes[LineSpacingDialogTypes.Multiple] && isLineSpacingChanged)
                {
                    result.SetPropertyValue(Paragraph.LineSpacingTypeProperty, LineSpacingType.Auto);
                    result.SetPropertyValue(Paragraph.LineSpacingProperty, this.radNumLineSpacing.Value);
                }
            }
        }

        private FlowDirection? GetFlowDirection()
        {
            if (this.radioButtonLeftToRight.IsChecked == true)
            {
                return FlowDirection.LeftToRight;
            }
            else if (this.radioButtonRightToLeft.IsChecked == true)
            {
                return FlowDirection.RightToLeft;
            }

            return null;
        }

        private void ResetInitialValues()
        {
            this.initialProperties = null;
            this.initialLineSpacingDialogType = null;
        }

        private void RecordIndentationChange()
        {
            if (!this.isUpdatingUI)
            {
                this.isIndentationChanged = true;
            }
        }

        protected override void OnClosed(WindowClosedEventArgs args)
        {
            base.OnClosed(args);

            this.Clear();
        }

        private void Clear()
        {
            this.ResetInitialValues();

            this.isIndentationChanged = false;
            this.isBackgroundChanged = false;
            this.applyCallback = null;
            this.Owner = null;
        }

        #endregion

        #region Event Handlers

        private void RadWindow_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Escape)
            {
                this.Close();
            }
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            StyleDefinition newStyle = this.GetParagraphStyleInfo();

            if (this.applyCallback != null)
            {
                this.applyCallback(newStyle);
            }

            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ComboLineSpacing_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string value = (string)comboLineSpacing.SelectedItem;

            if (value == this.lineSpacingTypes[LineSpacingDialogTypes.Single])
            {
                this.radNumLineSpacing.NumberFormatInfo.NumberDecimalDigits = 2;
                this.radNumLineSpacing.Value = 1;
                this.radNumLineSpacing.CustomUnit = null;
                this.radNumLineSpacing.IsEnabled = false;
            }
            else if (value == this.lineSpacingTypes[LineSpacingDialogTypes.LineAndAHalf])
            {
                this.radNumLineSpacing.NumberFormatInfo.NumberDecimalDigits = 2;
                this.radNumLineSpacing.Value = 1.5;
                this.radNumLineSpacing.CustomUnit = null;
                this.radNumLineSpacing.IsEnabled = false;
            }
            else if (value == this.lineSpacingTypes[LineSpacingDialogTypes.Double])
            {
                this.radNumLineSpacing.NumberFormatInfo.NumberDecimalDigits = 2;
                this.radNumLineSpacing.Value = 2;
                this.radNumLineSpacing.CustomUnit = null;
                this.radNumLineSpacing.IsEnabled = false;
            }
            else if (value == this.lineSpacingTypes[LineSpacingDialogTypes.Exactly])
            {
                this.radNumLineSpacing.NumberFormatInfo.NumberDecimalDigits = 0;
                this.radNumLineSpacing.Value = 12;
                this.radNumLineSpacing.CustomUnit = "pt";

                this.radNumLineSpacing.IsEnabled = true;
            }
            else if (value == this.lineSpacingTypes[LineSpacingDialogTypes.AtLeast])
            {
                this.radNumLineSpacing.NumberFormatInfo.NumberDecimalDigits = 0;
                this.radNumLineSpacing.Value = 12;
                this.radNumLineSpacing.CustomUnit = "pt";
                this.radNumLineSpacing.IsEnabled = true;
            }
            else if (value == this.lineSpacingTypes[LineSpacingDialogTypes.Multiple])
            {
                this.radNumLineSpacing.NumberFormatInfo.NumberDecimalDigits = 2;
                this.radNumLineSpacing.Value = 3;
                this.radNumLineSpacing.CustomUnit = null;
                this.radNumLineSpacing.IsEnabled = true;
            }
        }

        private void TabsButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            this.context.ShowTabStopsPropertiesDialogCallback();
        }

        private void RadNumIndent_ValueChanged(object sender, RadRangeBaseValueChangedEventArgs e)
        {
            this.RecordIndentationChange();
        }

        private void ComboFirstIndent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string value = comboFirstIndentType.SelectedItem.ToString();

            if (value == this.firstLineIndentTypes[FirstLineIndentDialogTypes.None])
            {
                this.radNumFirstIndent.Value = null;
            }
            else if ((value != this.firstLineIndentTypes[FirstLineIndentDialogTypes.FirstLine]) &&
                     (value != this.firstLineIndentTypes[FirstLineIndentDialogTypes.Hanging]))
            {
                Debug.Assert(false, "Unknown first line type selected.");
            }

            if ((this.oldFirstLineIndentType == this.firstLineIndentTypes[FirstLineIndentDialogTypes.None]) &&
                (value == this.firstLineIndentTypes[FirstLineIndentDialogTypes.FirstLine] ||
                 value == this.firstLineIndentTypes[FirstLineIndentDialogTypes.Hanging]))
            {
                this.radNumFirstIndent.Value = Unit.DipToPoint(CustomParagraphPropertiesDialog.DefaultFirstLineIndent);
            }

            this.oldFirstLineIndentType = value;

            this.RecordIndentationChange();
        }

        private void CheckBoxAutomaticSpacingBefore_Checked(object sender, RoutedEventArgs e)
        {
            this.radNumSpacingBefore.IsEnabled = false;
        }

        private void CheckBoxAutomaticSpacingBefore_Unchecked(object sender, RoutedEventArgs e)
        {
            this.radNumSpacingBefore.IsEnabled = true;
        }

        private void CheckBoxAutomaticSpacingAfter_Checked(object sender, RoutedEventArgs e)
        {
            this.radNumSpacingAfter.IsEnabled = false;
        }

        private void CheckBoxAutomaticSpacingAfter_Unchecked(object sender, RoutedEventArgs e)
        {
            this.radNumSpacingAfter.IsEnabled = true;
        }

        private void ParagraphBackgroundColorSelector_SelectedColorChanged(object sender, EventArgs e)
        {
            if (!this.isUpdatingUI)
            {
                this.isBackgroundChanged = true;
            }
        }

        #endregion
    }
}