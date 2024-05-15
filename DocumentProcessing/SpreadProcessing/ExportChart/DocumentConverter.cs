using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.OpenXml.Xlsx;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.Pdf;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.TextBased.Csv;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.TextBased.Txt;
using Telerik.Windows.Documents.Spreadsheet.Model;

namespace ExportChart
{
    public class DocumentConverter
    {
        private static readonly string SampleDocumentFilePath = "SampleData\\VegetablesProducingReport.xlsx";

        private readonly List<IWorkbookFormatProvider> importProviders;

        private Workbook workbook;
        public Workbook Workbook
        {
            get
            {
                return this.workbook;
            }
            set
            {
                if (this.workbook != value)
                {
                    this.workbook = value;
                    this.IsDocumentLoaded = value != null;
                }
            }
        }

        private bool isDocumentLoaded;
        public bool IsDocumentLoaded
        {
            get
            {
                return this.isDocumentLoaded;
            }
            set
            {
                if (this.isDocumentLoaded != value)
                {
                    this.isDocumentLoaded = value;
                }
            }
        }

        private PdfFormatProvider pdfFormatProvider;

        public PdfFormatProvider PdfFormatProvider
        {
            get
            {
                return this.pdfFormatProvider;
            }
            set
            {
                if(this.pdfFormatProvider != value)
                {
                    this.pdfFormatProvider = value;
                }
            }
        }

        public DocumentConverter()
        {
            this.importProviders = new List<IWorkbookFormatProvider>()
            {
                new XlsxFormatProvider(),
                new CsvFormatProvider(),
                new TxtFormatProvider()
            };
        }

        public void ConvertCustomDocument(string fileName)
        {
            this.Open(fileName);
            this.Convert();
        }

        public void ConvertSampleDocument()
        {
            this.OpenSample();
            this.Convert();
        }

        private void Convert()
        {
            this.Save();
        }

        private void Open(string fileName)
        {
            string extension = Path.GetExtension(fileName);
            IWorkbookFormatProvider provider = this.importProviders
                .FirstOrDefault(p => p.SupportedExtensions
                    .Any(e => string.Compare(extension, e, StringComparison.InvariantCultureIgnoreCase) == 0));

            if (provider != null)
            {
                using (Stream stream = File.OpenRead(fileName))
                {
                    try
                    {
                        this.Workbook = provider.Import(stream);
                    }
                    catch (Exception)
                    {
                       Console.WriteLine("Could not open file.");
                        this.Workbook = null;
                    }
                }
            }
            else
            {
                Console.WriteLine("Could not open file.");
            }
        }

        private void OpenSample()
        {
            using (Stream stream = File.OpenRead(DocumentConverter.SampleDocumentFilePath))
            {
                this.Workbook = new XlsxFormatProvider().Import(stream);
            }
        }

        private void Save()
        {
            if (this.PdfFormatProvider == null)
            {
                Console.WriteLine("Format provider not set.");
                return;
            }

            string path = "Sample document.pdf";
            using (var stream = File.Create(path))
            {
                this.PdfFormatProvider.Export(this.Workbook, stream);
            }

            Console.WriteLine("Document converted.");
            Console.WriteLine("Opening document...");
            Process.Start(path);
        }
    }
}