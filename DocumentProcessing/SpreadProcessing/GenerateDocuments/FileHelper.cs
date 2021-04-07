using System;
using System.Diagnostics;
using System.IO;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.OpenXml.Xlsx;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.Pdf;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.TextBased.Csv;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.TextBased.Txt;
using Telerik.Windows.Documents.Spreadsheet.Model;

namespace GenerateDocuments
{
    public static class FileHelper
    {
        public const string XlsxFormat = "xlsx";
        public const string CsvFormat = "csv";
        public const string TxtFormat = "txt";
        public const string PdfFormat = "pdf";

        public static void SaveDocument(Workbook workbook, string selectedFormat)
        {
            IWorkbookFormatProvider formatProvider = GetFormatProvider(selectedFormat);

            if (formatProvider == null)
            {
                Console.WriteLine("Unknown or not supported format.");
                return;
            }

            string path = "Sample document." + selectedFormat;
            using (FileStream stream = File.OpenWrite(path))
            {
                formatProvider.Export(workbook, stream);
            }

            Console.WriteLine("Document generated.");

            ProcessStartInfo psi = new ProcessStartInfo()
            {
                FileName = path,
                UseShellExecute = true
            };
            Process.Start(psi);
        }

        private static IWorkbookFormatProvider GetFormatProvider(string extension)
        {
            switch (extension)
            {
                case XlsxFormat:
                    return new XlsxFormatProvider();
                case CsvFormat:
                    IWorkbookFormatProvider formatProvider = new CsvFormatProvider();
                    (formatProvider as CsvFormatProvider).Settings.HasHeaderRow = true;
                    return formatProvider;
                case TxtFormat:
                    return new TxtFormatProvider();
                case PdfFormat:
                    return new PdfFormatProvider();

                default:
                    return null;
            }
        }
    }
}