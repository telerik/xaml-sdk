using System;
using System.Linq;
using System.Windows.Data;

namespace GlyphToolBox
{
    public class GlyphTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            byte[] bytes = System.Text.Encoding.Unicode.GetBytes(value.ToString());
            if (bytes.Length < 2)
            {
                return null;
            }

            if (value.ToString().StartsWith("&#x"))
            {
                return value.ToString();
            }

            string firstCode = bytes[0].ToString("x2");
            string categoryCode = bytes[1].ToString("x2"); //returns e0 e1 ... e9  

            string leftOver = string.Empty;
            if (bytes.Length > 2)
            {
                leftOver = System.Text.Encoding.Unicode.GetString(bytes, 2, bytes.Length - 2);
            }

            return "&#x" + categoryCode + firstCode + ";" + leftOver;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
