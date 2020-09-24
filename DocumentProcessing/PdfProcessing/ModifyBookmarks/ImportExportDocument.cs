using System;
using System.Diagnostics;
using System.IO;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf;
using Telerik.Windows.Documents.Fixed.Model;

namespace ModifyBookmarks
{
    public static class ImportExportDocument
    {
        public static readonly string RootDirectory = AppDomain.CurrentDomain.BaseDirectory;
        public static readonly string InputFile = RootDirectory + "Resources\\SampleDocument.pdf";
        private static PdfFormatProvider provider = new PdfFormatProvider();

        public static RadFixedDocument ImportDocument()
        {
            return provider.Import(File.ReadAllBytes(InputFile));
        }

        public static void ExportDocument(RadFixedDocument document)
        {
            string modifiedFileName = "ModifiedDocument.pdf";

            if (File.Exists(modifiedFileName))
            {
                File.Delete(modifiedFileName);
            }

            File.WriteAllBytes(modifiedFileName, provider.Export(document));
            Process.Start(modifiedFileName);
        }
    }
}
