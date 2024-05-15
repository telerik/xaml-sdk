using System;
using System.Globalization;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;

namespace AddCustomElementInToolBox
{
    public static class WebUIFontToGeometryConverter
    {
        private static FontFamily telerikWebUIFont;
        private static Typeface telerikWebUITypeface;
        private static ResourceDictionary fontResourceDictionary;

        static WebUIFontToGeometryConverter()
        {
            fontResourceDictionary = (ResourceDictionary)Application.LoadComponent(new Uri("/Telerik.Windows.Controls;component/Themes/FontResources.xaml", UriKind.RelativeOrAbsolute));
            telerikWebUIFont = new FontFamily(new Uri("pack://application:,,,/Telerik.Windows.Controls;component/Themes/Fonts/", UriKind.RelativeOrAbsolute), "./#TelerikWebUI");            
            telerikWebUITypeface = new Typeface(telerikWebUIFont, FontStyles.Normal, FontWeights.Normal, FontStretches.Normal);            
        }

        public static Geometry ConvertGlyphNameToGeometry(string glyphName, double emFontSize, Brush foreground, Point geometryOffset)
        {   
            string glyphString = GetGlyphStringValueFromName(glyphName);
            FormattedText formattedText = new FormattedText(glyphString, CultureInfo.InvariantCulture, FlowDirection.LeftToRight, telerikWebUITypeface, emFontSize, foreground);
            Geometry geometry = formattedText.BuildGeometry(geometryOffset);
            return geometry;
        }

        public static Geometry ConvertGlyphStringValueToGeometry(string glyphString, double emFontSize, Brush foreground, Point geometryOffset)
        {   
            char glyphChar = ConvertGlyphTextToCharacter(glyphString);
            FormattedText formattedText = new FormattedText(glyphChar.ToString(), CultureInfo.InvariantCulture, FlowDirection.LeftToRight, telerikWebUITypeface, emFontSize, foreground);
            Geometry geometry = formattedText.BuildGeometry(geometryOffset);
            return geometry;
        }

        private static char ConvertGlyphTextToCharacter(string glyphText)
        {
            glyphText = glyphText.Substring(3, 4);
            return (char)int.Parse(glyphText, NumberStyles.HexNumber);
        }

        private static string GetGlyphStringValueFromName(string name)
        {            
            object glyph = fontResourceDictionary[name];
            if (glyph != null)
            {
                return (string)glyph;
            }
            else
            {
                string exceptionMessage = String.Format("Glyph with name '{0}' cannot be found.", name);
                throw new XamlParseException(exceptionMessage);
            }            
        }
    }
}
