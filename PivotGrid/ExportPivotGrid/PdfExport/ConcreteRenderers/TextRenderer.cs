using System;
using System.Windows.Media;
using Telerik.Windows.Documents.Fixed.Model.Editing;

namespace ExportPivotGrid
{
    internal static class TextRenderer
    {
        public static void DrawTextBlock(string text, PdfRenderContext context, Brush foreground, double width, double height, FontFamily fontFamily, double fontSize, System.Windows.FontWeight fontWeight)
        {
            if (!string.IsNullOrEmpty(text))
            {
                using (context.drawingSurface.SaveProperties())
                {
                    UIElementRendererBase.SetFill(context, foreground, width, height);
                    SetFontFamily(context.drawingSurface, fontFamily, fontWeight);
                    context.drawingSurface.TextProperties.FontSize = fontSize;

                    Block block = new Block();
                    block.TextProperties.CopyFrom(context.drawingSurface.TextProperties);
                    block.GraphicProperties.CopyFrom(context.drawingSurface.GraphicProperties);
                    string[] textLines = text.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

                    foreach (string textLine in textLines)
                    {
                        block.InsertText(textLine);
                        block.InsertLineBreak();
                    }

                    context.drawingSurface.DrawBlock(block);
                }
            }
        }

        private static void SetFontFamily(Telerik.Windows.Documents.Fixed.Model.Editing.FixedContentEditor drawingSurface, System.Windows.Media.FontFamily fontFamily, System.Windows.FontWeight fontWeight)
        {
            if (!drawingSurface.TextProperties.TrySetFont(fontFamily, new System.Windows.FontStyle(), fontWeight))
            {
                throw new System.Exception("Unable to set font. Consider embedding the font.");
            }
        }
    }
}
