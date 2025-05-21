using System;
using System.IO;
using System.Linq;
using System.Windows;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf.Export;
using Telerik.Windows.Documents.Fixed.Model;
using Telerik.Windows.Documents.Fixed.Model.ColorSpaces;
using Telerik.Windows.Documents.Fixed.Model.Data;
using Telerik.Windows.Documents.Fixed.Model.Editing;

namespace ExportPivotGrid
{
    public class PDFExportHelper
    {
        public static RadFixedDocument CreatePagedDocument(FrameworkElement element, bool landscaped)
        {
            RadFixedDocument document = new RadFixedDocument();
            double pageMagins = 20;
            var numberOfPages = landscaped ? Math.Ceiling(element.ActualHeight / element.ActualWidth * 1.4) : Math.Ceiling(element.ActualHeight / element.ActualWidth * .7);

            Size elementSizePerPage = new Size(element.ActualWidth, element.ActualHeight / numberOfPages);
            Size pageSize = new Size(elementSizePerPage.Width + 2 * pageMagins, elementSizePerPage.Height + 2 * pageMagins);

            for (int pageIndex = 0; pageIndex < numberOfPages; pageIndex++)
            {
                RadFixedPage page = document.Pages.AddPage();
                page.Size = pageSize;

                FixedContentEditor editor = new FixedContentEditor(page, MatrixPosition.Default);
                editor.Position.Translate(pageMagins, pageMagins);

                // Draw dashed lines at the page clipping margins.
                using (editor.SaveGraphicProperties())
                {
                    editor.GraphicProperties.IsFilled = false;
                    editor.GraphicProperties.IsStroked = true;
                    editor.GraphicProperties.StrokeColor = new RgbColor(200, 200, 200);
                    editor.GraphicProperties.StrokeDashArray = new double[] { 5, 5 };
                    editor.DrawRectangle(new Rect(0, 0, elementSizePerPage.Width, elementSizePerPage.Height));
                }

                // Clip and translate the element, so that it is positioned correctly on different pages.
                using (editor.PushClipping(new Rect(pageMagins, pageMagins, elementSizePerPage.Width, elementSizePerPage.Height)))
                {
                    editor.Position.Translate(0, -pageIndex * elementSizePerPage.Height);
                    ExportHelper.ExportToPdf(element, editor);
                }

            }

            return document;
        }

        public static RadFixedDocument CreateDocument(FrameworkElement element)
        {
            RadFixedDocument document = new RadFixedDocument();

            RadFixedPage page = CreatePage(element);
            document.Pages.Add(page);

            return document;
        }
        public static RadFixedPage CreatePage(FrameworkElement element)
        {
            //RadFixedPage page = new RadFixedPage { Size = new Size(1000, 1000) };

            RadFixedPage page = new RadFixedPage() { Size = new Size(element.ActualWidth, element.ActualHeight) };
            FixedContentEditor editor = new FixedContentEditor(page, MatrixPosition.Default);

            ExportHelper.ExportToPdf(element, editor);

            return page;
        }

        private static void PrepareForExport(FrameworkElement element)
        {
            if (element.ActualWidth == 0 && element.ActualHeight == 0)
            {
                double width = element.Width > 0 ? element.Width : 500;
                double height = element.Height > 0 ? element.Height : 300;
                element.Measure(Size.Empty);
                element.Measure(new Size(width, height));
                element.Arrange(new Rect(0, 0, width, height));
                element.UpdateLayout();
            }
        }

        public static void ExportToFile(FrameworkElement element, Stream stream)
        {
            //((System.Windows.Controls.Border)element).BorderBrush = new SolidColorBrush(Colors.Transparent);

            RadFixedDocument document = PDFExportHelper.CreateDocument(element);
            if (element.GetType().Name != "RadPivotGrid")
            {
                document.Pages.FirstOrDefault().Size = new Size(element.ActualWidth + 48, element.ActualHeight + 48);
            }
            else
            {
                PrepareForExport(element);
            }
            PdfFormatProvider provider = new PdfFormatProvider { ExportSettings = { ImageQuality = ImageQuality.High } };

            provider.Export(document, stream, null);
        }

        public static void ExportToPagedFile(FrameworkElement element, Stream stream, bool lanscaped)
        {
            RadFixedDocument document = PDFExportHelper.CreatePagedDocument(element, lanscaped);
            PdfFormatProvider provider = new PdfFormatProvider { ExportSettings = { ImageQuality = ImageQuality.High } };
            provider.Export(document, stream, null);
        }
    }
}
