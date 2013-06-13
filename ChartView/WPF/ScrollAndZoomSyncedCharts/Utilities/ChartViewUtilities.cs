using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;
using Telerik.Windows.Controls.ChartView;

namespace ScrollAndZoomSyncedCharts
{
    public static class ChartViewUtilities
    {
        public static readonly DependencyProperty IsHorizontalRangeEnabledProperty = DependencyProperty.RegisterAttached(
            "IsHorizontalRangeEnabled",
            typeof(bool),
            typeof(ChartViewUtilities),
            new PropertyMetadata(false, IsHorizontalRangeEnabledChanged));

        public static readonly DependencyProperty HorizontalStartProperty = DependencyProperty.RegisterAttached(
            "HorizontalStart",
            typeof(double),
            typeof(ChartViewUtilities),
            new PropertyMetadata(0.0, HorizontalStartChanged));

        public static readonly DependencyProperty HorizontalEndProperty = DependencyProperty.RegisterAttached(
            "HorizontalEnd",
            typeof(double),
            typeof(ChartViewUtilities),
            new PropertyMetadata(1.0, HorizontalEndChanged));

        private static HashSet<RadChartBase> suspendedCharts = new HashSet<RadChartBase>();

        public static double GetHorizontalStart(DependencyObject obj)
        {
            return (double)obj.GetValue(HorizontalStartProperty);
        }

        public static void SetHorizontalStart(DependencyObject obj, double value)
        {
            obj.SetValue(HorizontalStartProperty, value);
        }

        public static double GetHorizontalEnd(DependencyObject obj)
        {
            return (double)obj.GetValue(HorizontalEndProperty);
        }

        public static void SetHorizontalEnd(DependencyObject obj, double value)
        {
            obj.SetValue(HorizontalEndProperty, value);
        }

        public static bool GetIsHorizontalRangeEnabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsHorizontalRangeEnabledProperty);
        }

        public static void SetIsHorizontalRangeEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(IsHorizontalRangeEnabledProperty, value);
        }

        private static void HorizontalStartChanged(DependencyObject target, DependencyPropertyChangedEventArgs args)
        {
            UpdateChartHorizontalPanOffsetAndZoom((RadChartBase)target);
        }

        private static void HorizontalEndChanged(DependencyObject target, DependencyPropertyChangedEventArgs args)
        {
            UpdateChartHorizontalPanOffsetAndZoom((RadChartBase)target);
        }

        private static void IsHorizontalRangeEnabledChanged(DependencyObject target, DependencyPropertyChangedEventArgs args)
        {
            var chart = (RadChartBase)target;
            if ((bool)args.NewValue == true)
            {
                chart.SizeChanged += Chart_SizeChanged;
                chart.ZoomChanged += Chart_ZoomChanged;
                chart.PanOffsetChanged += Chart_PanOffsetChanged;
            }
            else
            {
                chart.SizeChanged -= Chart_SizeChanged;
                chart.ZoomChanged -= Chart_ZoomChanged;
                chart.PanOffsetChanged -= Chart_PanOffsetChanged;
            }
        }

        private static void Chart_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var chart = (RadChartBase)sender;
            chart.SizeChanged -= Chart_SizeChanged; // we only need to know when the size changes initially
            UpdateChartHorizontalPanOffsetAndZoom(chart);
        }

        private static void Chart_ZoomChanged(object sender, ChartZoomChangedEventArgs e)
        {
            UpdateHorizontalRangeValues((RadChartBase)sender);
        }

        private static void Chart_PanOffsetChanged(object sender, ChartPanOffsetChangedEventArgs e)
        {
            UpdateHorizontalRangeValues((RadChartBase)sender);
        }

        private static void UpdateChartHorizontalPanOffsetAndZoom(RadChartBase chart)
        {
            if (ChartViewUtilities.GetIsHorizontalRangeEnabled(chart) && !suspendedCharts.Contains(chart))
            {
                suspendedCharts.Add(chart);

                if (chart.PlotAreaClip.Width > 0)
                {
                    double horizontalStart = ChartViewUtilities.GetHorizontalStart(chart);
                    double horizontalEnd = ChartViewUtilities.GetHorizontalEnd(chart);
                    double horizontalRange = horizontalEnd - horizontalStart;

                    if (horizontalRange > 0 && horizontalRange <= 1)
                    {
                        double newZoomX = 1 / horizontalRange;
                        double virtualWidth = newZoomX * chart.PlotAreaClip.Width;
                        double panOffsetX = -horizontalStart * virtualWidth;

                        chart.Zoom = new Size(newZoomX, chart.Zoom.Height);
                        chart.PanOffset = new Point(panOffsetX, chart.PanOffset.Y);
                    }
                }

                suspendedCharts.Remove(chart);
            }
        }

        private static void UpdateHorizontalRangeValues(RadChartBase chart)
        {
            if (ChartViewUtilities.GetIsHorizontalRangeEnabled(chart) && !suspendedCharts.Contains(chart))
            {
                suspendedCharts.Add(chart);

                var virtualWidth = chart.PlotAreaClip.Width * chart.Zoom.Width;
                if (virtualWidth > 0)
                {
                    double oldHorizontalStart = ChartViewUtilities.GetHorizontalStart(chart);
                    double oldHorizontalEnd = ChartViewUtilities.GetHorizontalEnd(chart);

                    double newHorizontalStart = (-chart.PanOffset.X) / virtualWidth;
                    double newHorizontalEnd = ((-chart.PanOffset.X) + chart.PlotAreaClip.Width) / virtualWidth;

                    ChartViewUtilities.SetHorizontalStart(chart, newHorizontalStart);
                    ChartViewUtilities.SetHorizontalEnd(chart, newHorizontalEnd);
                }

                suspendedCharts.Remove(chart);
            }
        }
    }
}
