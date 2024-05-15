using System;
using System.IO;
using Telerik.Windows.Documents.Core.Fonts;
using Telerik.Windows.Documents.Extensibility;

namespace CreateModifyExport
{
    internal class FontsProvider : FontsProviderBase
    {
        private readonly string fontFolder = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);

        public override byte[] GetFontData(FontProperties fontProperties)
        {
            string fontFamilyName = fontProperties.FontFamilyName;
            string fontFolder = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);

            if (fontFamilyName == "Segoe UI Light")
            {
                return this.GetFontDataFromFontFolder("segoeuil.ttf");
            }
            else if (fontFamilyName == "Cambria")
            {
                return this.GetFontDataFromFontFolder("cambria.ttc");
            }
            else if (fontFamilyName == "Segoe UI")
            {
                return this.GetFontDataFromFontFolder("segoeui.ttf");
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
