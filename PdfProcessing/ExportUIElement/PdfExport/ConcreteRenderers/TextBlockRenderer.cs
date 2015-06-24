using System.Windows;
using System.Windows.Controls;

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

            if (!string.IsNullOrEmpty(textBlock.Text))
            {
                using (context.drawingSurface.SaveProperties())
                {
                    SetFill(context, textBlock.Foreground, textBlock.ActualWidth, textBlock.ActualHeight);
                    SetFontFamily(context.drawingSurface, textBlock.FontFamily);
                    context.drawingSurface.TextProperties.FontSize = textBlock.FontSize;
                    context.drawingSurface.DrawText(textBlock.Text);
                }
            }

            return true;
        }

        private void SetFontFamily(Telerik.Windows.Documents.Fixed.Model.Editing.FixedContentEditor drawingSurface, System.Windows.Media.FontFamily fontFamily)
        {
#if WPF
            if (!drawingSurface.TextProperties.TrySetFont(fontFamily))
            {
                throw new System.Exception("Unable to set font. Consider embedding the font.");
            }
#elif SILVERLIGHT
            if (fontFamily.Source == "Times New Roman")
            {
                drawingSurface.TextProperties.Font = Telerik.Windows.Documents.Fixed.Model.Fonts.FontsRepository.TimesRoman;
            }
            else
            {
                throw new System.Exception("Unable to set font. Please see the RegisterAndExportPdfFonts sdk sample here https://github.com/telerik/xaml-sdk/tree/master/SpreadProcessing/RegisterAndExportPdfFonts");
            }
#endif
        }
    }
}
