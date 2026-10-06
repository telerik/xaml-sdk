using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ChartView;
using Telerik.Charting;

namespace DragToSelect
{
    public static class ChartUtilities
    {
        private const string SelectionRectangleTag = "SelectionRectangleTag";

        public static readonly DependencyProperty IsDragToSelectEnabledProperty = DependencyProperty.RegisterAttached(
            "IsDragToSelectEnabled",
            typeof(bool),
            typeof(ChartUtilities),
            new PropertyMetadata(false, IsDragToSelectEnabledChanged));

        public static readonly DependencyProperty SelectionRectangleStyleProperty = DependencyProperty.RegisterAttached(
            "SelectionRectangleStyle",
            typeof(Style),
            typeof(ChartUtilities),
            new PropertyMetadata(null));

        private static readonly DependencyProperty FromPositionProperty = DependencyProperty.RegisterAttached(
            "FromPosition",
            typeof(Point),
            typeof(ChartUtilities),
            new PropertyMetadata(new Point()));

        private static readonly DependencyProperty ToPositionProperty = DependencyProperty.RegisterAttached(
            "ToPosition",
            typeof(Point),
            typeof(ChartUtilities),
            new PropertyMetadata(new Point()));

        private static readonly DependencyProperty IsSelectionRectangleShownProperty = DependencyProperty.RegisterAttached(
            "IsSelectionRectangleShown",
            typeof(bool),
            typeof(ChartUtilities),
            new PropertyMetadata(false));

        public static bool GetIsDragToSelectEnabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsDragToSelectEnabledProperty);
        }

        public static void SetIsDragToSelectEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(IsDragToSelectEnabledProperty, value);
        }

        public static Style GetSelectionRectangleStyle(DependencyObject obj)
        {
            return (Style)obj.GetValue(SelectionRectangleStyleProperty);
        }

        public static void SetSelectionRectangleStyle(DependencyObject obj, Style value)
        {
            obj.SetValue(SelectionRectangleStyleProperty, value);
        }

        private static Point GetFromPosition(DependencyObject obj)
        {
            return (Point)obj.GetValue(FromPositionProperty);
        }

        private static void SetFromPosition(DependencyObject obj, Point value)
        {
            obj.SetValue(FromPositionProperty, value);
        }

        private static Point GetToPosition(DependencyObject obj)
        {
            return (Point)obj.GetValue(ToPositionProperty);
        }

        private static void SetToPosition(DependencyObject obj, Point value)
        {
            obj.SetValue(ToPositionProperty, value);
        }

        private static bool GetIsSelectionRectangleShown(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsSelectionRectangleShownProperty);
        }

        private static void SetIsSelectionRectangleShown(DependencyObject obj, bool value)
        {
            obj.SetValue(IsSelectionRectangleShownProperty, value);
        }

        private static void IsDragToSelectEnabledChanged(DependencyObject target, DependencyPropertyChangedEventArgs args)
        {
            RadCartesianChart chart = target as RadCartesianChart;
            if (chart == null)
            {
                return;
            }

            if ((bool)args.OldValue)
            {
                chart.MouseLeftButtonDown -= chart_MouseLeftButtonDown;
                chart.MouseLeftButtonUp -= chart_MouseLeftButtonUp;
                chart.MouseMove -= chart_MouseMove;
            }

            if ((bool)args.NewValue)
            {
                chart.MouseLeftButtonDown += chart_MouseLeftButtonDown;
                chart.MouseLeftButtonUp += chart_MouseLeftButtonUp;
                chart.MouseMove += chart_MouseMove;
            }
        }

        private static void chart_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            RadCartesianChart chart = (RadCartesianChart)sender;
            Point fromPosition = e.GetPosition(chart);
            SetFromPosition(chart, fromPosition);
            SetToPosition(chart, fromPosition);
            var plotAreaClip = chart.PlotAreaClip;
            plotAreaClip.X += chart.PanOffset.X;
            plotAreaClip.Y += chart.PanOffset.Y;

            if (!plotAreaClip.Contains(fromPosition.X, fromPosition.Y))
            {
                return;
            }

            chart.CaptureMouse();

            Canvas adorner = Telerik.Windows.Controls.ChildrenOfTypeExtensions.ChildrenOfType<Canvas>(chart).First(c => c.Name == "adornerContainer");
            Style style = GetSelectionRectangleStyle(chart);
            FrameworkElement selectionRectangle = BuildSelectionRectangle(style);
            adorner.Children.Add(selectionRectangle);
            UpdateSelectionRectanglePositionAndSize(chart);
            SetIsSelectionRectangleShown(chart, true);
        }

        private static void chart_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            RadCartesianChart chart = (RadCartesianChart)sender;
            if (!GetIsSelectionRectangleShown(chart))
            {
                return;
            }

            chart.ReleaseMouseCapture();
            Canvas adorner = Telerik.Windows.Controls.ChildrenOfTypeExtensions.ChildrenOfType<Canvas>(chart).First(c => c.Name == "adornerContainer");
            FrameworkElement selectionRectangle = Telerik.Windows.Controls.ChildrenOfTypeExtensions.ChildrenOfType<FrameworkElement>(chart).First(r => object.Equals(r.Tag, SelectionRectangleTag));
            adorner.Children.Remove(selectionRectangle);
            SetIsSelectionRectangleShown(chart, false);
            UpdateDataPointsInSelectionRectangle(chart);
        }

        private static void chart_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            RadCartesianChart chart = (RadCartesianChart)sender;
            if (!GetIsSelectionRectangleShown(chart))
            {
                return;
            }

            var plotAreaClip = chart.PlotAreaClip;
            plotAreaClip.X += chart.PanOffset.X;
            plotAreaClip.Y += chart.PanOffset.Y;

            Point toPosition = e.GetPosition(chart);
            toPosition.X = Math.Max(plotAreaClip.X, toPosition.X);
            toPosition.X = Math.Min(toPosition.X, plotAreaClip.Right);
            toPosition.Y = Math.Max(plotAreaClip.Y, toPosition.Y);
            toPosition.Y = Math.Min(toPosition.Y, plotAreaClip.Bottom);
            SetToPosition(chart, toPosition);

            UpdateSelectionRectanglePositionAndSize(chart);
            
            // Instant selection - select items while dragging.
            //UpdateDataPointsInSelectionRectangle(chart);
        }

        private static void UpdateSelectionRectanglePositionAndSize(RadCartesianChart chart)
        {
            Point fromPosition = GetFromPosition(chart);
            Point toPosition = GetToPosition(chart);
            Rect rect = new Rect(fromPosition, toPosition);
            FrameworkElement selectionRectangle = Telerik.Windows.Controls.ChildrenOfTypeExtensions.ChildrenOfType<FrameworkElement>(chart).First(r => object.Equals(r.Tag, SelectionRectangleTag));

            Canvas.SetLeft(selectionRectangle, rect.X);
            Canvas.SetTop(selectionRectangle, rect.Y);
            selectionRectangle.Width = rect.Width;
            selectionRectangle.Height = rect.Height;
        }

        private static void UpdateDataPointsInSelectionRectangle(RadCartesianChart chart)
        {
            Point fromPosition = GetFromPosition(chart);
            fromPosition.X -= chart.PanOffset.X;
            fromPosition.Y -= chart.PanOffset.Y;
            Point toPosition = GetToPosition(chart);
            toPosition.X -= chart.PanOffset.X;
            toPosition.Y -= chart.PanOffset.Y;
            Rect rect = new Rect(fromPosition, toPosition);

            foreach (CategoricalSeries series in chart.Series)
            {
                foreach (CategoricalDataPoint dp in series.DataPoints)
                {
                    RadPoint point = dp.LayoutSlot.Center;
                    dp.IsSelected = rect.Contains(new Point(point.X, point.Y));
                }
            }
        }

        private static FrameworkElement BuildSelectionRectangle(Style style)
        {
            FrameworkElement selectionRectangle = Activator.CreateInstance(style.TargetType) as FrameworkElement;
            selectionRectangle.Tag = SelectionRectangleTag;
            selectionRectangle.Style = style;
            return selectionRectangle;
        }
    }
}
