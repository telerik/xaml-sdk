using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Telerik.Charting;
using Telerik.Windows.Controls.ChartView;

namespace AxisLikeAnnotations
{
    public partial class CategoricalAxisExample : UserControl
    {
        public CategoricalAxisExample()
        {
            InitializeComponent();
            this.DataContext = GetData();
            this.horizontalAxis.ActualRangeChanged += HorizontalAxis_ActualRangeChanged;            
            this.chart.SizeChanged += Chart_SizeChanged;
        }

        private void Chart_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() => { 
                UpdateAxis();
            }));
        }

        private void HorizontalAxis_ActualRangeChanged(object sender, NumericalRangeChangedEventArgs e)
        {
            UpdateAxis();
        }

        private void UpdateAxis()
        {
            ClearCustomAxis();            

            var line = CreateLine();
            this.chart.Annotations.Add(line);

            if (this.verticalAxis.PlotMode == AxisPlotMode.OnTicks)
            {
                GenerateLabelsOnTicks();
            }
            else
            {
                GenerateLabelsBetweenTicks();
            }
        }
       
        private void GenerateLabelsBetweenTicks()
        {
            var count = this.verticalAxis.Categories.Count();
            var slotSize = this.chart.PlotAreaClip.Height / count;
            for (int i = 0; i < count; i++)
            {
                var category = this.verticalAxis.Categories.ElementAt(i);
                var tick = CreateTick(category, -slotSize);
                this.chart.Annotations.Add(tick);
            }
        }

        private void GenerateLabelsOnTicks()
        {            
            foreach (var category in this.verticalAxis.Categories)
            {
                var tick = CreateTick(category);
                this.chart.Annotations.Add(tick);
            }
        }

        private CartesianCustomAnnotation CreateTick(ChartAxisCategoryInfo item, double verticalOffset = 0)
        {
            var tick = new CartesianCustomAnnotation();
            tick.Content = CreateTickVisual(item.Category, verticalOffset);
            tick.VerticalValue = item.Category;
            tick.HorizontalValue = GetAxisCenter();
            tick.HorizontalAlignment = HorizontalAlignment.Left;
            tick.VerticalAlignment = VerticalAlignment.Center;
            tick.Tag = "CustomAxisElement";
            return tick;
        }

        private UIElement CreateTickVisual(object category, double verticalOffset = 0)
        {   
            var tickVisual = new Rectangle() { Fill = Brushes.Black, Width = 5, Height = 1, Margin = new Thickness(3, verticalOffset, 0, 0) };
            var label = new TextBlock() { Text = category.ToString() };
            var panel = new StackPanel() { Orientation = Orientation.Horizontal };
            panel.Children.Add(label);
            panel.Children.Add(tickVisual);            
            return panel;
        }        

        public CartesianGridLineAnnotation CreateLine()
        {
            var annotation = new CartesianGridLineAnnotation();
            annotation.Stroke = Brushes.Black;
            annotation.Axis = this.horizontalAxis;
            annotation.Value = GetAxisCenter();
            annotation.Tag = "CustomAxisElement";
            return annotation;
        }

        private double GetAxisCenter()
        {
            var min = this.horizontalAxis.ActualRange.Minimum;
            var max = this.horizontalAxis.ActualRange.Maximum;
            var delta = max - min;
            var center = min + (delta / 2);
            return center;
        }

        private void ClearCustomAxis()
        {
            var axisElements = this.chart.Annotations.Where(x => x.Tag != null && x.Tag.Equals("CustomAxisElement")).ToList();
            foreach (var item in axisElements)
            {
                this.chart.Annotations.Remove(item);
            }
        }

        public ObservableCollection<PlotInfo> GetData()
        {
            var data = new ObservableCollection<PlotInfo>();
            for (int i = 0; i < 10; i++)
            {
                var value = i % 2 == 0 ? 
                    PlotInfo.RandomNumberGenerator.Next(100, 300) : 
                    PlotInfo.RandomNumberGenerator.Next(-300, -100);
                data.Add(new PlotInfo() { Value = value, Category = "C" + i });
            }
            return data;
        }
    }
}
