using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace RegexValidationForTimeSpan_WPF
{
    public class SelectedTimeSpanConverter : IValueConverter
    {
        private static string format = "dd' days 'hh' hours 'mm' minutes 'ss' seconds 'fff' milliseconds'";
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string result = value == null ? "No TimeSpan is selected." : "Selected TimeSpan: " + ((TimeSpan)value).ToString(format);
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
