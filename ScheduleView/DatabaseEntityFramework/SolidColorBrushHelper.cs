using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Windows.Media;

namespace DatabaseEntityFramework
{
    internal static class SolidColorBrushHelper
    {
        public static Brush FromNameString(string brushName)
        {
            string s = "<SolidColorBrush " + "xmlns=" + "'http://schemas.microsoft.com/winfx/2006/xaml/presentation'" + " Color='" + brushName + "' />";

            byte[] byteArray = Encoding.ASCII.GetBytes(s);

            MemoryStream stream = new MemoryStream(byteArray);

            try
            {
                return (Brush) XamlReader.Load(stream);
            }
            catch (Exception)
            {
                return FromHexString(brushName);
            }
        }

        private static Brush FromHexString(string colorArgb)
        {
            var colorHex = uint.Parse(colorArgb, NumberStyles.HexNumber);
            byte a = (byte) ((0xFF000000 & colorHex) / 0x01000000);
            byte r = (byte) ((0x00FF0000 & colorHex) / 0x00010000);
            byte g = (byte) ((0x0000FF00 & colorHex) / 0x00000100);
            byte b = (byte) ((0x000000FF & colorHex) / 0x00000001);
            return new SolidColorBrush(Color.FromArgb(a, r, g, b));
        }
    }
}
