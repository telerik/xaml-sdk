using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Telerik.Windows.Documents.Common.FormatProviders;
using Telerik.Windows.Documents.Flow.FormatProviders.Docx;
using Telerik.Windows.Documents.Flow.FormatProviders.Html;
using Telerik.Windows.Documents.Flow.FormatProviders.Pdf;
using Telerik.Windows.Documents.Flow.FormatProviders.Rtf;
using Telerik.Windows.Documents.Flow.FormatProviders.Txt;
using Telerik.Windows.Documents.Flow.Model;

namespace ConvertDocuments
{
    internal class DocumentConverter
    {
        private static readonly string sampleDocumentFilePath = "SampleData\\SampleDocument.docx";

        private readonly List<IFormatProvider<RadFlowDocument>> providers;

        private RadFlowDocument document;

        public DocumentConverter()
        {
            this.providers = new List<IFormatProvider<RadFlowDocument>>()
            {
                new DocxFormatProvider(),
                new HtmlFormatProvider(),
                new PdfFormatProvider(),
                new RtfFormatProvider(),
                new TxtFormatProvider()
            };
        }

        public void ConvertCustomDocument(string fileName, string convertToFormat)
        {
            this.Open(fileName);
            this.Save(convertToFormat);
        }

        public void ConvertSampleDocument(string convertToFormat)
        {
            this.OpenSample();
            this.Save(convertToFormat);
        }

        private void Open(string fileName)
        {
            string extension = Path.GetExtension(fileName);
            IFormatProvider<RadFlowDocument> provider = this.providers
                                                            .FirstOrDefault(p => p.SupportedExtensions
                                                                                  .Any(e => string.Compare(extension, e, StringComparison.InvariantCultureIgnoreCase) == 0));

            if (provider != null)
            {
                using (Stream stream = File.OpenRead(fileName))
                {
                    try
                    {
                        this.document = provider.Import(stream);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Could not open file.");
                        this.document = null;
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
            using (Stream stream = File.OpenRead(DocumentConverter.sampleDocumentFilePath))
            {
                this.document = new DocxFormatProvider().Import(stream);
            }
        }

        private void Save(string format)
        {
            if (this.document == null)
            {
                Console.WriteLine("Cannot save. A document is not loaded.");
                return;
            }

            IFormatProvider<RadFlowDocument> formatProvider = null;
            switch (format)
            {
                case "docx":
                    formatProvider = new DocxFormatProvider();
                    break;
                case "html":
                    formatProvider = new HtmlFormatProvider();
                    break;
                case "rtf":
                    formatProvider = new RtfFormatProvider();
                    break;
                case "txt":
                    formatProvider = new TxtFormatProvider();
                    break;
                case "pdf":
                    formatProvider = new PdfFormatProvider();
                    break;
            }

            if (formatProvider == null)
            {
                Console.WriteLine("Not supported document format.");
                return;
            }

            string path = "Converted." + format;
            using (FileStream stream = File.OpenWrite(path))
            {
                formatProvider.Export(this.document, stream);
            }

            Console.WriteLine("Document converted.");

            ProcessStartInfo psi = new ProcessStartInfo()
            {
                FileName = path,
                UseShellExecute = true
            };
            Process.Start(psi);
        }
    }
}

