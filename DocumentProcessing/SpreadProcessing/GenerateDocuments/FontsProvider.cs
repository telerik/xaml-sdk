using System;
using System.IO;
using Telerik.Windows.Documents.Core.Fonts;
using Telerik.Windows.Documents.Extensibility;

namespace GenerateDocuments
{
    internal class FontsProvider : FontsProviderBase
    {
        private readonly string fontFolder = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);

        public override byte[] GetFontData(FontProperties fontProperties)
        {
            string fontFamilyName = fontProperties.FontFamilyName;
            string fontFolder = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);


            if (fontFamilyName == "Calibri")
            {
                return this.GetFontDataFromFontFolder("calibri.ttf");
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