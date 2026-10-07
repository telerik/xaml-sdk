using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Telerik.Windows.Controls;
using Telerik.Charting;
using Telerik.Windows.Controls.ChartView;
using System.Windows.Controls;
using System.Windows.Media;

namespace TrackBallLikeAnnotations
{
    public class ChartUtilities
    {
        private static HashSet<RadCartesianChart> attachedCharts = new HashSet<RadCartesianChart>();
        private static RadCartesianChart lastMouseOveredChart;

        public static bool HidesAnnotationsOnMouseLeave { get; set; }

        public static string GetAnnotationsGroup(DependencyObject obj)
        {
            return (string)obj.GetValue(AnnotationsGroupProperty);
        }

        public static void SetAnnotationsGroup(DependencyObject obj, string value)
        {
            obj.SetValue(AnnotationsGroupProperty, value);
        }

        public static DataTemplate GetIntersectionPointTemplate(DependencyObject obj)
        {
            return (DataTemplate)obj.GetValue(IntersectionPointTemplateProperty);
        }

        public static void SetIntersectionPointTemplate(DependencyObject obj, DataTemplate value)
        {
            obj.SetValue(IntersectionPointTemplateProperty, value);
        }

        public static DataTemplate GetDataPointTemplate(DependencyObject obj)
        {
            return (DataTemplate)obj.GetValue(DataPointTemplateProperty);
        }

        public static void SetDataPointTemplate(DependencyObject obj, DataTemplate value)
        {
            obj.SetValue(DataPointTemplateProperty, value);
        }
                
        private static object GetCurrentXCategory(DependencyObject obj)
        {
            return (object)obj.GetValue(CurrentXCategoryProperty);
        }

        private static void SetCurrentXCategory(DependencyObject obj, object value)
        {
            obj.SetValue(CurrentXCategoryProperty, value);
        }

        public static readonly DependencyProperty AnnotationsGroupProperty = DependencyProperty.RegisterAttached("AnnotationsGroup", typeof(string), typeof(ChartUtilities), new PropertyMetadata(null, AnnotationsGroupChanged));

        public static readonly DependencyProperty IntersectionPointTemplateProperty = DependencyProperty.RegisterAttached("IntersectionPointTemplate", typeof(DataTemplate), typeof(ChartUtilities), new PropertyMetadata(null));

        public static readonly DependencyProperty DataPointTemplateProperty = DependencyProperty.RegisterAttached("DataPointTemplate", typeof(DataTemplate), typeof(ChartUtilities), new PropertyMetadata(null));

        private static readonly DependencyProperty CurrentXCategoryProperty = DependencyProperty.RegisterAttached("CurrentXCategory", typeof(object), typeof(ChartUtilities), new PropertyMetadata(null));

        private static void AnnotationsGroupChanged(DependencyObject target, DependencyPropertyChangedEventArgs args)
        {
            RadCartesianChart chart = target as RadCartesianChart;
            if (chart == null)
            {
                return;
            }

            string group = (string)args.NewValue;
            if (group != null && !attachedCharts.Contains(chart))
            {
                attachedCharts.Add(chart);
                chart.MouseMove += Chart_MouseMove;
                chart.MouseLeave += Chart_MouseLeave;
            }
            else if (group == null)
            {
                chart.MouseMove -= Chart_MouseMove;
                attachedCharts.Remove(chart);
                chart.MouseLeave -= Chart_MouseLeave;

                if (lastMouseOveredChart == chart)
                {
                    lastMouseOveredChart = null;
                }
            }
        }

        private static void Chart_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            RadCartesianChart chart = (RadCartesianChart)sender;
            if (HidesAnnotationsOnMouseLeave)
            {
                string group = GetAnnotationsGroup(chart);
                HideAnnotations(group);
            }
        }

        private static void Chart_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {            
            RadCartesianChart chart = (RadCartesianChart)sender;
            lastMouseOveredChart = chart;
            string group = GetAnnotationsGroup(chart);
            object currentXCategory = GetCurrentXCategory(chart);

            Point position = e.GetPosition(chart);
            if (!chart.PlotAreaClip.Contains(position.X - chart.PanOffset.X, position.Y - chart.PanOffset.Y))
            {
                if (HidesAnnotationsOnMouseLeave && currentXCategory != null)
                {
                    HideAnnotations(group);
                }

                return;
            }

            DataTuple tuple = chart.ConvertPointToData(position);
            object xCategory = tuple.FirstValue;

            if (object.Equals(xCategory, currentXCategory))
            {
                return;
            }

            UpdateCharts(group, xCategory);            
        }

        private static void HideAnnotations(string group)
        {
            foreach (var chart in attachedCharts.Where(chart => group == GetAnnotationsGroup(chart)))
            {
                RemoveAnnotations(chart);
                SetCurrentXCategory(chart, null);
            }
        }

        public static void UpdateCharts(string group, object xCategory)
        {
            var charts = attachedCharts.Where(chart => object.Equals(group, GetAnnotationsGroup(chart)));

            foreach (var chart in charts)
	        {
                SetCurrentXCategory(chart, xCategory);
                RemoveAnnotations(chart);
                AddAnnotations(chart, xCategory);
	        }
        }

        private static void RemoveAnnotations(RadCartesianChart chart)
        {
            var toBeRemoved = chart.Annotations.Where(ann => object.Equals(ann.Tag, typeof(ChartUtilities))).ToList();
            foreach (var ann in toBeRemoved)
            {
                chart.Annotations.Remove(ann);
            }
        }

        private static void AddAnnotations(RadCartesianChart chart, object xCategory)
        {
            chart.Annotations.Add(new CartesianGridLineAnnotation { Axis = chart.HorizontalAxis, Value = xCategory, Tag = typeof(ChartUtilities) });
            StackPanel dataPointsInfoPanel = new StackPanel();

            foreach (CartesianSeries series in chart.Series)
            {
                CategoricalSeries categoricalSeries = series as CategoricalSeries;
                if (categoricalSeries == null)
                {
                    continue;
                }

                var dataPoint = GetDataPoint(categoricalSeries.DataPoints, xCategory);
                if (dataPoint == null)
                {
                    continue;
                }

                AddIntersectionPoint(chart, GetIntersectionPointTemplate(series), dataPoint);
                ContentPresenter cp = new ContentPresenter();
                cp.ContentTemplate = GetDataPointTemplate(series);
                cp.Content = dataPoint;
                dataPointsInfoPanel.Children.Add(cp);
            }

            foreach (IndicatorBase indicator in chart.Indicators)
            {
                foreach (var dataPoint in GetDataPoints(indicator, xCategory))
                {
                    AddIntersectionPoint(chart, GetIntersectionPointTemplate(indicator), dataPoint);
                }
            }

            LinearAxis vAxis = (LinearAxis)chart.VerticalAxis;
            CartesianCustomAnnotation infoAnnotation = new CartesianCustomAnnotation();
            infoAnnotation.Tag = typeof(ChartUtilities);
            infoAnnotation.HorizontalValue = xCategory;
            infoAnnotation.VerticalValue = vAxis.ActualRange.Maximum;
            infoAnnotation.Content = PrepareInfoContent(dataPointsInfoPanel);
            chart.Annotations.Add(infoAnnotation);
        }

        private static object PrepareInfoContent(StackPanel dataPointsInfoPanel)
        {
            Border border = new Border();
            border.Child = dataPointsInfoPanel;
            border.Background = new SolidColorBrush(Colors.White);
            border.BorderBrush = new SolidColorBrush(Colors.Black);
            border.BorderThickness = new Thickness(1);
            border.CornerRadius = new CornerRadius(4);
            border.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            border.Margin = new Thickness(-border.DesiredSize.Width / 2, 0, 0, 0);

            return border;
        }

        private static CategoricalDataPoint GetDataPoint(DataPointCollection<CategoricalDataPoint> dataPoints, object xCategory)
        {
            foreach (var dp in dataPoints)
            {
                if (object.Equals(xCategory, dp.Category))
                {
                    return dp;
                }
            }

            return null;
        }

        private static IEnumerable<CategoricalDataPoint> GetDataPoints(IndicatorBase indicator, object xCategory)
        {
            yield return GetDataPoint(indicator.DataPoints, xCategory);

            var bollinger = indicator as BollingerBandsIndicator;
            if (bollinger != null)
            {
                yield return GetDataPoint(bollinger.LowerBandDataPoints, xCategory);
            }
        }

        private static void AddIntersectionPoint(RadCartesianChart chart, DataTemplate intersectionPointTemplate, CategoricalDataPoint dataPoint)
        {
            CartesianCustomAnnotation annotation = new CartesianCustomAnnotation();
            annotation.ContentTemplate = intersectionPointTemplate;
            annotation.HorizontalValue = dataPoint.Category;
            annotation.VerticalValue = dataPoint.Value;
            annotation.Tag = typeof(ChartUtilities);
            chart.Annotations.Add(annotation);
        }

        public static void RightKeyPressed()
        {
            var chart = lastMouseOveredChart;
            if (chart == null)
            {
                return;
            }

            object xCategory = GetCurrentXCategory(chart);
            object rightXCategory = null;
            bool categoryReached = false;

            for (int i = 0; i < chart.ActualWidth; i++)
            {
                DataTuple tuple = chart.ConvertPointToData(new Point(i, 0));
                if (object.Equals(xCategory, tuple.FirstValue))
                {
                    categoryReached = true;
                }
                else if (categoryReached)
                {
                    rightXCategory = tuple.FirstValue;
                    break;
                }
            }

            if (rightXCategory == null)
            {
                return;
            }

            UpdateCharts(GetAnnotationsGroup(chart), rightXCategory);
        }

        public static void LeftKeyPressed()
        {
            var chart = lastMouseOveredChart;
            if (chart == null)
            {
                return;
            }

            object xCategory = GetCurrentXCategory(chart);

            if (xCategory == null)
            {
                return;
            }

            object leftXCategory = null;

            for (int i = 0; i <= chart.ActualWidth; i++)
            {
                DataTuple tuple = chart.ConvertPointToData(new Point(i, 0));
                if (object.Equals(xCategory, tuple.FirstValue))
                {
                    break;
                }

                leftXCategory = tuple.FirstValue;
            }

            if (leftXCategory == null)
            {
                return;
            }

            UpdateCharts(GetAnnotationsGroup(chart), leftXCategory);
        }
    }
}
