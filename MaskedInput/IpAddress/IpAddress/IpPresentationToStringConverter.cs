using System;
using System.Linq;
using System.Windows.Data;

namespace IpAddress
{
    public class IpPresentationToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return string.Empty;
            }
            else if (value is IpAddressPresentation)
            {
                IpAddressPresentation ipAddress = value as IpAddressPresentation;
                return this.CompletePart(ipAddress.PartA.ToString()) + this.CompletePart(ipAddress.PartB.ToString()) + 
                        this.CompletePart(ipAddress.PartC.ToString()) + this.CompletePart(ipAddress.PartD.ToString());
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
           throw new NotImplementedException();
        }

        private string CompletePart(string part)
        {
            if (part.Length == 3)
            {
                return part;
            }
            else if (part.Length == 2)
            {
                return "0" + part;
            }
            else if (part.Length == 1)
            {
                return "00" + part;
            }
            return string.Empty;
        }
    }
}
