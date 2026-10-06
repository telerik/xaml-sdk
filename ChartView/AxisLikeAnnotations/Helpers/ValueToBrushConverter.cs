using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace AxisLikeAnnotations
{
    public class ValueToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var colors = parameter.ToString().Split(';');
            
            if ((double)value < 0)
            {
                return new SolidColorBrush((Color)ColorConverter.ConvertFromString(colors[1]));
            }
            return new SolidColorBrush((Color)ColorConverter.ConvertFromString(colors[0])); ;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
