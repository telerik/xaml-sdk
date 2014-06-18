using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.Charting;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ChartView;

namespace Animations
{
    public static class ChartAnimationUtilities
    {
        public static readonly DependencyProperty CartesianAnimationProperty = DependencyProperty.RegisterAttached(
            "CartesianAnimation",
            typeof(CartesianAnimation),
            typeof(ChartAnimationUtilities),
            new PropertyMetadata(CartesianAnimation.None, CartesianAnimationChanged));

        public static readonly DependencyProperty PieAnimationProperty = DependencyProperty.RegisterAttached(
            "PieAnimation",
            typeof(PieAnimation),
            typeof(ChartAnimationUtilities),
            new PropertyMetadata(PieAnimation.None, PieAnimationChanged));

        private static readonly DependencyProperty RunningAnimationsCountProperty = DependencyProperty.RegisterAttached(
            "RunningAnimationsCount",
            typeof(int),
            typeof(ChartAnimationUtilities),
            new PropertyMetadata(0));

        private static readonly DependencyProperty StartAngleProperty = DependencyProperty.RegisterAttached(
            "StartAngle",
            typeof(double),
            typeof(ChartAnimationUtilities),
            new PropertyMetadata(double.NaN, StartAngleChanged));

        private static readonly DependencyProperty SweepAngleProperty = DependencyProperty.RegisterAttached(
            "SweepAngle",
            typeof(double),
            typeof(ChartAnimationUtilities),
            new PropertyMetadata(double.NaN, SweepAngleChanged));

        private static readonly DependencyProperty SeriesScaleTransformXProperty = DependencyProperty.RegisterAttached(
            "SeriesScaleTransformX",
            typeof(double),
            typeof(ChartAnimationUtilities),
            new PropertyMetadata(double.NaN, SeriesScaleTransformXChanged));

        private static readonly DependencyProperty SeriesScaleTransformYProperty = DependencyProperty.RegisterAttached(
            "SeriesScaleTransformY",
            typeof(double),
            typeof(ChartAnimationUtilities),
            new PropertyMetadata(double.NaN, SeriesScaleTransformYChanged));

        private static readonly DependencyProperty BarScaleTransformXProperty = DependencyProperty.RegisterAttached(
            "BarScaleTransformX",
            typeof(double),
            typeof(ChartAnimationUtilities),
            new PropertyMetadata(double.NaN, BarScaleTransformXChanged));

        private static readonly DependencyProperty BarScaleTransformYProperty = DependencyProperty.RegisterAttached(
            "BarScaleTransformY",
            typeof(double),
            typeof(ChartAnimationUtilities),
            new PropertyMetadata(double.NaN, BarScaleTransformYChanged));

        private static readonly DependencyProperty SliceScaleTransformXYProperty = DependencyProperty.RegisterAttached(
            "SliceScaleTransformXY",
            typeof(double),
            typeof(ChartAnimationUtilities),
            new PropertyMetadata(double.NaN, SliceScaleTransformXYChanged));

        private static readonly DependencyProperty SeriesTranslateTransformYProperty = DependencyProperty.RegisterAttached(
            "SeriesTranslateTransformY",
            typeof(double),
            typeof(ChartAnimationUtilities),
            new PropertyMetadata(double.NaN, SeriesTranslateTransformYChanged));

        private const int DelayInMilliseconds = 1000;
        private const int PieDelayInMilliseconds = 300;
        private const int BarDelayInMilliseconds = 200;
        private static Duration AnimationDuration = new Duration(TimeSpan.FromMilliseconds(1500));
        private static Duration PieSliceAnimationDuration = new Duration(TimeSpan.FromMilliseconds(500));
        private static Duration BarAnimationDuration = new Duration(TimeSpan.FromMilliseconds(500));
        private static object locker = new object();

        public static CartesianAnimation GetCartesianAnimation(DependencyObject obj)
        {
            return (CartesianAnimation)obj.GetValue(CartesianAnimationProperty);
        }

        public static void SetCartesianAnimation(DependencyObject obj, CartesianAnimation value)
        {
            obj.SetValue(CartesianAnimationProperty, value);
        }

        public static PieAnimation GetPieAnimation(DependencyObject obj)
        {
            return (PieAnimation)obj.GetValue(PieAnimationProperty);
        }

        public static void SetPieAnimation(DependencyObject obj, PieAnimation value)
        {
            obj.SetValue(PieAnimationProperty, value);
        }

        public static void DispatchRunAnimations(RadChartBase chart)
        {
            IEnumerable<ChartSeries> series = null;

            RadCartesianChart cartesianChart = chart as RadCartesianChart;
            if (cartesianChart != null)
            {
                series = cartesianChart.Series;
            }

            RadPieChart pieChart = chart as RadPieChart;
            if (pieChart != null)
            {
                series = pieChart.Series;
            }

            if (series.Any(s => (int)s.GetValue(ChartAnimationUtilities.RunningAnimationsCountProperty) > 0))
            {
                return;
            }

            foreach (ChartSeries s in series)
            {
                DispatchRunSeriesAnimations(s);
            }
        }

        private static void CartesianAnimationChanged(DependencyObject target, DependencyPropertyChangedEventArgs args)
        {
            CartesianSeries series = (CartesianSeries)target;

            if ((CartesianAnimation)args.NewValue == CartesianAnimation.None)
            {
                series.Loaded -= ChartSeries_Loaded;
                series.DataBindingComplete -= ChartSeries_DataBindingComplete;
            }

            if ((CartesianAnimation)args.OldValue == CartesianAnimation.None)
            {
                series.Loaded += ChartSeries_Loaded;
                series.DataBindingComplete += ChartSeries_DataBindingComplete;
            }
        }

        private static void PieAnimationChanged(DependencyObject target, DependencyPropertyChangedEventArgs args)
        {
            PieSeries series = (PieSeries)target;

            if ((PieAnimation)args.NewValue == PieAnimation.None)
            {
                series.Loaded -= ChartSeries_Loaded;
                series.DataBindingComplete -= ChartSeries_DataBindingComplete;
            }

            if ((PieAnimation)args.OldValue == PieAnimation.None)
            {
                series.Loaded += ChartSeries_Loaded;
                series.DataBindingComplete += ChartSeries_DataBindingComplete;
            }
        }

        private static void StartAngleChanged(DependencyObject target, DependencyPropertyChangedEventArgs args)
        {
            PieSeries series = (PieSeries)target;
            double startAngle = (double)args.NewValue;
            if (double.IsNaN(startAngle) || series.AngleRange.StartAngle == startAngle)
            {
                return;
            }

            series.AngleRange = new AngleRange(startAngle, series.AngleRange.SweepAngle, series.AngleRange.SweepDirection);
        }

        private static void SweepAngleChanged(DependencyObject target, DependencyPropertyChangedEventArgs args)
        {
            PieSeries series = (PieSeries)target;
            double sweepAngle = (double)args.NewValue;
            if (double.IsNaN(sweepAngle) || series.AngleRange.SweepAngle == sweepAngle)
            {
                return;
            }

            series.AngleRange = new AngleRange(series.AngleRange.StartAngle, sweepAngle, series.AngleRange.SweepDirection);
        }

        private static void SeriesScaleTransformXChanged(DependencyObject target, DependencyPropertyChangedEventArgs args)
        {
            ChartSeries series = (ChartSeries)target;
            double scaleX = (double)args.NewValue;

            if (!double.IsNaN(scaleX))
            {
                ScaleTransform transform = (ScaleTransform)series.RenderTransform;
                transform.ScaleX = scaleX;
            }
        }

        private static void SeriesScaleTransformYChanged(DependencyObject target, DependencyPropertyChangedEventArgs args)
        {
            ChartSeries series = (ChartSeries)target;
            double scaleY = (double)args.NewValue;

            if (!double.IsNaN(scaleY))
            {
                ScaleTransform transform = (ScaleTransform)series.RenderTransform;
                transform.ScaleY = (double)args.NewValue;
            }
        }

        private static void BarScaleTransformXChanged(DependencyObject target, DependencyPropertyChangedEventArgs args)
        {
            FrameworkElement bar = (FrameworkElement)target;
            double scaleX = (double)args.NewValue;

            if (!double.IsNaN(scaleX))
            {
                ScaleTransform transform = (ScaleTransform)bar.RenderTransform;
                transform.ScaleX = scaleX;
            }
        }

        private static void BarScaleTransformYChanged(DependencyObject target, DependencyPropertyChangedEventArgs args)
        {
            FrameworkElement bar = (FrameworkElement)target;
            double scaleY = (double)args.NewValue;

            if (!double.IsNaN(scaleY))
            {
                ScaleTransform transform = (ScaleTransform)bar.RenderTransform;
                transform.ScaleY = (double)args.NewValue;
            }
        }

        private static void SliceScaleTransformXYChanged(DependencyObject target, DependencyPropertyChangedEventArgs args)
        {
            Path slice = (Path)target;
            double scale = (double)args.NewValue;

            if (!double.IsNaN(scale))
            {
                ScaleTransform transform = (ScaleTransform)slice.RenderTransform;
                transform.ScaleX = scale;
                transform.ScaleY = scale;
            }
        }

        private static void SeriesTranslateTransformYChanged(DependencyObject target, DependencyPropertyChangedEventArgs args)
        {
            ChartSeries series = (ChartSeries)target;
            double transformY = (double)args.NewValue;

            if (!double.IsNaN(transformY))
            {
                TranslateTransform transform = (TranslateTransform)series.RenderTransform;
                transform.Y = (double)args.NewValue;
            }
        }

        private static void ChartSeries_Loaded(object sender, RoutedEventArgs e)
        {
            RunOrDispatchAnimations((ChartSeries)sender);
        }

        private static void ChartSeries_DataBindingComplete(object sender, EventArgs e)
        {
            DispatchRunSeriesAnimations((ChartSeries)sender);
        }

        private static void RunOrDispatchAnimations(ChartSeries series)
        {
            bool started = TryRunSeriesAnimation(series);
            if (!started)
            {
                DispatchRunSeriesAnimations(series);
            }
        }

        private static void DispatchRunSeriesAnimations(ChartSeries series)
        {
            series.Dispatcher.BeginInvoke((Action)(() => TryRunSeriesAnimation(series)));
        }

        private static bool HasDataPointsInPlotRange(ChartSeries series)
        {
            IList<DataPoint> dataPoints = GetDataPoints(series);

            foreach (DataPoint dp in dataPoints)
            {
                if (dp.IsInPlotRange)
                {
                    return true;
                }
            }

            return false;
        }

        private static bool TryRunSeriesAnimation(ChartSeries series)
        {
            if (!HasDataPointsInPlotRange(series))
            {
                return false;
            }

            int count = (int)series.GetValue(ChartAnimationUtilities.RunningAnimationsCountProperty);
            if (count > 0)
            {
                return false;
            }

            bool started = false;

            CartesianSeries cartesianSeries = series as CartesianSeries;
            if (cartesianSeries != null)
            {
                CartesianAnimation animation = GetCartesianAnimation(cartesianSeries);
                if (animation == CartesianAnimation.Drop || animation == CartesianAnimation.DropWithDelay)
                {
                    bool useDelay = animation == CartesianAnimation.DropWithDelay;
                    started |= TryRunDropAnimtation(cartesianSeries, useDelay);
                }
                if (animation == CartesianAnimation.Rise || animation == CartesianAnimation.RiseWithDelay)
                {
                    bool useDelay = animation == CartesianAnimation.RiseWithDelay;
                    started |= TryRunRiseAnimtation(cartesianSeries, useDelay);
                }
                if (animation == CartesianAnimation.Stretch)
                {
                    started |= TryRunStretchAnimtation(cartesianSeries);
                }
                if (animation == CartesianAnimation.StackedBars)
                {
                    started |= TryRunStackedBarsAnimtation(cartesianSeries);
                }
            }

            PieSeries pieSeries = series as PieSeries;
            if (pieSeries != null)
            {
                PieAnimation animation = GetPieAnimation(pieSeries);
                if (animation.HasFlag(PieAnimation.RadiusFactor))
                {
                    started |= TryRunRadiusFactorAnimtation(pieSeries);
                }
                if (animation.HasFlag(PieAnimation.Slice))
                {
                    started |= TryRunSliceAnimtation(pieSeries);
                }
                if (animation.HasFlag(PieAnimation.StartAngle))
                {
                    started |= TryRunStartAngleAnimtation(pieSeries);
                }
                if (animation.HasFlag(PieAnimation.SweepAngle))
                {
                    started |= TryRunSweepAngleAnimtation(pieSeries);
                }
            }

            return started;
        }

        private static bool TryRunRadiusFactorAnimtation(PieSeries series)
        {
            DoubleAnimation animation = new DoubleAnimation();
            animation.From = 0.0;
            animation.Duration = AnimationDuration;
            animation.EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseOut, };

            Storyboard.SetTargetProperty(animation, new PropertyPath(PieSeries.RadiusFactorProperty));
            Storyboard.SetTarget(animation, series);
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(animation);

            return Run(storyboard, series);
        }

        private static bool TryRunSliceAnimtation(PieSeries series)
        {
            Canvas renderSurface = Telerik.Windows.Controls.ChildrenOfTypeExtensions.FindChildByType<Canvas>(series);
            List<Path> slices = new List<Path>();
            foreach (UIElement uiElement in renderSurface.Children)
            {
                Path slice = uiElement as Path;
                if (slice != null && slice.DataContext is PieDataPoint)
                {
                    slices.Add(slice);
                }
            }

            Storyboard storyboard = new Storyboard();
            Point center = new Point(series.Chart.ActualWidth / 2, series.Chart.ActualHeight / 2);
            TimeSpan? beginTime = null;
            for (int i = 0; i < slices.Count; i++)
            {
                var animation = BuildSliceAnimation(slices[i], beginTime, PieSliceAnimationDuration, center, series);
                storyboard.Children.Add(animation);
                beginTime = new TimeSpan(0, 0, 0, 0, PieDelayInMilliseconds * (i + 1) / slices.Count);
            }

            bool showLabels = series.ShowLabels;
            series.ShowLabels = false;
            Action completed = () => series.ShowLabels = showLabels;
            bool started = Run(storyboard, series, completed);
            if (!started)
            {
                completed();
            }
            return started;
        }

        private static bool TryRunStartAngleAnimtation(PieSeries series)
        {
            double startAngle = (double)series.GetAnimationBaseValue(ChartAnimationUtilities.StartAngleProperty);
            if (double.IsNaN(startAngle))
            {
                series.SetValue(ChartAnimationUtilities.StartAngleProperty, series.AngleRange.StartAngle);
                startAngle = series.AngleRange.SweepAngle;
            }

            DoubleAnimation animation = new DoubleAnimation();
            animation.From = startAngle - 90;
            animation.To = startAngle;
            animation.Duration = AnimationDuration;
            animation.EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseOut, };

            Storyboard.SetTargetProperty(animation, new PropertyPath(ChartAnimationUtilities.StartAngleProperty));
            Storyboard.SetTarget(animation, series);
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(animation);
            return Run(storyboard, series);
        }

        private static bool TryRunSweepAngleAnimtation(PieSeries series)
        {
            double sweepAngle = (double)series.GetAnimationBaseValue(ChartAnimationUtilities.SweepAngleProperty);
            if (double.IsNaN(sweepAngle))
            {
                series.SetValue(ChartAnimationUtilities.SweepAngleProperty, series.AngleRange.SweepAngle);
                sweepAngle = series.AngleRange.SweepAngle;
            }

            DoubleAnimation animation = new DoubleAnimation();
            animation.From = 0.0;
            animation.To = sweepAngle;
            animation.Duration = AnimationDuration;
            animation.EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseOut, };

            Storyboard.SetTargetProperty(animation, new PropertyPath(ChartAnimationUtilities.SweepAngleProperty));
            Storyboard.SetTarget(animation, series);
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(animation);
            return Run(storyboard, series);
        }

        private static bool TryRunRiseAnimtation(CartesianSeries series, bool useDelay)
        {
            RadRect plotAreClip = series.Chart.PlotAreaClip;

            bool isHorizontalBar = !IsSeriesHorizontal(series);
            bool isInverse = IsNumericalAxisInverse(series);

            double centerX = 0;
            double centerY = 0;
            if (isHorizontalBar)
            {
                centerX = isInverse ? plotAreClip.Right : plotAreClip.X;
            }
            else
            {
                centerY = isInverse ? plotAreClip.Y : plotAreClip.Bottom;
            }

            var scaleTransform = new ScaleTransform();
            scaleTransform.ScaleX = isHorizontalBar ? 0 : 1;
            scaleTransform.ScaleY = isHorizontalBar ? 1 : 0;
            scaleTransform.CenterX = centerX;
            scaleTransform.CenterY = centerY;

            series.RenderTransform = scaleTransform;

            TimeSpan? beginTime = useDelay ? CalculateBeginTime(series) : null;

            DoubleAnimation animation = new DoubleAnimation();
            animation.From = 0;
            animation.To = 1;
            animation.Duration = AnimationDuration;
            if (beginTime != null)
            {
                animation.EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseOut, };
            }
            else
            {
                animation.EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseOut, };
            }

            Storyboard.SetTarget(animation, series);
            if (isHorizontalBar)
            {
                Storyboard.SetTargetProperty(animation, new PropertyPath(ChartAnimationUtilities.SeriesScaleTransformXProperty));
            }
            else
            {
                Storyboard.SetTargetProperty(animation, new PropertyPath(ChartAnimationUtilities.SeriesScaleTransformYProperty));
            }

            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(animation);
            if (beginTime != null)
            {
                storyboard.BeginTime = beginTime;
            }
            return Run(storyboard, series);
        }

        private static bool TryRunStretchAnimtation(CartesianSeries series)
        {
            RadRect plotAreClip = series.Chart.PlotAreaClip;

            bool isHorizontal = IsSeriesHorizontal(series);
            ScaleTransform transform = new ScaleTransform();
            transform.ScaleX = isHorizontal ? 1 : 0;
            transform.ScaleY = isHorizontal ? 0 : 1;
            transform.CenterX = isHorizontal ? 0 : CalculateHorizontalSeriesMiddle(series);
            transform.CenterY = isHorizontal ? CalculateVerticalSeriesMiddle(series) : 0;

            if (!IsValidNumber(transform.CenterX) || !IsValidNumber(transform.CenterY))
            {
                return false;
            }

            series.RenderTransform = transform;

            DoubleAnimation animation = new DoubleAnimation();
            animation.From = 0;
            animation.To = 1;
            animation.Duration = AnimationDuration;
            animation.EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseOut, };
            DependencyProperty prop = isHorizontal ? ChartAnimationUtilities.SeriesScaleTransformYProperty : ChartAnimationUtilities.SeriesScaleTransformXProperty;
            Storyboard.SetTargetProperty(animation, new PropertyPath(prop));
            Storyboard.SetTarget(animation, series);

            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(animation);
            return Run(storyboard, series);
        }

        private static bool TryRunDropAnimtation(CartesianSeries series, bool useDelay)
        {
            IList<DataPoint> dataPoints = GetDataPoints(series);

            bool isInverse = IsNumericalAxisInverse(series);

            double offsetY = isInverse ? double.PositiveInfinity : double.NegativeInfinity;
            foreach (DataPoint dp in dataPoints)
            {
                if (isInverse && dp.IsInPlotRange && dp.LayoutSlot.Center.Y < offsetY)
                {
                    offsetY = dp.LayoutSlot.Center.Y;
                }
                else if (!isInverse && dp.IsInPlotRange && offsetY < dp.LayoutSlot.Center.Y)
                {
                    offsetY = dp.LayoutSlot.Center.Y;
                }
            }

            if (!IsValidNumber(offsetY))
            {
                return false;
            }

            RadRect plotAreClip = series.Chart.PlotAreaClip;
            offsetY = (isInverse ? 1 : -1) * (plotAreClip.Height / 2);
            TranslateTransform transform = new TranslateTransform();
            transform.Y = offsetY;
            series.RenderTransform = transform;

            TimeSpan? beginTime = useDelay ? CalculateBeginTime(series) : null;

            series.Opacity = 0;

            DoubleAnimation transformAnimation = new DoubleAnimation();
            transformAnimation.From = offsetY;
            transformAnimation.To = 0;
            transformAnimation.Duration = AnimationDuration;
            transformAnimation.EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseOut, };
            Storyboard.SetTargetProperty(transformAnimation, new PropertyPath(ChartAnimationUtilities.SeriesTranslateTransformYProperty));
            Storyboard.SetTarget(transformAnimation, series);

            DoubleAnimation opacityAnimation = new DoubleAnimation();
            opacityAnimation.From = 0;
            opacityAnimation.To = 1;
            opacityAnimation.Duration = AnimationDuration;
            opacityAnimation.EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseOut, };
            Storyboard.SetTargetProperty(opacityAnimation, new PropertyPath(ChartSeries.OpacityProperty));
            Storyboard.SetTarget(opacityAnimation, series);

            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(transformAnimation);
            storyboard.Children.Add(opacityAnimation);

            if (beginTime != null)
            {
                storyboard.BeginTime = beginTime;
            }

            Action completed = () => series.Opacity = 1;
            bool started = Run(storyboard, series, completed);
            if (!started)
            {
                completed();
            }
            return started;
        }

        private static bool TryRunStackedBarsAnimtation(CartesianSeries series)
        {
            Canvas renderSurface = Telerik.Windows.Controls.ChildrenOfTypeExtensions.FindChildByType<Canvas>(series);
            List<FrameworkElement> bars = new List<FrameworkElement>();
            foreach (FrameworkElement uiElement in renderSurface.Children)
            {
                Border bar = uiElement as Border;
                ContentPresenter cp = uiElement as ContentPresenter;
                if ((bar != null && (bar.DataContext is CategoricalDataPoint)) ||
                    (cp != null && cp.Content is CategoricalDataPoint))
                {
                    bars.Add(uiElement);
                }
            }

            Storyboard storyboard = new Storyboard();
            RadCartesianChart chart = (RadCartesianChart)series.Chart;
            int initialDelay = (int)(BarAnimationDuration.TimeSpan.Milliseconds * chart.Series.IndexOf(series));
            TimeSpan? beginTime = TimeSpan.FromMilliseconds(initialDelay);
            for (int i = 0; i < bars.Count; i++)
            {
                var animation = BuildStackedBarAnimation(bars[i], beginTime, BarAnimationDuration, series);
                storyboard.Children.Add(animation);
                beginTime = new TimeSpan(0, 0, 0, 0, initialDelay + (BarDelayInMilliseconds * (i + 1)));
            }

            return Run(storyboard, series);
        }

        private static bool Run(Storyboard storyboard, ChartSeries series, Action completed = null)
        {
            if (storyboard.Children.Count == 0)
            {
                return false;
            }

            storyboard.Completed += (s, e) =>
            {
                storyboard.Stop();

                lock (locker)
                {
                    int count = (int)series.GetValue(ChartAnimationUtilities.RunningAnimationsCountProperty);
                    count--;
                    series.SetValue(ChartAnimationUtilities.RunningAnimationsCountProperty, count);
                }

                if (completed != null)
                {
                    completed();
                }
            };

            storyboard.Begin();
            lock (locker)
            {
                int count = (int)series.GetValue(ChartAnimationUtilities.RunningAnimationsCountProperty);
                count++;
                series.SetValue(ChartAnimationUtilities.RunningAnimationsCountProperty, count);
            }

            return true;
        }

        private static DoubleAnimation BuildSliceAnimation(Path path, TimeSpan? beginTime, Duration duration, Point center, PieSeries series)
        {
            var scaleTransform = new ScaleTransform();
            scaleTransform.ScaleX = 0;
            scaleTransform.ScaleY = 0;
            scaleTransform.CenterX = center.X;
            scaleTransform.CenterY = center.Y;

            path.RenderTransform = scaleTransform;

            DoubleAnimation animation = new DoubleAnimation();
            animation.From = 0;
            animation.To = 1;
            animation.Duration = duration;
            animation.EasingFunction = new BackEase() { EasingMode = EasingMode.EaseOut, Amplitude = 0.4 };
            if (beginTime != null)
            {
                animation.BeginTime = beginTime;
            }

            Storyboard.SetTarget(animation, path);
            Storyboard.SetTargetProperty(animation, new PropertyPath(ChartAnimationUtilities.SliceScaleTransformXYProperty));
            return animation;
        }

        private static DoubleAnimation BuildStackedBarAnimation(FrameworkElement bar, TimeSpan? beginTime, Duration duration, CartesianSeries series)
        {
            bool isHorizontalBar = !IsSeriesHorizontal(series);
            bool isInverse = IsNumericalAxisInverse(series);

            double centerX = 0;
            double centerY = 0;
            DataPoint dp = bar.DataContext as DataPoint;
            if (dp == null)
            {
                dp = (DataPoint)((bar as ContentPresenter).Content);
            }

            if (isHorizontalBar)
            {
                centerX = isInverse ? dp.LayoutSlot.Width : 0;
            }
            else
            {
                centerY = isInverse ? 0 : dp.LayoutSlot.Height;
            }

            var scaleTransform = new ScaleTransform();
            scaleTransform.ScaleX = isHorizontalBar ? 0 : 1;
            scaleTransform.ScaleY = isHorizontalBar ? 1 : 0;
            scaleTransform.CenterX = centerX;
            scaleTransform.CenterY = centerY;

            bar.RenderTransform = scaleTransform;

            DoubleAnimation animation = new DoubleAnimation();
            animation.From = 0;
            animation.To = 1;
            animation.Duration = duration;
            if (beginTime != null)
            {
                animation.BeginTime = beginTime;
            }

            Storyboard.SetTarget(animation, bar);
            if (isHorizontalBar)
            {
                Storyboard.SetTargetProperty(animation, new PropertyPath(ChartAnimationUtilities.BarScaleTransformXProperty));
            }
            else
            {
                Storyboard.SetTargetProperty(animation, new PropertyPath(ChartAnimationUtilities.BarScaleTransformYProperty));
            }

            return animation;
        }

        private static TimeSpan? CalculateBeginTime(CartesianSeries series)
        {
            RadCartesianChart chart = (RadCartesianChart)series.Chart;

            if (chart.Series.Count == 1)
            {
                return null;
            }

            int delay = DelayInMilliseconds * chart.Series.IndexOf(series) / chart.Series.Count;
            return new TimeSpan(0, 0, 0, 0, delay);
        }

        private static IList<DataPoint> GetDataPoints(ChartSeries series)
        {
            PieSeries pieSeries = series as PieSeries;
            if (pieSeries != null)
            {
                return (IList<DataPoint>)pieSeries.DataPoints;
            }

            CategoricalSeries categoricalSeries = series as CategoricalSeries;
            if (categoricalSeries != null)
            {
                return categoricalSeries.DataPoints;
            }

            RangeSeries rangeSeries = series as RangeSeries;
            if (rangeSeries != null)
            {
                return rangeSeries.DataPoints;
            }

            OhlcSeries ohlcSeries = series as OhlcSeries;
            if (ohlcSeries != null)
            {
                return ohlcSeries.DataPoints;
            }

            ScatterPointSeries scatterPointSeries = (ScatterPointSeries)series;
            return scatterPointSeries.DataPoints;
        }

        private static bool IsNumericalAxisInverse(CartesianSeries series)
        {
            NumericalAxis axis = series.VerticalAxis as NumericalAxis;
            if (axis != null)
            {
                return axis.IsInverse;
            }

            axis = ((RadCartesianChart)series.Chart).VerticalAxis as NumericalAxis;
            if (axis != null)
            {
                return axis.IsInverse;
            }

            axis = series.HorizontalAxis as NumericalAxis;
            if (axis != null)
            {
                return axis.IsInverse;
            }

            axis = ((RadCartesianChart)series.Chart).HorizontalAxis as NumericalAxis;
            if (axis != null)
            {
                return axis.IsInverse;
            }

            return false;
        }

        private static bool IsSeriesHorizontal(CartesianSeries series)
        {
            NumericalAxis axis = series.VerticalAxis as NumericalAxis;
            if (axis != null)
            {
                return true;
            }

            axis = ((RadCartesianChart)series.Chart).VerticalAxis as NumericalAxis;
            if (axis != null)
            {
                return true;
            }

            return false;
        }

        private static double CalculateHorizontalSeriesMiddle(CartesianSeries series)
        {
            return CalculateSeriesMiddle(series, dp => dp.LayoutSlot.Center.X);
        }

        private static double CalculateVerticalSeriesMiddle(CartesianSeries series)
        {
            return CalculateSeriesMiddle(series, dp => dp.LayoutSlot.Center.Y);
        }

        private static double CalculateSeriesMiddle(CartesianSeries series, Func<DataPoint, double> position)
        {
            double min = double.PositiveInfinity;
            double max = double.NegativeInfinity;

            IList<DataPoint> dataPoints = GetDataPoints(series);
            foreach (DataPoint dp in dataPoints)
            {
                if (dp.IsInPlotRange)
                {
                    double pos = position(dp);
                    if (pos < min)
                    {
                        min = pos;
                    }
                    if (max < pos)
                    {
                        max = pos;
                    }
                }
            }

            return (min + max) / 2;
        }

        private static bool IsValidNumber(double value)
        {
            return double.MinValue <= value && value <= double.MaxValue;
        }
    }
}
