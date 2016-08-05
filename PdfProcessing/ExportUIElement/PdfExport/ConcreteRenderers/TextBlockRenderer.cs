using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ExportUIElement
{
    internal class TextBlockRenderer : UIElementRendererBase
    {
        internal override bool Render(UIElement element, PdfRenderContext context)
        {
            TextBlock textBlock = element as TextBlock;
            if (textBlock == null)
            {
                return false;
            }

            string text = textBlock.Text;
            Brush foreground = textBlock.Foreground;
            double width = textBlock.ActualWidth;
            double height = textBlock.ActualHeight;
            var fontFamily = textBlock.FontFamily;
            double fontSize = textBlock.FontSize;

            TextRenderer.DrawTextBlock(text, context, foreground, width, height, fontFamily, fontSize);

            return true;
        }
    }
}
