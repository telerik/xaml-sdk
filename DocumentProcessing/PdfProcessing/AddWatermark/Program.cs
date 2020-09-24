using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

#if NETCOREAPP
using Telerik.Documents.Core.Fonts;
using Telerik.Documents.Primitives;
#else
using System.Windows;
using System.Windows.Media;
#endif

using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf;
using Telerik.Windows.Documents.Fixed.Model;
using Telerik.Windows.Documents.Fixed.Model.ColorSpaces;
using Telerik.Windows.Documents.Fixed.Model.Common;
using Telerik.Windows.Documents.Fixed.Model.Editing;
using Telerik.Windows.Documents.Fixed.Model.Text;

namespace WatermarkTextDemo
{
    class Program
    {
        static void Main()
        {
            RadFixedDocument document = Program.ImportDocument("BlueTemplate.pdf");
            RadFixedPage page = document.Pages.First();

            Program.DeleteTextFragment(page, "This is the blue template!");
            Program.AddWatermarkText(page, "Watermark text!", 100);

            Program.ExportAndViewPdf(document, "testWatermarks.pdf");            
        }

        private static RadFixedDocument ImportDocument(string fileName)
        {
            PdfFormatProvider provider = new PdfFormatProvider();
            RadFixedDocument document = provider.Import(File.ReadAllBytes(fileName));

            return document;
        }

        private static void ExportAndViewPdf(RadFixedDocument document, string exportFileName)
        {
            if (File.Exists(exportFileName))
            {
                File.Delete(exportFileName);
            }

            File.WriteAllBytes(exportFileName, new PdfFormatProvider().Export(document));
            ProcessStartInfo psi = new ProcessStartInfo()
            {
                FileName = exportFileName,
                UseShellExecute = true
            };
            Process.Start(psi);
        }

        private static void AddWatermarkText(RadFixedPage page, string text, byte transparency)
        {
            FixedContentEditor editor = new FixedContentEditor(page);

            Block block = new Block();
            block.TextProperties.FontSize = 80;
            block.TextProperties.TrySetFont(new FontFamily("Arial"), FontStyles.Normal, FontWeights.Bold);
            block.HorizontalAlignment = Telerik.Windows.Documents.Fixed.Model.Editing.Flow.HorizontalAlignment.Center;
            block.GraphicProperties.FillColor = new RgbColor(transparency, 255, 0, 0);
            block.InsertText(text);

            double angle = -45;
            editor.Position.Rotate(angle);
            editor.Position.Translate(0, page.Size.Width);
            editor.DrawBlock(block, new Size(page.Size.Width / Math.Abs(Math.Sin(angle)), double.MaxValue));
        }

        private static void DeleteTextFragment(RadFixedPage page, string text)
        {
            ContentElementBase fragment = page.Content.FirstOrDefault((element) =>
                {
                    TextFragment textElement = element as TextFragment;

                    return textElement != null && textElement.Text == text;
                });

            if (fragment != null)
            {
                page.Content.Remove(fragment);
            }
        }
    }
}
