using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using Telerik.Windows.Controls.Spreadsheet.Layers;
using Telerik.Windows.Documents.Fixed.Model.Editing;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.Pdf.Export;
using Telerik.Windows.Documents.Spreadsheet.Model.Shapes;

namespace ExportChart
{
    public class WpfPdfChartImageRenderer : IPdfChartRenderer
    {
        private readonly ChartModelToImageConverter chartToImageConverter;

        public WpfPdfChartImageRenderer()
        {
            // The ChartModelToImageConverter object is readily available in the Telerik.Windows.Controls.Spreadsheet assembly and
            // uses internally the RadChartView control to visualize the chart and create an image.
            this.chartToImageConverter = new ChartModelToImageConverter();
        }

        // This is the method which will be called when the internal logic of the PdfFormatProvider reaches a chart which has to be rendered.
        public void RenderChart(FixedContentEditor editor, FloatingChartShape chart)
        {
            BitmapSource source = this.chartToImageConverter.GetBitmapSourceFromFloatingChartShape(chart, 300d, 300d);

            // The editor draws the image in the PDF.
            editor.DrawImage(this.StreamFromBitmapSource(source), new Size(chart.Width, chart.Height));
        }

        public Stream StreamFromBitmapSource(BitmapSource writeBmp)
        {
            Stream bmp = new MemoryStream();

            BitmapEncoder enc = new BmpBitmapEncoder();
            enc.Frames.Add(BitmapFrame.Create(writeBmp));
            enc.Save(bmp);

            return bmp;
        }
    }
}
