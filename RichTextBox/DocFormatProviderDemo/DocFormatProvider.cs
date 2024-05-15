using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DIaLOGIKa.b2xtranslator.DocFileFormat;
using DIaLOGIKa.b2xtranslator.OpenXmlLib;
using DIaLOGIKa.b2xtranslator.OpenXmlLib.WordprocessingML;
using DIaLOGIKa.b2xtranslator.StructuredStorage.Reader;
using DIaLOGIKa.b2xtranslator.WordprocessingMLMapping;
using Telerik.Windows.Documents.FormatProviders;
using Telerik.Windows.Documents.FormatProviders.OpenXml.Docx;
using Telerik.Windows.Documents.Model;

namespace DocFormatProviderDemo
{
    // Install from http://sourceforge.net/projects/b2xtranslator/ and add references from the install directory.
    // Then copy zlibwapi.dll from install directory to output dir.
    [CustomDocumentFormatProvider]
    public class DocFormatProvider : DocumentFormatProviderBase
    {
        private static readonly string name = "DocFormatProvider";
        private static readonly IEnumerable<string> supportedExtensions = new string[] { ".doc" };
        private readonly DocxFormatProvider docxProvider;

        public DocFormatProvider()
        {
            this.docxProvider = new DocxFormatProvider();
        }

        public override string Name
        {
            get
            {
                return DocFormatProvider.name;
            }
        }

        public override string FilesDescription
        {
            get
            {
                return "Doc Documents";
            }
        }

        public override IEnumerable<string> SupportedExtensions
        {
            get
            {
                return DocFormatProvider.supportedExtensions;
            }
        }

        public override bool CanImport
        {
            get { return true; }
        }

        public override bool CanExport
        {
            get
            {
                return false;
            }
        }

        public override RadDocument Import(Stream input)
        {
            using (StructuredStorageReader structuredStorageReader = new StructuredStorageReader(input))
            {
                WordDocument wordDocument = new WordDocument(structuredStorageReader);
                OpenXmlPackage.DocumentType documentType = Converter.DetectOutputType(wordDocument);

                string tempFileName = Path.GetTempFileName();

                WordprocessingDocument wordprocessingDocument = WordprocessingDocument.Create(tempFileName, documentType);
                Converter.Convert(wordDocument, wordprocessingDocument);

                RadDocument document;
                using (FileStream stream = File.OpenRead(tempFileName))
                {
                    document = this.docxProvider.Import(stream);
                }

                File.Delete(tempFileName);

                return document;
            }
        }

        public override void Export(RadDocument document, Stream output)
        {
            throw new NotSupportedException();
        }
    }
}
