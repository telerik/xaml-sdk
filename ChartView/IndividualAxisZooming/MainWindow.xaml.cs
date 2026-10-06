using System;
using System.Collections.Generic;
using System.Windows;
using Telerik.Charting;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ChartView;

namespace IndividualAxisZooming
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new List<PlotInfo> 
            { 
                new PlotInfo { XCat = "x1", YVal1 = 10, YVal2 = 40, },
                new PlotInfo { XCat = "x2", YVal1 = 15, YVal2 = 25, },
                new PlotInfo { XCat = "x3", YVal1 = 5, YVal2 = 24, },
                new PlotInfo { XCat = "x4", YVal1 = 5, YVal2 = 37, },
                new PlotInfo { XCat = "x5", YVal1 = 15, YVal2 = 25, },
                new PlotInfo { XCat = "x6", YVal1 = 10, YVal2 = 35, },
                new PlotInfo { XCat = "x7", YVal1 = 10, YVal2 = 30, },
            };

            this.Loaded += this.MainWindow_Loaded;

            this.chart1.ZoomChanged += this.chart1_ZoomChanged;
            this.chart1.PanOffsetChanged += this.chart1_PanOffsetChanged;
            this.chart1.SizeChanged += this.chart1_SizeChanged;
        }

        private void chart1_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.UpdateSlidersMargin();
        }
        
        private void chart1_PanOffsetChanged(object sender, ChartPanOffsetChangedEventArgs e)
        {
            this.UpdateSlidersMargin();
        }

        private void chart1_ZoomChanged(object sender, ChartZoomChangedEventArgs e)
        {
            this.UpdateSlidersMargin();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.UpdateAxis(this.chart1.Series[0] as CategoricalSeries, this.LeftPanZoomBar.SelectionStart, this.LeftPanZoomBar.SelectionEnd);
            this.UpdateAxis(this.chart1.Series[1] as CategoricalSeries, this.RightPanZoomBar.SelectionStart, this.RightPanZoomBar.SelectionEnd);
            this.Dispatcher.BeginInvoke(((Action)(() => this.UpdateSlidersMargin())));
        }

        private void PanZoomBar_SelectionChanged(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            CategoricalSeries series = null;
            RadSlider slider = null;
            if (sender == this.LeftPanZoomBar)
            {
                slider = this.LeftPanZoomBar;
                series = this.chart1.Series[0] as CategoricalSeries;
            }
            if (sender == this.RightPanZoomBar)
            {
                slider = this.RightPanZoomBar;
                series = this.chart1.Series[1] as CategoricalSeries;
            }

            if (slider != null)
            {
                this.UpdateAxis(series, slider.SelectionStart, slider.SelectionEnd);
            }
        }

        private void UpdateAxis(CategoricalSeries series, double selectionStart, double selectionEnd)
        {
            if (series == null)
            {
                return;
            }

            LinearAxis axis = series.VerticalAxis as LinearAxis;
            if (axis == null)
            {
                return;
            }

            double min, max;
            this.GetMinAndMax(series.DataPoints, out min, out max);
            if (double.IsNaN(min) || double.IsNaN(max))
            {
                return;
            }

            axis.Minimum = min + ((max - min) * selectionStart);
            axis.Maximum = min + ((max - min) * selectionEnd);
        }

        private void GetMinAndMax(DataPointCollection<CategoricalDataPoint> dataPoints, out double min, out double max)
        {
            min = double.NaN;
            max = double.NaN;

            foreach (CategoricalDataPoint dp in dataPoints)
            {
                if (dp.Value.HasValue)
                {
                    if (double.IsNaN(min) || dp.Value.Value < min)
                    {
                        min = dp.Value.Value;
                    }
                    if (double.IsNaN(max) || max < dp.Value.Value)
                    {
                        max = dp.Value.Value;
                    }
                }
            }

            if (double.IsNaN(min) || double.IsNaN(max))
            {
                return;
            }

            double expansion = (max - min) / 10;
            min -= expansion;
            max += expansion;

            if (min >= 0 && 0.16 < (max - min) / max)
            {
                min = 0;
            }
        }

        private void UpdateSlidersMargin()
        {
            var plotArea = this.chart1.PlotAreaClip;

            double marginTop = plotArea.Y;
            double marginBottom = this.chart1.ActualHeight - plotArea.Bottom;

            if (plotArea.Y != this.LeftPanZoomBar.Margin.Top ||
                marginBottom != this.LeftPanZoomBar.Margin.Bottom)
            {
                this.LeftPanZoomBar.Margin = new Thickness(0, plotArea.Y, 0, marginBottom);
            }
            if (plotArea.Y != this.RightPanZoomBar.Margin.Top ||
                marginBottom != this.RightPanZoomBar.Margin.Bottom)
            {
                this.RightPanZoomBar.Margin = new Thickness(0, plotArea.Y, 0, marginBottom);
            }
        }
    }
}
