using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Telerik.Windows.Controls.ChartView;

namespace AxisLikeAnnotations
{
    public partial class NumericAxisExample : UserControl
    {
        public const string CustomAxisPositionKey = "CustomAxisPosition";
        public const string CustomAxisElementKey = "CustomAxisElement";

        public NumericAxisExample()
        {
            InitializeComponent();
            this.DataContext = GetData();
            this.chart.SizeChanged += Chart_SizeChanged;
        }

        private void Chart_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() => {
                UpdateAxis();
            }));
        }

        private void UpdateAxis()
        {
            ClearCustomAxis();

            var line = CreateLine();
            this.chart.Annotations.Add(line);

            var step = this.verticalAxis.ActualMajorStep;
            var min = this.verticalAxis.ActualRange.Minimum + step;
            var max = this.verticalAxis.ActualRange.Maximum;            
            for (double i = min; i <= max; i += step)
            {
                var tick = CreateTick(i);
                this.chart.Annotations.Add(tick);
            }
        }

        private CartesianCustomAnnotation CreateTick(double value)
        {
            var tick = new CartesianCustomAnnotation();
            tick.ClipToPlotArea = false;
            tick.Content = CreateTickVisual(value);            
            tick.VerticalValue = value;
            tick.HorizontalValue = CustomAxisPositionKey;
            tick.HorizontalAlignment = HorizontalAlignment.Left;
            tick.VerticalAlignment = VerticalAlignment.Center;
            tick.Tag = CustomAxisElementKey;
            return tick;
        }

        private UIElement CreateTickVisual(double value)
        {
            var tick = new Rectangle() { Fill = Brushes.Black, Width = 5, Height = 1, Margin = new Thickness(3, 0, 0, 0) };
            var label = new TextBlock() { Text = value.ToString() };
            var panel = new StackPanel() { Orientation = Orientation.Horizontal };
            panel.Children.Add(label);
            panel.Children.Add(tick);
            return panel;
        }

        public CartesianGridLineAnnotation CreateLine()
        {
            var annotation = new CartesianGridLineAnnotation();
            annotation.Stroke = Brushes.Black;
            annotation.Axis = this.horizontalAxis;
            annotation.Value = CustomAxisPositionKey;
            annotation.Tag = CustomAxisElementKey;
            return annotation;
        }

        private void ClearCustomAxis()
        {
            var axisElements = this.chart.Annotations.Where(x => x.Tag != null && x.Tag.Equals(CustomAxisElementKey)).ToList();
            foreach (var item in axisElements)
            {
                this.chart.Annotations.Remove(item);
            }
        }

        public ObservableCollection<PlotInfo> GetData()
        {
            var data = new ObservableCollection<PlotInfo>();
            for (int i = 1; i <= 5; i++)
            {
                var value = PlotInfo.RandomNumberGenerator.Next(100, 300);
                data.Add(new PlotInfo() { Value = value, Category = "A" + i });
            }

            data.Add(new PlotInfo() { Value = null, Category = CustomAxisPositionKey });

            for (int i = 1; i <= 5; i++)
            {
                var value = PlotInfo.RandomNumberGenerator.Next(100, 300);
                data.Add(new PlotInfo() { Value = value, Category = "B" + i });
            }
            return data;
        }
    }
}
