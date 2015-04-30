using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Telerik.Windows.Controls.RichTextBoxUI.Dialogs;
using Telerik.Windows.Documents.Model;
using Telerik.Windows.Documents.Model.Styles;
using Telerik.Windows.Documents.UI.Extensibility;
using Telerik.Windows.Documents.UI.TextDecorations.DecorationProviders;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.RichTextBoxUI;
using Telerik.Windows.Controls.RichTextBoxUI.ColorPickers;
using Telerik.Windows.Documents.Layout;
using System.Globalization;
using Telerik.Windows.Documents.UI.Extensibility;
using Tuples = System;
using BaselineAlignment = Telerik.Windows.Documents.Model.BaselineAlignment;



namespace CustomFontPropertiesDialog
{
    [CustomFontPropertiesDialog]
    public partial class FontPropertiesDialog : RadRichTextBoxWindow, IFontPropertiesDialog
    {
         #region Fields

        bool isInInit = false;

        private Action<StyleDefinition> applyCallback;
        private StyleDefinition defaultStyle;
        private FontFamily initialFontFamily;
        private double? initialFontSize;
        private bool? initialStrikeThrough;
        private Color? initialForeColor;
        private Color? initialHighlightColor;
        private BaselineAlignment? initialBaselineAlignment;
        private DialogFontStyle initialDialogFontStyle;
        private IUnderlineUIDecorationProvider initialUnderline;
        private IUnderlineUIDecorationProvider currentUnderline;
        private Color? currentForeColor;
        private Color? currentHighlightColor;

        #endregion

        #region Constructors

        public FontPropertiesDialog()
        {
            InitializeComponent();

            this.fontStyleListBox.ItemsSource = new List<Tuples.Tuple<string, DialogFontStyle>>() 
            { 
                new Tuples.Tuple<string, DialogFontStyle>(LocalizationManager.GetString("Documents_FontPropertiesDialog_FontStyles_Regular"), DialogFontStyle.Regular),
                new Tuples.Tuple<string, DialogFontStyle>(LocalizationManager.GetString("Documents_FontPropertiesDialog_FontStyles_Italic"), DialogFontStyle.Italic),
                new Tuples.Tuple<string, DialogFontStyle>(LocalizationManager.GetString("Documents_FontPropertiesDialog_FontStyles_Bold"), DialogFontStyle.Bold),
                new Tuples.Tuple<string, DialogFontStyle>(LocalizationManager.GetString("Documents_FontPropertiesDialog_FontStyles_BoldItalic"), DialogFontStyle.BoldItalic),
            };
            this.fontSizeListBox.ItemsSource = new List<double> { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
        }

        #endregion

        #region Methods

        public void ShowDialog(FontPropertiesDialogContext context)
        {
            this.ShowDialogInternal(context.DefaultStyle, context.ApplyStyle, context.Owner);
        }

        #endregion

        #region PrivateMethods

        private void ShowDialogInternal(StyleDefinition defaultStyle, Action<StyleDefinition> applyCallback, RadRichTextBox owner)
        {
            this.defaultStyle = defaultStyle;
            this.applyCallback = applyCallback;
            this.SetOwner(owner);
            StyleUIHelper helper = new StyleUIHelper(owner);

            FontFamily fontFamily = helper.GetFontFamilyOfSpanStyle();
            double? fontSize = helper.GetFontSizeOfSpanStyle();
            Color? foreColor = helper.GetForeColorOfSpanStyle();
            Color? highlightColor = helper.GetHighlightColorOfSpanStyle();
            IUnderlineUIDecorationProvider underlineDecoration = helper.GetUnderlineDecoration();
            BaselineAlignment? baselineAlignment = helper.GetBaselineAlignment();
            FontWeight? fontWeight = helper.GetFontWeightOfSpanStyle();
            FontStyle? fontStyle = helper.GetFontStyleOfSpanStyle();
            bool? strikeThrough = helper.GetStrikeThroughOfSpanStyle();
            this.SetInitialValues(fontFamily, fontSize, foreColor, highlightColor, fontWeight, fontStyle, underlineDecoration, strikeThrough, baselineAlignment);
            this.SetFontPropertiesDialog(fontFamily, fontSize, foreColor, highlightColor, underlineDecoration, baselineAlignment, fontWeight, fontStyle, strikeThrough);

            this.ShowDialog();
        }

        private void SetInitialValues(FontFamily family, double? fontSize, Color? foreColor, Color? highlightColor, Tuples.Windows.FontWeight? fontWeight, Tuples.Windows.FontStyle? fontStyle, IUnderlineUIDecorationProvider underline, bool? strikeThrough, BaselineAlignment? alignment)
        {
            this.initialFontFamily = family;
            if (fontSize.HasValue)
            {
                this.initialFontSize = Math.Round(Unit.DipToPoint(fontSize.Value), 2);
            }

            this.initialForeColor = foreColor;
            this.initialHighlightColor = highlightColor;

            if (fontWeight.HasValue && fontWeight.HasValue)
            {
                if (fontWeight.Value == FontWeights.Bold)
                {
                    if (fontStyle.Value == FontStyles.Italic)
                    {
                        this.initialDialogFontStyle = DialogFontStyle.BoldItalic;
                    }
                    else
                    {
                        this.initialDialogFontStyle = DialogFontStyle.Bold;
                    }
                }
                else
                {
                    if (fontStyle.Value == FontStyles.Italic)
                    {
                        this.initialDialogFontStyle = DialogFontStyle.Italic;
                    }
                    else
                    {
                        this.initialDialogFontStyle = DialogFontStyle.Regular;
                    }
                }
            }
            else
            {
                this.initialDialogFontStyle = DialogFontStyle.Empty;
            }
            this.initialUnderline = underline;
            this.initialStrikeThrough = strikeThrough;
            this.initialBaselineAlignment = alignment;
        }

        private void ResetInitialValues()
        {
            this.initialForeColor = null;
            this.initialHighlightColor = null;
            this.initialBaselineAlignment = null;
            this.initialDialogFontStyle = DialogFontStyle.Empty;
            this.initialFontSize = null;
            this.initialFontFamily = null;
            this.initialUnderline = null;
            this.currentUnderline = null;
            this.currentForeColor = null;
            this.currentHighlightColor = null;
        }

        private void SetFontPropertiesDialog(FontFamily fontFamily, double? fontSize, Color? foreColor, Color? highlightColor,
            IUnderlineUIDecorationProvider underline, BaselineAlignment? baselineAlignment, FontWeight? fontWeight, FontStyle? fontStyle, bool? strikeThrough)
        {
            this.isInInit = true;
            this.SetFontFamily(fontFamily);
            this.SetFontSize(fontSize);
            this.SetForeColor(foreColor);
            this.SetHighlightColor(highlightColor);
            this.SetUnderlines(underline);
            this.SetBaselineAlignment(baselineAlignment);
            this.SetFontWeightAndStyle(fontWeight, fontStyle);
            this.SetStrikeThrough(strikeThrough);
            this.isInInit = false;
        }

        private void SetStrikeThrough(bool? strikeThrough)
        {
            this.strikeThrough.IsChecked = strikeThrough;
        }

        private void SetFontWeightAndStyle(FontWeight? fontWeight, FontStyle? fontStyle)
        {
            if (fontStyle.HasValue && fontWeight.HasValue)
            {
                if (fontStyle.Value == FontStyles.Italic)
                {
                    if (fontWeight.Value == FontWeights.Bold)
                    {
                        this.fontStyleListBox.SelectedValue = DialogFontStyle.BoldItalic;
                    }
                    else
                    {
                        this.fontStyleListBox.SelectedValue = DialogFontStyle.Italic;
                    }
                }
                else
                {
                    if (fontWeight.Value == FontWeights.Bold)
                    {
                        this.fontStyleListBox.SelectedValue = DialogFontStyle.Bold;
                    }
                    else
                    {
                        this.fontStyleListBox.SelectedValue = DialogFontStyle.Regular;
                    }
                }
            }
            else
            {
                this.fontStyleListBox.SelectedItem = null;
            }
        }

        private void SetBaselineAlignment(BaselineAlignment? baselineAlignment)
        {
            if (baselineAlignment.HasValue)
            {
                switch (baselineAlignment.Value)
                {
                    case (BaselineAlignment.Subscript):
                        this.subscript.IsChecked = true;
                        break;
                    case (BaselineAlignment.Superscript):
                        this.superscript.IsChecked = true;
                        break;
                    case (BaselineAlignment.Baseline):
                        this.subscript.IsChecked = false;
                        this.superscript.IsChecked = false;
                        break;
                    default:
                        throw new ArgumentException("Not supported baseline alignment");
                }
            }
            else
            {
                this.subscript.IsChecked = null;
                this.superscript.IsChecked = null;
            }
        }

        private void SetUnderlines(IUnderlineUIDecorationProvider underline)
        {
            string underlineToString = "None";
            if (underline != null)
            {
                DecorationUIConverter decorationConverter = new DecorationUIConverter();
                underlineToString = (string)decorationConverter.ConvertTo(underline, typeof(string));
            }

            this.currentUnderline = underline;

            if (underlineToString == "None")
            {
                foreach (CheckBox checkBox in underlinesGrid.Children)
                {
                    checkBox.IsChecked = false;
                }
            }
            else
            {
                foreach (CheckBox checkBox in underlinesGrid.Children)
                {
                    if (String.Equals(checkBox.Name, underlineToString, StringComparison.InvariantCultureIgnoreCase))
                    {
                        checkBox.IsChecked = true;
                        break;
                    }
                }
            }
        }

        private void SetHighlightColor(Color? highlightColor)
        {
            if (highlightColor.HasValue)
            {
                this.highlightColorSelector.SelectedColor = highlightColor.Value;
            }
            else
            {
                this.highlightColorSelector.ClearValue(DropDownColorPicker.SelectedColorProperty);
            }
        }

        private void SetForeColor(Color? foreColor)
        {
            if (foreColor.HasValue)
            {
                this.foreColorSelector.SelectedColor = foreColor.Value;
            }
            else
            {
                this.foreColorSelector.ClearValue(DropDownColorPicker.SelectedColorProperty);
            }
        }

        private void SetFontSize(double? fontSize)
        {
            if (fontSize.HasValue)
            {
                double size = Math.Round(Unit.DipToPoint(fontSize.Value), 1);
                if (this.fontSizeListBox.Items.Contains(size))
                {
                    this.fontSizeListBox.SelectedItem = size;
                    this.Dispatcher.BeginInvoke(new Action(() => this.fontSizeListBox.ScrollIntoView(this.fontSizeListBox.SelectedItem)));
                }
                else
                {
                    this.fontSizeListBox.SelectedItem = null;
                }
            }
            else
            {
                this.fontSizeListBox.SelectedItem = null;
            }
        }

        private void SetFontFamily(FontFamily family)
        {
            if (family != null)
            {
                FontFamilyInfo fontfamilyInfo = FontManager.GetRegisteredFontFamilyInfo(family);

                if (this.fontFamilyListBox.Items.Contains(fontfamilyInfo))
                {
                    this.fontFamilyListBox.SelectedItem = fontfamilyInfo;
                    this.Dispatcher.BeginInvoke(new Action(() => this.fontFamilyListBox.ScrollIntoView(this.fontFamilyListBox.SelectedItem)));
                }
                else
                {
                    this.fontFamilyListBox.SelectedItem = null;
                    if (fontfamilyInfo != null)
                    {
                        this.fontFamilyTextBox.Text = fontfamilyInfo.DisplayName;
                    }
                }
            }
            else
            {
                this.fontFamilyListBox.SelectedItem = family;
            }
        }

        private FontFamily GetSelectedFontFamily()
        {
            FontFamily selectedFontFamily = null;

            if (this.fontFamilyListBox.SelectedItem != null)
            {
                selectedFontFamily = (FontFamily)this.fontFamilyListBox.SelectedItem;
            }

            return selectedFontFamily;
        }

        private StyleDefinition GetStyleDefinitionToApply()
        {
            StyleDefinition result = new StyleDefinition(StyleType.Character);
            FontFamily selectedFontFamily = this.GetSelectedFontFamily();
            
            this.SetPropertyToStyleDefinitionIfDifferent<FontFamily, SpanProperties>(result, Span.FontFamilyProperty, selectedFontFamily, this.initialFontFamily);
            this.SetFontSizeToStyleDefinitionToApply(result);
            this.SetPropertyToStyleDefinitionIfDifferent<Color?, SpanProperties>(result, Span.ForeColorProperty, this.currentForeColor, this.initialForeColor);
            this.SetPropertyToStyleDefinitionIfDifferent<Color?, SpanProperties>(result, Span.HighlightColorProperty, this.currentHighlightColor, this.initialHighlightColor);
            this.SetUnderlineToStyleDefinitionToApply(result);
            this.SetBaselineAlignmentToStyleDefinitionToApply(result);
            this.SetFontWeightAndStyleToStyleDefinitionToApply(result);
            this.SetPropertyToStyleDefinitionIfDifferent<bool?, SpanProperties>(result, Span.StrikethroughProperty, this.strikeThrough.IsChecked, this.initialStrikeThrough);

            return result;
        }
  
        private void SetFontSizeToStyleDefinitionToApply(StyleDefinition result)
        {
            if (fontSizeListBox.SelectedItem as double? != this.initialFontSize)
            {
                double fontsize;
                if (double.TryParse(this.fontSizeTextBox.Text, out fontsize))
                {
                    result.SetPropertyValue(Span.FontSizeProperty, Unit.PointToDip(fontsize));
                }
            }
        }

        private void SetUnderlineToStyleDefinitionToApply(StyleDefinition result)
        {
            this.SetPropertyToStyleDefinitionIfDifferent<IUnderlineUIDecorationProvider, SpanProperties>(result, Span.UnderlineDecorationProperty, this.currentUnderline, this.initialUnderline);
        }

        private void SetBaselineAlignmentToStyleDefinitionToApply(StyleDefinition result)
        {
            if (this.subscript.IsChecked == true)
            {
                this.SetPropertyToStyleDefinitionIfDifferent<BaselineAlignment?, SpanProperties>(result, Span.BaselineAlignmentProperty, BaselineAlignment.Subscript, this.initialBaselineAlignment);
            }
            else if (this.superscript.IsChecked == true)
            {
                this.SetPropertyToStyleDefinitionIfDifferent<BaselineAlignment?, SpanProperties>(result, Span.BaselineAlignmentProperty, BaselineAlignment.Superscript, this.initialBaselineAlignment);
            }
            else if (this.subscript.IsChecked == false && this.superscript.IsChecked == false)
            {
                this.SetPropertyToStyleDefinitionIfDifferent<BaselineAlignment?, SpanProperties>(result, Span.BaselineAlignmentProperty, BaselineAlignment.Baseline, this.initialBaselineAlignment);
            }
        }

        private void SetFontWeightAndStyleToStyleDefinitionToApply(StyleDefinition result)
        {
            if (this.fontStyleListBox.SelectedValue != null && (DialogFontStyle)this.fontStyleListBox.SelectedValue != this.initialDialogFontStyle)
            {
                DialogFontStyle dialogFontStyle = (DialogFontStyle)this.fontStyleListBox.SelectedValue;
                switch (dialogFontStyle)
                {
                    case DialogFontStyle.Regular:
                        result.SetPropertyValue(Span.FontStyleProperty, FontStyles.Normal);
                        result.SetPropertyValue(Span.FontWeightProperty, FontWeights.Normal);
                        break;
                    case DialogFontStyle.Italic:
                        result.SetPropertyValue(Span.FontStyleProperty, FontStyles.Italic);
                        result.SetPropertyValue(Span.FontWeightProperty, FontWeights.Normal);
                        break;
                    case DialogFontStyle.Bold:
                        result.SetPropertyValue(Span.FontStyleProperty, FontStyles.Normal);
                        result.SetPropertyValue(Span.FontWeightProperty, FontWeights.Bold);
                        break;
                    case DialogFontStyle.BoldItalic:
                        result.SetPropertyValue(Span.FontStyleProperty, FontStyles.Italic);
                        result.SetPropertyValue(Span.FontWeightProperty, FontWeights.Bold);
                        break;
                    default:
                        break;
                }
            }
        }

        private void SetPropertyToStyleDefinitionIfDifferent<T, V>(StyleDefinition result, StylePropertyDefinition<T, V> stylePropertyDefinition, T valueToSet, T valueToCompare)
            where V : DocumentElementPropertiesBase
        {
            if (valueToSet != null && !valueToSet.Equals(valueToCompare))
            {
                result.SetPropertyValue(stylePropertyDefinition, valueToSet);
            }
        }

        private void DefaultButton_Click(object sender, RoutedEventArgs e)
        {
            SpanProperties spanProperties = this.defaultStyle.SpanProperties;
            ParagraphProperties paragraphProperties = this.defaultStyle.ParagraphProperties;
            this.SetFontPropertiesDialog(spanProperties.FontFamily, spanProperties.FontSize,
                spanProperties.ForeColor, spanProperties.HighlightColor,
                spanProperties.UnderlineDecoration, spanProperties.BaselineAlignment,
                spanProperties.FontWeight, spanProperties.FontStyle, spanProperties.Strikethrough);

            this.currentForeColor = spanProperties.ForeColor;
            this.currentHighlightColor = spanProperties.HighlightColor;
        }

        #endregion

        #region Event Handlers

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            StyleDefinition style = GetStyleDefinitionToApply();
            if (applyCallback != null)
            {
                applyCallback(style);
            }
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void Baseline_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox == this.superscript && (bool)this.superscript.IsChecked)
            {
                this.subscript.IsChecked = false;
            }
            if (checkBox == this.subscript && (bool)this.subscript.IsChecked)
            {
                this.superscript.IsChecked = false;
            }
        }

        private void Underlines_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox justChecked = sender as CheckBox;
            DecorationUIConverter decorationConverter = new DecorationUIConverter();
            this.currentUnderline = (IUnderlineUIDecorationProvider)decorationConverter.ConvertFrom(null, CultureInfo.CurrentCulture, justChecked.Name);
            foreach (CheckBox checkBox in this.underlinesGrid.Children)
            {
                if (checkBox != justChecked)
                {
                    checkBox.IsChecked = false;
                }
            }
        }

        private void Underlines_Unchecked(object sender, RoutedEventArgs e)
        {
            this.currentUnderline = UnderlineTypes.None;
        }

        private void fontFamilyListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (this.fontFamilyListBox.SelectedItem != null)
            {
                this.fontFamilyTextBox.Text = this.fontFamilyListBox.SelectedItem.ToString();
#if WPF
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    this.fontFamilyListBox.ScrollIntoView(this.fontFamilyListBox.SelectedItem);
                }), Tuples.Windows.Threading.DispatcherPriority.Loaded);
#else
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        this.fontFamilyListBox.ScrollIntoView(this.fontFamilyListBox.SelectedItem);
                    }));
                }));
#endif
            }
            else
            {
                this.fontFamilyTextBox.Text = string.Empty;
            }
        }

        private void fontSizeListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (this.fontSizeListBox.SelectedItem != null)
            {
                this.fontSizeTextBox.Text = this.fontSizeListBox.SelectedItem.ToString();
            }
            else
            {
                this.fontSizeTextBox.Text = string.Empty;
            }
        }

        private void foreColorSelector_SelectedColorChanged(object sender, EventArgs e)
        {
            if (!this.isInInit)
            {
                this.currentForeColor = this.foreColorSelector.SelectedColor;
            }
        }

        private void highlightColorSelector_SelectedColorChanged(object sender, EventArgs e)
        {
            if (!this.isInInit)
            {
                this.currentHighlightColor = this.highlightColorSelector.SelectedColor;
            }
        }

        protected override void OnClosed(WindowClosedEventArgs args)
        {
            base.OnClosed(args);
            this.ResetInitialValues();

            this.applyCallback = null;
            this.Owner = null;
        }

        #endregion

        public enum DialogFontStyle
        {
            Regular,
            Italic,
            Bold,
            BoldItalic,
            Empty
        }
    }
}
