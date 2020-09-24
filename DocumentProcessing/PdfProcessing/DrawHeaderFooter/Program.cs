using System;
using System.Diagnostics;
using System.IO;
#if NETCOREAPP
using Telerik.Documents.Primitives;
#else
using System.Windows;
#endif
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf;
using Telerik.Windows.Documents.Fixed.Model;
using Telerik.Windows.Documents.Fixed.Model.Editing;
using Telerik.Windows.Documents.Fixed.Model.Resources;

namespace DrawHeaderFooter
{
    internal class Program
    {
        public static readonly string RootDirectory = AppDomain.CurrentDomain.BaseDirectory;
        public static readonly string InputDocumentFile = RootDirectory + "InputFiles\\SampleDocument.pdf";
        public static readonly string InputLogoFile = RootDirectory + "InputFiles\\progress-logo.jpg";
        private static readonly string ExportedDocument = "ExportedSample.pdf";
        private static RadFixedDocument document;
        private static PdfFormatProvider formatProvider = new PdfFormatProvider();

        private static void Main(string[] args)
        {
            ImportDocument(InputDocumentFile);

            DrawHeaderAndFooterToDocument();

            ExportDocument(ExportedDocument);

            ProcessStartInfo psi = new ProcessStartInfo()
            {
                FileName = ExportedDocument,
                UseShellExecute = true
            };
            Process.Start(psi);
        }

        private static void ImportDocument(string inputDocumentFile)
        {
            using (Stream stream = File.OpenRead(inputDocumentFile))
            {
                document = formatProvider.Import(stream);
            }
        }

        public static void DrawHeaderAndFooterToDocument()
        {
            int numberOfPages = document.Pages.Count;
            for (int pageIndex = 0; pageIndex < numberOfPages; pageIndex++)
            {
                int pageNumber = pageIndex + 1;
                RadFixedPage currentPage = document.Pages[pageIndex];
                DrawHeaderAndFooterToPage(currentPage, pageNumber, numberOfPages);
            }
        }

        private static void DrawHeaderAndFooterToPage(RadFixedPage page, int pageNumber, int numberOfPages)
        {
            FixedContentEditor pageEditor = new FixedContentEditor(page);

            Block header = new Block();
            ImageSource imageSource = ImportImage(InputLogoFile);
            Size imageSize = new Size(168, 50);
            header.InsertImage(imageSource, imageSize);
            header.Measure();

            double headerOffsetX = (page.Size.Width / 2) - (header.DesiredSize.Width / 2);
            double headerOffsetY = 50;
            pageEditor.Position.Translate(headerOffsetX, headerOffsetY);
            pageEditor.DrawBlock(header);

            Block footer = new Block();
            footer.InsertText(String.Format("Page {0} of {1}", pageNumber, numberOfPages));
            footer.Measure();

            double footerOffsetX = (page.Size.Width / 2) - (footer.DesiredSize.Width / 2);
            double fotterOffsetY = page.Size.Height - 50 - footer.DesiredSize.Height;
            pageEditor.Position.Translate(footerOffsetX, fotterOffsetY);
            pageEditor.DrawBlock(footer);
        }

        private static ImageSource ImportImage(string inputLogoFile)
        {
            ImageSource imageSource;
            using (FileStream source = File.Open(inputLogoFile, FileMode.Open))
            {
                imageSource = new ImageSource(source);
            }

            return imageSource;
        }

        private static void ExportDocument(string exportedDocument)
        {
            using (Stream output = File.OpenWrite(exportedDocument))
            {
                formatProvider.Export(document, output);
            }
        }
    }
}
