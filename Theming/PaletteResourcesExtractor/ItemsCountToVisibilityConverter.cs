using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace PaletteResourcesExtractor
{
    public class ItemsCountToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var count = (int)value;
            bool isInverted = parameter != null && bool.Parse(parameter.ToString());
            if (isInverted)
            {
                return count > 0 ? Visibility.Collapsed : Visibility.Visible;
            }
            return count > 0 ? Visibility.Visible : Visibility.Collapsed;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
