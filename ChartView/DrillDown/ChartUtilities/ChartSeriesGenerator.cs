using System.Windows;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ChartView;

namespace DrillDown.ChartUtilities
{
    public static class ChartSeriesGenerator
    {
        public static DrillDownHelper GetDrillDownHelper(DependencyObject obj)
        {
            return (DrillDownHelper)obj.GetValue(DrillDownHelperProperty);
        }

        public static void SetDrillDownHelper(DependencyObject obj, DrillDownHelper value)
        {
            obj.SetValue(DrillDownHelperProperty, value);
        }

        public static readonly DependencyProperty DrillDownHelperProperty = DependencyProperty.RegisterAttached(
            "DrillDownHelper",
            typeof(DrillDownHelper),
            typeof(ChartSeriesGenerator),
            new PropertyMetadata(null, OnDrillDownHelperChanged));

        private static void OnDrillDownHelperChanged(DependencyObject target, DependencyPropertyChangedEventArgs args)
        {
            RadCartesianChart chart = target as RadCartesianChart;
            if (chart == null)
            {
                return;
            }

            chart.Series.Clear();

            DrillDownHelper driller = args.NewValue as DrillDownHelper;
            if (driller == null)
            {
                return;
            }

            CategoricalSeries series = CreateSeries(driller.CatBinding, driller.ValBinding, driller.Data, driller.SeriesType);
            chart.Series.Add(series);
        }

        private static CategoricalSeries CreateSeries(string catBinding, string valBinding, System.Collections.IEnumerable data, string seriesType)
        {
            CategoricalSeries series;

            if (seriesType == "LineSeries")
            {
                series = new LineSeries();
            }
            else
            {
                series = new BarSeries();
            }

            series.CategoryBinding = new PropertyNameDataPointBinding(catBinding);
            series.ValueBinding = new PropertyNameDataPointBinding(valBinding);
            series.ItemsSource = data;

            return series;
        }
    }
}
