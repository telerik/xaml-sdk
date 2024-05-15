using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ExportUIElement
{
    internal class TextBoxRenderer : UIElementRendererBase
    {
        internal override bool Render(UIElement element, PdfRenderContext context)
        {
            TextBox textBox = element as TextBox;
            if (textBox == null)
            {
                return false;
            }

            string text = textBox.Text;
            Brush foreground = textBox.Foreground;
            double width = textBox.ActualWidth;
            double height = textBox.ActualHeight;
            var fontFamily = textBox.FontFamily;
            double fontSize = textBox.FontSize;

            using (SaveClip(context.drawingSurface, new Rect(0, 0, textBox.ActualWidth, textBox.ActualHeight)))
            {
                TextRenderer.DrawTextBlock(text, context, foreground, width, height, fontFamily, fontSize);
            }

            return true;
        }
    }
}
