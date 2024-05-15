using System;
using System.Globalization;
using System.Windows.Data;
using Telerik.Windows.Controls.ChartView;

namespace SeriesProvider
{
    public class SeriesTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SeriesViewModel seriesItem = value as SeriesViewModel;

            if (seriesItem.SeriesType == "Line")
            {
                return typeof(LineSeries);
            }
            else
            {
                return typeof(BarSeries);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
