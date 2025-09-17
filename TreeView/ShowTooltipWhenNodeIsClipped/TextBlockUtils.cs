using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ShowTooltipWhenNodeIsClipped
{
    public class TextBlockUtils
    {
        public static bool GetShowToolTipWhenClipped(DependencyObject obj)
        {
            return (bool)obj.GetValue(ShowToolTipWhenClippedProperty);
        }

        public static void SetShowToolTipWhenClipped(DependencyObject obj, bool value)
        {
            obj.SetValue(ShowToolTipWhenClippedProperty, value);
        }

        public static readonly DependencyProperty ShowToolTipWhenClippedProperty =
            DependencyProperty.RegisterAttached(
                "ShowToolTipWhenClipped",
                typeof(bool),
                typeof(TextBlockUtils),
                new PropertyMetadata(false, OnShowToolTipWhenClippedChanged));

        private static void OnShowToolTipWhenClippedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBlock txtBlock = d as TextBlock;
            if (txtBlock == null)
            {
                return;
            }

            bool showToolTip = (bool)e.NewValue;
            if (showToolTip)
            {
                txtBlock.ToolTipOpening += TxtBlock_ToolTipOpening;
            }
            else
            {
                txtBlock.ToolTipOpening -= TxtBlock_ToolTipOpening;
            }
        }

        private static void TxtBlock_ToolTipOpening(object sender, ToolTipEventArgs e)
        {
            TextBlock txtBlock = (TextBlock)sender;
            e.Handled = !IsTextTrimmed(txtBlock);
        }

        private static bool IsTextTrimmed(TextBlock headerTextBlock)
        {
            Typeface typeface = new Typeface(
                headerTextBlock.FontFamily,
                headerTextBlock.FontStyle,
                headerTextBlock.FontWeight,
                headerTextBlock.FontStretch);

            FormattedText formmatedText = new FormattedText(
                headerTextBlock.Text.ToString(),
                System.Threading.Thread.CurrentThread.CurrentCulture,
                headerTextBlock.FlowDirection,
                typeface,
                headerTextBlock.FontSize,
                headerTextBlock.Foreground);

            double roundWidth = Math.Round(formmatedText.Width);
            bool isTextTrimmed = roundWidth > headerTextBlock.ActualWidth;
            return isTextTrimmed;
        }
    }
}
