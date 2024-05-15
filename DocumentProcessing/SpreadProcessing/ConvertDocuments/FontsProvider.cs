using System;
using System.IO;
using Telerik.Documents.Core.Fonts;
using Telerik.Windows.Documents.Core.Fonts;
using Telerik.Windows.Documents.Extensibility;

namespace ConvertDocuments
{
    internal class FontsProvider : FontsProviderBase
    {
        private readonly string fontFolder = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);

        public override byte[] GetFontData(FontProperties fontProperties)
        {
            string fontFamilyName = fontProperties.FontFamilyName;
            bool isBold = fontProperties.FontWeight == FontWeights.Bold;
            string fontFolder = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);

            if (fontFamilyName == "Franklin Gothic Book" && isBold)
            {
                return this.GetFontDataFromFontFolder("FRABK.TTF");
            }
            else if (fontFamilyName == "Franklin Gothic Book")
            {
                return this.GetFontDataFromFontFolder("FRABK.TTF");
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