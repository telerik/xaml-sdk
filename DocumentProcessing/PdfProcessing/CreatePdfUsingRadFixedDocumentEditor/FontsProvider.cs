using System;
using System.IO;
using Telerik.Documents.Core.Fonts;
using Telerik.Windows.Documents.Core.Fonts;
using Telerik.Windows.Documents.Extensibility;

namespace CreatePdfUsingRadFixedDocumentEditor
{
    internal class FontsProvider : FontsProviderBase
    {
        private readonly string fontFolder = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);

        public override byte[] GetFontData(FontProperties fontProperties)
        {
            string fontFamilyName = fontProperties.FontFamilyName;
            bool isItalic = fontProperties.FontStyle == FontStyles.Italic;
            bool isBold = fontProperties.FontWeight == FontWeights.Bold;

            if (fontFamilyName == "Jokerman")
            {
                return this.GetFontDataFromFontFolder("JOKERMAN.TTF");
            }
            else if (fontFamilyName == "Arial" && isItalic && isBold)
            {
                return this.GetFontDataFromFontFolder("arialbi.ttf");
            }
            else if (fontFamilyName == "Arial")
            {
                return this.GetFontDataFromFontFolder("arial.ttf");
            }

            return null;
        }

        private byte[] GetFontDataFromFontFolder(string fontFileName)
        {
            using (FileStream fileStream = File.OpenRead(this.fontFolder + "\\" + fontFileName))
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    fileStream.CopyTo(memoryStream);
                    return memoryStream.ToArray();
                }
            }
        }
    }
}