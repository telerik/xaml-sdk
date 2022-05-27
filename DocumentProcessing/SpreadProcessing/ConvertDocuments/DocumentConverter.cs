using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.OpenXml.Xlsx;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.TextBased.Csv;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.TextBased.Txt;
using Telerik.Windows.Documents.Spreadsheet.Model;

namespace ConvertDocuments
{
    public class DocumentConverter
    {
        private const string SampleDocumentFilePath = "SampleData\\SampleDocument.xlsx";

        private readonly List<IWorkbookFormatProvider> providers;
        private readonly string defaultFromat = "xlsx";

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

        private string selectedExportFormat;
        public string SelectedExportFormat
        {
            get
            {
                return this.selectedExportFormat;
            }
            set
            {
                value = value.ToLower();
                if (!object.Equals(this.selectedExportFormat, value))
                {
                    this.selectedExportFormat = value;
                }
            }
        }

        public DocumentConverter()
        {
            this.providers = new List<IWorkbookFormatProvider>()
            {
                new XlsxFormatProvider(),
                new CsvFormatProvider(),
                new TxtFormatProvider()
            };

            this.SelectedExportFormat = this.defaultFromat;
        }

        public void ConvertCustomDocument(string fileName, string convertToFormat)
        {
            this.Open(fileName);
            this.Convert(convertToFormat);
        }

        public void ConvertSampleDocument(string convertToFormat)
        {
            this.OpenSample();
            this.Convert(convertToFormat);
        }

        private void Convert(string convertToFormat)
        {
            if (!string.IsNullOrEmpty(convertToFormat))
            {
                this.SelectedExportFormat = convertToFormat;
            }

            this.Save();
        }

        private void Open(string fileName)
        {
            string extension = Path.GetExtension(fileName);
            IWorkbookFormatProvider provider = this.providers
                .FirstOrDefault(p => p.SupportedExtensions
                    .Any(e => string.Compare(extension, e, StringComparison.OrdinalIgnoreCase) == 0));

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
            string selectedFormat = this.SelectedExportFormat;
            FileHelper.SaveDocument(this.Workbook, selectedFormat);
        }
    }
}