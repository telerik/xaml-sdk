using System;
using System.Windows.Data;
using Telerik.Charting;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ChartView;

namespace _2._5D_Chart
{
    public class PaletteBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var dp = (DataPoint)value;
            var series = (ChartSeries)dp.Presenter;
            int index = GetPaletteEntryIndex(dp);
            var entry = series.Chart.Palette.GetEntry(series, index);
            return entry.Value.Fill;
        }

        private int GetPaletteEntryIndex(DataPoint dp)
        {
            var series = (ChartSeries)dp.Presenter;
            var cartesianChart = series.Chart as RadCartesianChart;
            var pieChart = series.Chart as RadPieChart;
            int index;

            if (cartesianChart != null)
            {
                BarSeries barSeries = series as BarSeries;
                BubbleSeries bubbleSeries = series as BubbleSeries;
                if ((barSeries != null && barSeries.PaletteMode == SeriesPaletteMode.DataPoint) ||
                    (bubbleSeries != null && bubbleSeries.PaletteMode == SeriesPaletteMode.DataPoint))
                {
                    index = dp.Index;
                }
                else
                {
                    index = cartesianChart.Series.IndexOf((CartesianSeries)series);
                }
            }
            else if (pieChart != null)
            {
                index = pieChart.Series.IndexOf((PieSeries)series);
            }
            else
            {
                index = ((RadPolarChart)series.Chart).Series.IndexOf((PolarSeries)series);
            }

            return index;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
