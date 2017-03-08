using System;
using System.Diagnostics;
using System.IO;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf.Streaming;

namespace PdfStreamWriterPerformance
{
    class Program
    {
        public static readonly string RootDirectory = AppDomain.CurrentDomain.BaseDirectory;
        public static readonly string InputFileName = RootDirectory + "InputFiles\\BarChart.pdf";
        public const string ResultFileName = "MergeResult.pdf";
        public const int MergedDocumentPagesCount = 10000;

        static void Main()
        {
            if (File.Exists(ResultFileName))
            {
                File.Delete(ResultFileName);
            }

            using (PdfStreamWriter fileWriter = new PdfStreamWriter(File.OpenWrite(ResultFileName)))
            {
                fileWriter.Settings.DocumentInfo.Author = "Progress Software";
                fileWriter.Settings.DocumentInfo.Title = "Merged document";
                fileWriter.Settings.DocumentInfo.Description = "This big document is generated with PdfStreamWriter class in less than a second, with minimal memory footprint and optimized result file size.";

                using (PdfFileSource fileSource = new PdfFileSource(File.OpenRead(InputFileName)))
                {
                    PdfPageSource pageToMerge = fileSource.Pages[0];

                    for (int i = 0; i < MergedDocumentPagesCount; i++)
                    {
                        fileWriter.WritePage(pageToMerge);
                    }
                }
            }

            Process.Start(ResultFileName);
        }
    }
}
