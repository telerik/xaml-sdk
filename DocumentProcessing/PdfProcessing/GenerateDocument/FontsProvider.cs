using System;
using System.IO;
using Telerik.Documents.Core.Fonts;
using Telerik.Windows.Documents.Core.Fonts;
using Telerik.Windows.Documents.Extensibility;

namespace GenerateDocument
{
    internal class FontsProvider : FontsProviderBase
    {
        private readonly string fontFolder = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);

        public override byte[] GetFontData(FontProperties fontProperties)
        {
            string fontFamilyName = fontProperties.FontFamilyName;
            bool isItalic = fontProperties.FontStyle == FontStyles.Italic;
            bool isBold = fontProperties.FontWeight == FontWeights.Bold;

            if (fontFamilyName == "Algerian")
            {
                return this.GetFontDataFromFontFolder("ALGER.TTF");
            }
            else if (fontFamilyName == "Arial")
            {
                return this.GetFontDataFromFontFolder("arial.ttf");
            }
            else if (fontFamilyName == "Calibri" && isItalic && isBold)
            {
                return this.GetFontDataFromFontFolder("calibriz.ttf");
            }
            else if (fontFamilyName == "Calibri")
            {
                return this.GetFontDataFromFontFolder("calibri.ttf");
            }
            else if (fontFamilyName == "Consolas" && isBold)
            {
                return this.GetFontDataFromFontFolder("consolaz.ttf");
            }
            else if (fontFamilyName == "Consolas")
            {
                return this.GetFontDataFromFontFolder("consola.ttf");
            }
            else if (fontFamilyName == "Lucida Calligraphy")
            {
                return this.GetFontDataFromFontFolder("LCALLIG.TTF");
            }
            else if (fontFamilyName == "Malgun Gothic")
            {
                return this.GetFontDataFromFontFolder("malgun.ttf");
            }
            else if (fontFamilyName == "Trebuchet MS")
            {
                return this.GetFontDataFromFontFolder("trebuc.ttf");
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