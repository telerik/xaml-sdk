using System;
using System.Windows.Data;

namespace AddNewItemsWithButton
{
    public class SearchTextToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
           return !string.IsNullOrEmpty((string)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
