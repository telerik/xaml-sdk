using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Telerik.Windows.Controls;
using Telerik.Windows;

namespace _2._5D_Chart
{
    public static class ChartViewUtilities
    {
        public static readonly DependencyProperty MarginLeftProperty = DependencyProperty.RegisterAttached(
            "MarginLeft",
            typeof(double),
            typeof(ChartViewUtilities),
            new PropertyMetadata(double.NaN, MarginLeftChanged));

        public static readonly DependencyProperty MarginTopProperty = DependencyProperty.RegisterAttached(
            "MarginTop",
            typeof(double),
            typeof(ChartViewUtilities),
            new PropertyMetadata(double.NaN, MarginTopChanged));

        public static readonly DependencyProperty MarginRightProperty = DependencyProperty.RegisterAttached(
            "MarginRight",
            typeof(double),
            typeof(ChartViewUtilities),
            new PropertyMetadata(double.NaN, MarginRightChanged));

        public static readonly DependencyProperty RadialGradientOriginAndCenterXProperty = DependencyProperty.RegisterAttached(
            "RadialGradientOriginAndCenterX",
            typeof(double),
            typeof(ChartViewUtilities),
            new PropertyMetadata(double.NaN, RadialGradientOriginAndCenterXChanged));

        public static readonly DependencyProperty RadialGradientOriginAndCenterYProperty = DependencyProperty.RegisterAttached(
            "RadialGradientOriginAndCenterY",
            typeof(double),
            typeof(ChartViewUtilities),
            new PropertyMetadata(double.NaN, RadialGradientOriginAndCenterYChanged));

        public static readonly DependencyProperty Is3DCameraEnabledProperty = DependencyProperty.RegisterAttached(
            "Is3DCameraEnabled",
            typeof(bool),
            typeof(ChartViewUtilities),
            new PropertyMetadata(false, Is3DCameraEnabledChanged));

        public static readonly DependencyProperty IsInitialAnimationEnabledProperty = DependencyProperty.RegisterAttached(
            "IsInitialAnimationEnabled",
            typeof(bool),
            typeof(ChartViewUtilities),
            new PropertyMetadata(false, IsInitialAnimationEnabledChanged));

        private static readonly DependencyProperty CameraInfoProperty = DependencyProperty.RegisterAttached(
            "CameraInfo",
            typeof(CameraInfo),
            typeof(ChartViewUtilities),
            new PropertyMetadata());

        private static readonly DependencyProperty ShouldTrackFillChangeProperty = DependencyProperty.RegisterAttached(
            "ShouldTrackFillChange",
            typeof(bool),
            typeof(ChartViewUtilities),
            new PropertyMetadata(false, ShouldTrackFillChangeChanged));

        private static readonly DependencyProperty EllipseFillWatcherProperty = DependencyProperty.RegisterAttached(
            "EllipseFillWatcher",
            typeof(DependencyPropertyWatcher<Brush>),
            typeof(ChartViewUtilities),
            new PropertyMetadata());

        internal const int MaxOffset = 20;

        public static double GetMarginLeft(DependencyObject obj)
        {
            return (double)obj.GetValue(MarginLeftProperty);
        }

        public static void SetMarginLeft(DependencyObject obj, double value)
        {
            obj.SetValue(MarginLeftProperty, value);
        }

        public static double GetMarginTop(DependencyObject obj)
        {
            return (double)obj.GetValue(MarginTopProperty);
        }

        public static void SetMarginTop(DependencyObject obj, double value)
        {
            obj.SetValue(MarginTopProperty, value);
        }

        public static double GetMarginRight(DependencyObject obj)
        {
            return (double)obj.GetValue(MarginRightProperty);
        }

        public static void SetMarginRight(DependencyObject obj, double value)
        {
            obj.SetValue(MarginRightProperty, value);
        }

        public static double GetRadialGradientOriginAndCenterX(DependencyObject obj)
        {
            return (double)obj.GetValue(RadialGradientOriginAndCenterXProperty);
        }

        public static void SetRadialGradientOriginAndCenterX(DependencyObject obj, double value)
        {
            obj.SetValue(RadialGradientOriginAndCenterXProperty, value);
        }

        public static double GetRadialGradientOriginAndCenterY(DependencyObject obj)
        {
            return (double)obj.GetValue(RadialGradientOriginAndCenterYProperty);
        }

        public static void SetRadialGradientOriginAndCenterY(DependencyObject obj, double value)
        {
            obj.SetValue(RadialGradientOriginAndCenterYProperty, value);
        }

        public static bool GetIs3DCameraEnabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(Is3DCameraEnabledProperty);
        }

        public static void SetIs3DCameraEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(Is3DCameraEnabledProperty, value);
        }

        public static bool GetIsInitialAnimationEnabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsInitialAnimationEnabledProperty);
        }

        public static void SetIsInitialAnimationEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(IsInitialAnimationEnabledProperty, value);
        }

        private static bool GetShouldTrackFillChange(DependencyObject obj)
        {
            return (bool)obj.GetValue(ShouldTrackFillChangeProperty);
        }

        private static void SetShouldTrackFillChange(DependencyObject obj, bool value)
        {
            obj.SetValue(ShouldTrackFillChangeProperty, value);
        }

        private static DependencyPropertyWatcher<Brush> GetEllipseFillWatcher(DependencyObject obj)
        {
            return (DependencyPropertyWatcher<Brush>)obj.GetValue(EllipseFillWatcherProperty);
        }

        private static void SetEllipseFillWatcher(DependencyObject obj, DependencyPropertyWatcher<Brush> value)
        {
            obj.SetValue(EllipseFillWatcherProperty, value);
        }

        private static CameraInfo GetOrCreateCameraInfo(RadCartesianChart chart)
        {
            CameraInfo cameraInfo = (CameraInfo)chart.GetValue(CameraInfoProperty);
            if (cameraInfo == null)
            {
                cameraInfo = new CameraInfo();
                chart.SetValue(CameraInfoProperty, cameraInfo);
            }

            return cameraInfo;
        }

        private static void MarginLeftChanged(DependencyObject target, DependencyPropertyChangedEventArgs args)
        {
            FrameworkElement element = (FrameworkElement)target;
            double left = (double)args.NewValue;
            var margin = element.Margin;
            element.Margin = new Thickness(left, margin.Top, margin.Right, margin.Bottom);
        }

        private static void MarginTopChanged(DependencyObject target, DependencyPropertyChangedEventArgs args)
        {
            FrameworkElement element = (FrameworkElement)target;
            double top = (double)args.NewValue;
            var margin = element.Margin;
            element.Margin = new Thickness(margin.Left, top, margin.Right, margin.Bottom);
        }

        private static void MarginRightChanged(DependencyObject target, DependencyPropertyChangedEventArgs args)
        {
            FrameworkElement element = (FrameworkElement)target;
            double right = (double)args.NewValue;
            var margin = element.Margin;
            element.Margin = new Thickness(margin.Left, margin.Top, right, margin.Bottom);
        }

        private static void RadialGradientOriginAndCenterXChanged(DependencyObject target, DependencyPropertyChangedEventArgs args)
        {
            Ellipse ellipse = (Ellipse)target;
            double x = (double)args.NewValue;
            SetRadialFill(ellipse, x, GetRadialGradientOriginAndCenterY(ellipse));
            EnsureFillChangedListener(ellipse, x, GetRadialGradientOriginAndCenterY(ellipse));
        }

        private static void RadialGradientOriginAndCenterYChanged(DependencyObject target, DependencyPropertyChangedEventArgs args)
        {
            Ellipse ellipse = (Ellipse)target;
            double y = (double)args.NewValue;
            SetRadialFill(ellipse, GetRadialGradientOriginAndCenterX(ellipse), y);
            EnsureFillChangedListener(ellipse, GetRadialGradientOriginAndCenterX(ellipse), y);
        }

        private static void Is3DCameraEnabledChanged(DependencyObject target, DependencyPropertyChangedEventArgs args)
        {
            RadCartesianChart chart = (RadCartesianChart)target;

            chart.MouseLeftButtonDown -= Chart_MouseDown;
            chart.MouseMove -= Chart_MouseMove;
            chart.MouseLeftButtonUp -= Chart_MouseUp;
            chart.LostMouseCapture -= Chart_LostMouseCapture;
            chart.Cursor = Cursors.Arrow;

            if ((bool)args.NewValue)
            {
                chart.MouseLeftButtonDown += Chart_MouseDown;
                chart.MouseMove += Chart_MouseMove;
                chart.MouseLeftButtonUp += Chart_MouseUp;
                chart.LostMouseCapture += Chart_LostMouseCapture;
                chart.Cursor = Cursors.SizeAll;
            }

            if (!GetIsInitialAnimationEnabled(chart))
            {
                MoveCamera(chart, new Point(100, -100));
            }
        }

        private static void IsInitialAnimationEnabledChanged(DependencyObject target, DependencyPropertyChangedEventArgs args)
        {
            RadCartesianChart chart = (RadCartesianChart)target;

            chart.Loaded -= Chart_Loaded;

            if ((bool)args.NewValue)
            {
                chart.Loaded += Chart_Loaded;
            }
        }

        private static void ShouldTrackFillChangeChanged(DependencyObject target, DependencyPropertyChangedEventArgs args)
        {
            Ellipse ellipse = (Ellipse)target;

            DependencyPropertyWatcher<Brush> watcher = GetEllipseFillWatcher(ellipse);
            if (watcher != null)
            {
                watcher.Dispose();
            }

            if ((bool)args.NewValue)
            {
                watcher = new DependencyPropertyWatcher<Brush>(ellipse, "Fill", Ellipse_FillChanged);
                SetEllipseFillWatcher(ellipse, watcher);
            }
        }

        private static void SetRadialFill(Ellipse ellipse, double hOffset, double vOffset)
        {
            double x = -hOffset / MaxOffset;
            double y = vOffset / MaxOffset;
            if (-1 <= x && x <= 1 && -1 <= y && y <= 1)
            {
                x = NormalizeOffset(x);
                y = NormalizeOffset(y);

                RadialGradientBrush oldBrush = (RadialGradientBrush)ellipse.Fill;
                Point newPoint = new Point(x, y);
                if (oldBrush != null && (oldBrush.GradientOrigin != newPoint || oldBrush.Center != newPoint))
                {
                    RadialGradientBrush newBrush = new RadialGradientBrush(oldBrush.GradientStops);
                    newBrush.GradientOrigin = newPoint;
                    newBrush.Center = newPoint;
                    ellipse.Fill = newBrush;
                }
            }
        }

        private static double NormalizeOffset(double value)
        {
            double inputMin = -1;
            double inputMax = 1;
            double outputMin = 0.35;
            double outputMax = 0.65;

            double inputRange = inputMax - inputMin;
            double outputRange = outputMax - outputMin;

            double relativeValue = (value - inputMin) / inputRange;
            double outputValue = outputMin + (relativeValue * outputRange);
            return outputValue;
        }

        private static void Chart_Loaded(object sender, RoutedEventArgs e)
        {
            RadCartesianChart chart = (RadCartesianChart)sender;
            CameraInfo cameraInfo = GetOrCreateCameraInfo(chart);

            if (cameraInfo.initialAnimationTimer != null)
            {
                cameraInfo.initialAnimationTimer.Stop();
            }

            cameraInfo.initialAnimationDateTime = DateTime.Now;
            cameraInfo.initialAnimationTimer = new DispatcherTimer();
            cameraInfo.initialAnimationTimer.Tick += (s, args) => { OnTimerTick(chart); };
            cameraInfo.initialAnimationTimer.Start();
        }

        private static void OnTimerTick(RadCartesianChart chart)
        {
            CameraInfo cameraInfo = GetOrCreateCameraInfo(chart);

            double durationInSeconds = 1.5;
            Point targetOffset = new Point(100, -75);

            TimeSpan timeSpan = DateTime.Now - cameraInfo.initialAnimationDateTime;
            if (durationInSeconds < timeSpan.TotalSeconds)
            {
                cameraInfo.initialAnimationTimer.Stop();
                cameraInfo.initialAnimationTimer = null;
                return;
            }

            double progress = timeSpan.TotalSeconds / durationInSeconds;

            EasingFunctionBase ease = new BackEase { EasingMode = EasingMode.EaseIn };
            double easedProgress = ease.Ease(progress);
            double hOffset = easedProgress * targetOffset.X;

            ease = new CubicEase { EasingMode = EasingMode.EaseInOut };
            easedProgress = ease.Ease(progress);
            double vOffset = easedProgress * targetOffset.Y;

            MoveCamera(chart, new Point(hOffset, vOffset));
        }

        private static void MoveCamera(RadCartesianChart chart, Point position)
        {
            Point newOffset = GetNewOffset(chart, position);

            double angleX = Math.Atan(newOffset.X / newOffset.Y) * 180 / Math.PI;
            double angleY = Math.Atan(newOffset.Y / newOffset.X) * 180 / Math.PI;

            chart.Resources["hOffset"] = newOffset.X;
            chart.Resources["hOffsetNegated"] = -newOffset.X;
            chart.Resources["hOffsetHalf"] = newOffset.X / 2;
            chart.Resources["vOffset"] = newOffset.Y;
            chart.Resources["vOffsetNegated"] = -newOffset.Y;
            chart.Resources["vOffsetNegatedHalf"] = -newOffset.Y / 2;
            chart.Resources["chart25DAngleX"] = -angleX;
            chart.Resources["chart25DAngleY"] = -angleY;
        }

        private static Point GetNewOffset(RadCartesianChart chart, Point pos)
        {
            CameraInfo cameraInfo = GetOrCreateCameraInfo(chart);

            Point pos1 = cameraInfo.mouseDownPosition;
            Point pos2 = pos;

            double hDelta = pos2.X - pos1.X;
            hDelta = hDelta / 10;
            double newHOffset = cameraInfo.mouseDownOffset.X + hDelta;
            newHOffset = Math.Max(Math.Min(newHOffset, MaxOffset), -MaxOffset);

            double vDelta = pos1.Y - pos2.Y;
            vDelta = vDelta / 10;
            double newVOffset = cameraInfo.mouseDownOffset.Y + vDelta;
            newVOffset = Math.Max(Math.Min(newVOffset, MaxOffset), 0);

            return new Point(newHOffset, newVOffset);
        }

        private static void Chart_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            RadCartesianChart chart = (RadCartesianChart)sender;
            CameraInfo cameraInfo = GetOrCreateCameraInfo(chart);

            object hOffsetResource = chart.TryFindResource("hOffset");
            object vOffsetResource = chart.TryFindResource("vOffset");
            double hOffset = hOffsetResource is double ? (double)hOffsetResource : 0.0;
            double vOffset = vOffsetResource is double ? (double)vOffsetResource : 0.0;

            cameraInfo.mouseDownPosition = e.GetPosition(chart);
            cameraInfo.mouseDownOffset = new Point(hOffset, vOffset);
            cameraInfo.isMouseCaptured = true;
            chart.CaptureMouse();
        }

        private static void Chart_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            RadCartesianChart chart = (RadCartesianChart)sender;
            CameraInfo cameraInfo = GetOrCreateCameraInfo(chart);
            if (cameraInfo.isMouseCaptured)
            {
                Point pos = e.GetPosition(chart);
                MoveCamera(chart, pos);
            }
        }

        private static void Chart_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            RadCartesianChart chart = (RadCartesianChart)sender;
            CameraInfo cameraInfo = GetOrCreateCameraInfo(chart);

            if (cameraInfo.isMouseCaptured)
            {
                chart.ReleaseMouseCapture();
                cameraInfo.isMouseCaptured = false;
            }
        }

        private static void Chart_LostMouseCapture(object sender, MouseEventArgs e)
        {
            RadCartesianChart chart = (RadCartesianChart)sender;
            CameraInfo cameraInfo = GetOrCreateCameraInfo(chart);

            if (cameraInfo.isMouseCaptured)
            {
                cameraInfo.isMouseCaptured = false;
            }
        }

        private static void EnsureFillChangedListener(Ellipse ellipse, double x, double y)
        {
            bool shouldTrackFillChange = !double.IsNaN(x) || !double.IsNaN(y);
            SetShouldTrackFillChange(ellipse,  shouldTrackFillChange);
        }

        private static void Ellipse_FillChanged(object sender, RadRoutedEventArgs e)
        {
            Ellipse ellipse = (Ellipse)sender;
            SetRadialFill(ellipse, GetRadialGradientOriginAndCenterX(ellipse), GetRadialGradientOriginAndCenterY(ellipse));
        }

        private class CameraInfo
        {
            internal DispatcherTimer initialAnimationTimer;
            internal DateTime initialAnimationDateTime;

            internal Point mouseDownPosition;
            internal Point mouseDownOffset;
            internal bool isMouseCaptured;
        }
    }
}
