using System;
using System.Windows.Data;
using System.Windows.Media;

namespace CustomDragDropBehavior
{
    public class ReadOnlyToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool && (bool)value == true)
            {
                return new SolidColorBrush(Colors.Red);
            }
            else
            {
                return new SolidColorBrush(Colors.Transparent);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
