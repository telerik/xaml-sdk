using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Telerik.Charting;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ChartView;

namespace DragChartAnnotation
{
    public static class AnnotationUtilities
    {
        private static Dictionary<CartesianChartAnnotation, bool> annotationToIsDragging = new Dictionary<CartesianChartAnnotation, bool>();
         
        public static readonly DependencyProperty IsDraggingEnabledProperty =
            DependencyProperty.RegisterAttached(
                "IsDraggingEnabled", 
                typeof(bool), 
                typeof(AnnotationUtilities), 
                new PropertyMetadata(false, OnIsDraggingEnabledChanged));

        public static bool GetIsDraggingEnabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsDraggingEnabledProperty);
        }

        public static void SetIsDraggingEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(IsDraggingEnabledProperty, value);
        }

        private static void OnIsDraggingEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ManageEventsRegistering((bool)e.NewValue, (CartesianChartAnnotation)d);
        }
        
        private static void Annotation_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var annotation = (CartesianChartAnnotation)sender;
            annotation.Cursor = null;
        }

        private static void Annotation_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var annotation = (CartesianChartAnnotation)sender;
            annotation.Cursor = Cursors.Hand;
        }

        private static void Annotation_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var annotation = (CartesianChartAnnotation)sender;
            annotation.CaptureMouse();
            annotationToIsDragging.Add(annotation, true);
        }

        private static void Annotation_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var annotation = (CartesianChartAnnotation)sender;
            annotation.ReleaseMouseCapture();
            annotationToIsDragging.Remove(annotation);
        }

        private static void Annotation_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var annotation = (CartesianChartAnnotation)sender;
            if (annotationToIsDragging.ContainsKey(annotation))
            {
                bool isDragging = annotationToIsDragging[annotation];
                if (isDragging)
                {
                    var chart = (RadCartesianChart)annotation.Chart;
                    Point mousePosition = e.GetPosition(chart);
                    Point coercedPosition = GetCoercedPosition(mousePosition, chart.PlotAreaClip);
                    DataTuple dataTuple = chart.ConvertPointToData(coercedPosition);

                    UpdateAnnotationPosition(annotation, dataTuple);
                }
            }                     
        }
        
        private static void UpdateAnnotationPosition(CartesianChartAnnotation annotation, DataTuple dataTuple)
        {
            if (annotation is CartesianGridLineAnnotation)
            {
                UpdateGridLineAnnotation((CartesianGridLineAnnotation)annotation, dataTuple);
            }
        }

        private static void UpdateGridLineAnnotation(CartesianGridLineAnnotation annotation, DataTuple dataTuple)
        {
            if (annotation.Chart != null)
            {
                var chart = (RadCartesianChart)annotation.Chart;
                if (annotation.Axis == chart.VerticalAxis)
                {
                    annotation.Value = dataTuple.SecondValue;
                }
                else
                {
                    annotation.Value = dataTuple.FirstValue;
                }
            }         
        }

        private static Point GetCoercedPosition(Point mousePosition, RadRect plotAreaClip)
        {
            var x = Math.Max(mousePosition.X, plotAreaClip.X);
            x = Math.Min(x, plotAreaClip.Right);
            var y = Math.Max(mousePosition.Y, plotAreaClip.Y);
            y = Math.Min(y, plotAreaClip.Bottom);

            return new Point(x, y);
        }

        private static void ManageEventsRegistering(bool isDraggingEnabled, CartesianChartAnnotation annotation)
        {
            UnregisterFromEvents(annotation);
            if (isDraggingEnabled)
            {
                annotation.MouseEnter += Annotation_MouseEnter;
                annotation.MouseLeftButtonDown += Annotation_MouseLeftButtonDown;
                annotation.MouseMove += Annotation_MouseMove;
                annotation.MouseLeftButtonUp += Annotation_MouseLeftButtonUp;
                annotation.MouseLeave += Annotation_MouseLeave;
                annotation.Unloaded += Annotation_Unloaded;
            }
        }

        private static void Annotation_Unloaded(object sender, RoutedEventArgs e)
        {
            UnregisterFromEvents((CartesianChartAnnotation)sender);
        }

        private static void UnregisterFromEvents(CartesianChartAnnotation annotation)
        {
            annotation.MouseEnter -= Annotation_MouseEnter;
            annotation.MouseLeftButtonDown -= Annotation_MouseLeftButtonDown;
            annotation.MouseMove -= Annotation_MouseMove;
            annotation.MouseLeftButtonUp -= Annotation_MouseLeftButtonUp;
            annotation.MouseLeave -= Annotation_MouseLeave;
            annotation.Unloaded -= Annotation_Unloaded;
        }
    }
}
