using System;
using System.Windows.Data;
using Telerik.Charting;
using Telerik.Windows.Controls.ChartView;

namespace SeriesDescriptorSelector
{
    public class ToLinearAxisConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            DailySeriesViewModel dailyVM = (DailySeriesViewModel)value;
            LinearAxis axis = new LinearAxis();
            axis.HorizontalLocation = AxisHorizontalLocation.Right;
            BindingOperations.SetBinding(axis, LinearAxis.ElementBrushProperty, new Binding("DailyStroke") { Source = dailyVM });
            return axis;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
