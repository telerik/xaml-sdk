using System;
using System.Globalization;
using System.Windows.Data;

namespace IpAddress
{
    public class IpPresentationToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((IpAddressPresentation)value).ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            IpAddressPresentation ipObject;

            if (!this.TryParseIpString(value.ToString(), out ipObject))
            {
                ipObject.IsValid = false;
            }
            else
            {
                ipObject.IsValid = true;
            }

            return ipObject;
        }

        private bool TryParseIpString(string ipText, out IpAddressPresentation ipAddressObject)
        {
            bool result;
            string[] parts = ipText.Split(new char[] { '.' });

            byte partA = 0, partB = 0, partC = 0, partD = 0;

            result = (byte.TryParse(parts[0], NumberStyles.Integer, CultureInfo.InvariantCulture, out partA) &&
                     byte.TryParse(parts[1], NumberStyles.Integer, CultureInfo.InvariantCulture, out partB) &&
                     byte.TryParse(parts[2], NumberStyles.Integer, CultureInfo.InvariantCulture, out partC) &&
                     byte.TryParse(parts[3], NumberStyles.Integer, CultureInfo.InvariantCulture, out partD));

            ipAddressObject = new IpAddressPresentation(partA, partB, partC, partD);
            return result;
        }
    }
}
