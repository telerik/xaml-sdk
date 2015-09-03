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
        private string oldFirstLineIndentType;

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
            this.UpdateUI(this.context.StyleInfo);
#if WPF
            this.SetOwner(this.context.Owner);
#else
            this.SetOwner(null);
#endif
            this.applyCallback = this.context.ApplyPropCallback;
            this.ShowDialog();
        }

        private void UpdateUI(StyleDefinition paragraphStyle)
        {
            this.comboAligment.SelectedValue = (RadTextAlignment)paragraphStyle.GetPropertyValue(Paragraph.TextAlignmentProperty);

            this.radNumSpacingBefore.Value = Unit.DipToPoint(((double?)paragraphStyle.GetPropertyValue(Paragraph.SpacingBeforeProperty)).Value);
            this.radNumSpacingAfter.Value = Unit.DipToPoint(((double?)paragraphStyle.GetPropertyValue(Paragraph.SpacingAfterProperty)).Value);

            this.checkBoxAutomaticSpacingBefore.IsChecked = ((bool?)paragraphStyle.GetPropertyValue(Paragraph.AutomaticSpacingBeforeProperty)).Value;
            this.checkBoxAutomaticSpacingAfter.IsChecked = ((bool?)paragraphStyle.GetPropertyValue(Paragraph.AutomaticSpacingAfterProperty)).Value;

            double leftIndent = Unit.DipToPoint(((double?)paragraphStyle.GetPropertyValue(Paragraph.LeftIndentProperty)).Value);
            this.radNumRightIndent.Value = Unit.DipToPoint(((double?)paragraphStyle.GetPropertyValue(Paragraph.RightIndentProperty)).Value);

            LineSpacingType lineSpacingType = ((LineSpacingType?)paragraphStyle.GetPropertyValue(Paragraph.LineSpacingTypeProperty)).Value;
            double lineSpacingValue = ((double?)paragraphStyle.GetPropertyValue(Paragraph.LineSpacingProperty)).Value;

            this.UpdateLineSpacingUI(lineSpacingType, lineSpacingValue);

            this.paragraphBackgroundColorSelector.SelectedColor = (Color)paragraphStyle.GetPropertyValue(Paragraph.BackgroundProperty);

            this.SetFlowDirection((FlowDirection)paragraphStyle.GetPropertyValue(Paragraph.FlowDirectionProperty));

            double firstIndentValue = Unit.DipToPoint(((double?)paragraphStyle.GetPropertyValue(Paragraph.FirstLineIndentProperty)).Value);

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
                leftIndent += firstIndentValue;
                this.radNumFirstIndent.Value = -firstIndentValue;
            }
            else
            {
                this.comboFirstIndentType.SelectedItem = this.firstLineIndentTypes[FirstLineIndentDialogTypes.None];
            }

            this.radNumLeftIndent.Value = leftIndent;
        }

        private void DefaultButton_Click(object sender, RoutedEventArgs e)
        {
            this.UpdateUI(new Paragraph().ExtractStyleFromProperties());
        }

        private void UpdateLineSpacingUI(LineSpacingType lineSpacingType, double lineSpacingValue)
        {
            if (lineSpacingType == LineSpacingType.Exact)
            {
                this.comboLineSpacing.SelectedValue = this.lineSpacingTypes[LineSpacingDialogTypes.Exactly];
                this.radNumLineSpacing.Value = Unit.DipToPoint(lineSpacingValue);
            }
            else if (lineSpacingType == LineSpacingType.AtLeast)
            {
                this.comboLineSpacing.SelectedValue = this.lineSpacingTypes[LineSpacingDialogTypes.AtLeast];
                this.radNumLineSpacing.Value = Unit.DipToPoint(lineSpacingValue);
            }
            else if (lineSpacingType == LineSpacingType.Auto)
            {
                if (lineSpacingValue == 1.0)
                {
                    this.comboLineSpacing.SelectedValue = this.lineSpacingTypes[LineSpacingDialogTypes.Single];
                }
                else if (lineSpacingValue == 1.5)
                {
                    this.comboLineSpacing.SelectedValue = this.lineSpacingTypes[LineSpacingDialogTypes.LineAndAHalf];
                }
                else if (lineSpacingValue == 2.0)
                {
                    this.comboLineSpacing.SelectedValue = this.lineSpacingTypes[LineSpacingDialogTypes.Double];
                }
                else
                {
                    this.comboLineSpacing.SelectedValue = this.lineSpacingTypes[LineSpacingDialogTypes.Multiple];
                    this.radNumLineSpacing.Value = lineSpacingValue;
                }
            }
            else
            {
                Debug.Assert(false, "Unknown LineSpacingType");
            }
        }

        public StyleDefinition GetParagraphStyleInfo()
        {
            StyleDefinition result = new StyleDefinition(StyleType.Paragraph);

            if (comboAligment.SelectedValue != null)
            {
                result.SetPropertyValue(Paragraph.TextAlignmentProperty, (RadTextAlignment)comboAligment.SelectedValue);
            }

            result.SetPropertyValue(Paragraph.FlowDirectionProperty, this.GetFlowDirection());

            result.SetPropertyValue(Paragraph.SpacingBeforeProperty, Unit.PointToDip(this.radNumSpacingBefore.Value ?? this.radNumSpacingBefore.Minimum));
            result.SetPropertyValue(Paragraph.SpacingAfterProperty, Unit.PointToDip(this.radNumSpacingAfter.Value ?? this.radNumSpacingAfter.Minimum));
            result.SetPropertyValue(Paragraph.AutomaticSpacingBeforeProperty, this.checkBoxAutomaticSpacingBefore.IsChecked ?? false);
            result.SetPropertyValue(Paragraph.AutomaticSpacingAfterProperty, this.checkBoxAutomaticSpacingAfter.IsChecked ?? false);
            result.SetPropertyValue(Paragraph.RightIndentProperty, Unit.PointToDip(this.radNumRightIndent.Value ?? 0));

            this.SetLineSpacingInStyle(result);

            result.SetPropertyValue(Paragraph.BackgroundProperty, paragraphBackgroundColorSelector.SelectedColor);

            double leftIndent = Unit.PointToDip(this.radNumLeftIndent.Value ?? 0);
            string firstIndentValue = this.comboFirstIndentType.SelectedValue.ToString();
            if (this.comboFirstIndentType.SelectedValue != null)
            {
                if (firstIndentValue == this.firstLineIndentTypes[FirstLineIndentDialogTypes.FirstLine])
                {
                    result.SetPropertyValue(Paragraph.FirstLineIndentProperty, Unit.PointToDip(this.radNumFirstIndent.Value ?? this.radNumFirstIndent.Minimum));
                }
                else if (firstIndentValue == this.firstLineIndentTypes[FirstLineIndentDialogTypes.Hanging])
                {
                    // hanging indent is negative first indent
                    result.SetPropertyValue(Paragraph.FirstLineIndentProperty, -Unit.PointToDip(this.radNumFirstIndent.Value ?? this.radNumFirstIndent.Minimum));
                    leftIndent += Unit.PointToDip(this.radNumFirstIndent.Value ?? this.radNumFirstIndent.Minimum);
                }
                else
                {
                    result.SetPropertyValue(Paragraph.FirstLineIndentProperty, 0);
                }
            }
            result.SetPropertyValue(Paragraph.LeftIndentProperty, leftIndent);

            return result;
        }

        private void SetFlowDirection(FlowDirection flowDirection)
        {
            this.radioButtonLeftToRight.IsChecked = flowDirection == FlowDirection.LeftToRight;
            this.radioButtonRightToLeft.IsChecked = flowDirection == FlowDirection.RightToLeft;
        }

        private FlowDirection GetFlowDirection()
        {
            if (this.radioButtonLeftToRight.IsChecked == true)
            {
                return FlowDirection.LeftToRight;
            }
            else if (this.radioButtonRightToLeft.IsChecked == true)
            {
                return FlowDirection.RightToLeft;
            }

            Debug.Assert(false, "Unset Table Flow Direction in UI.");

            return FlowDirection.LeftToRight;
        }

        private void SetLineSpacingInStyle(StyleDefinition result)
        {
            string value = comboLineSpacing.SelectedValue.ToString();

            if (value == this.lineSpacingTypes[LineSpacingDialogTypes.Single])
            {
                result.SetPropertyValue(Paragraph.LineSpacingTypeProperty, LineSpacingType.Auto);
                result.SetPropertyValue(Paragraph.LineSpacingProperty, 1.0);
            }
            else if (value == this.lineSpacingTypes[LineSpacingDialogTypes.LineAndAHalf])
            {
                result.SetPropertyValue(Paragraph.LineSpacingTypeProperty, LineSpacingType.Auto);
                result.SetPropertyValue(Paragraph.LineSpacingProperty, 1.5);
            }
            else if (value == this.lineSpacingTypes[LineSpacingDialogTypes.Double])
            {
                result.SetPropertyValue(Paragraph.LineSpacingTypeProperty, LineSpacingType.Auto);
                result.SetPropertyValue(Paragraph.LineSpacingProperty, 2.0);
            }
            else if (value == this.lineSpacingTypes[LineSpacingDialogTypes.Exactly])
            {
                result.SetPropertyValue(Paragraph.LineSpacingTypeProperty, LineSpacingType.Exact);
                result.SetPropertyValue(Paragraph.LineSpacingProperty, Unit.PointToDip(this.radNumLineSpacing.Value ?? this.radNumLineSpacing.Minimum));
            }
            else if (value == this.lineSpacingTypes[LineSpacingDialogTypes.AtLeast])
            {
                result.SetPropertyValue(Paragraph.LineSpacingTypeProperty, LineSpacingType.AtLeast);
                result.SetPropertyValue(Paragraph.LineSpacingProperty, Unit.PointToDip(this.radNumLineSpacing.Value ?? this.radNumLineSpacing.Minimum));
            }
            else if (value == this.lineSpacingTypes[LineSpacingDialogTypes.Multiple])
            {
                result.SetPropertyValue(Paragraph.LineSpacingTypeProperty, LineSpacingType.Auto);
                result.SetPropertyValue(Paragraph.LineSpacingProperty, this.radNumLineSpacing.Value);
            }
            else
            {
                Debug.Assert(false, "Unknown lines spacing type selected");
            }
        }

        protected override void OnClosed(WindowClosedEventArgs args)
        {
            base.OnClosed(args);

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

        private void comboLineSpacing_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
            else
            {
                Debug.Assert(false, "Unknown lines spacing type selected");
            }
        }

        private void TabsButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            this.context.ShowTabStopsPropertiesDialogCallback();
        }

        private void comboFirstIndent_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
        }

        #endregion

    }
}
