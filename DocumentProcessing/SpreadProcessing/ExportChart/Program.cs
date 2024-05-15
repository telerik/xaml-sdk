using System;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.Pdf;

namespace ExportChart
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Press Enter for converting a sample document to PDF or paste a path to a file you would like to convert: ");
            string input = Console.ReadLine();

            DocumentConverter converter = new DocumentConverter();

            PdfFormatProvider pdfFormatProvider = new PdfFormatProvider();

            // The PdfFormatProvider instance accepts a renderer in its settings. The renderer needs to implement the IPdfChartRenderer interface,
            // more specifically the RenderChart method. The method takes a FixedContentEditor in its parameters, which will draw the chart, and the
            // other parameters contain the information necessary to draw it. The WpfPdfChartImageRenderer is an example implementation which uses
            // the Telerik.Windows.Controls.Spreadsheet and Telerik.Windows.Controls.Chart assemblies to draw the chart.
            pdfFormatProvider.ExportSettings.ChartRenderer = new WpfPdfChartImageRenderer();

            converter.PdfFormatProvider = pdfFormatProvider;

            if (string.IsNullOrEmpty(input))
            {
                converter.ConvertSampleDocument();
            }
            else
            {
                converter.ConvertCustomDocument(input);
            }

            Console.ReadKey();
        }
    }
}
