using System;
using System.Globalization;
using System.Windows.Data;
using Telerik.Windows.Controls.Scheduling;

namespace ConnectToDatabase_WPF
{
    public class RangeConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return new DateRange((DateTime)values[0], (DateTime)values[1]);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
