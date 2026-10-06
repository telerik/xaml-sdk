using System;
using System.Globalization;
using System.Windows.Data;

namespace ErrorBars
{
    public class WidthPercentageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var percentage = double.Parse(parameter.ToString());
            var width = (double)value;
            return width * percentage;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
