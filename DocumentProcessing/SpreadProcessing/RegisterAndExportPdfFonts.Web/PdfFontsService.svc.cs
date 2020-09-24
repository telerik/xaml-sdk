using System;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Windows;
using System.Windows.Media;

namespace RegisterAndExportPdfFonts.Web
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class PdfFontsService
    {
        [OperationContract]
        public FontData GetFontData(string fontFamilyName, bool isItalic, bool isBold)
        {
            FontData data = new FontData()
            {
                IsValid = false,
                Bytes = null,
                FontFamilyName = fontFamilyName,
                IsItalic = isItalic,
                IsBold = isBold
            };

            FontFamily fontFamily = new FontFamily(fontFamilyName);
            FontStyle fontStyle = isItalic ? FontStyles.Italic : FontStyles.Normal;
            FontWeight fontWeight = isBold ? FontWeights.Bold : FontWeights.Normal;
            Typeface typeface = new Typeface(fontFamily, fontStyle, fontWeight, FontStretches.Normal);
            GlyphTypeface glyphTypeface;
            if (typeface.TryGetGlyphTypeface(out glyphTypeface))
            {
                using (var memoryStream = new MemoryStream())
                {
                    glyphTypeface.GetFontStream().CopyTo(memoryStream);
                    data.Bytes = memoryStream.ToArray();
                    data.IsValid = true;
                }
            }

            return data;
        }

        public class FontData
        {
            public bool IsValid { get; set; }
            public byte[] Bytes { get; set; }
            public string FontFamilyName { get; set; }
            public bool IsItalic { get; set; }
            public bool IsBold { get; set; }
        }
    }
}
