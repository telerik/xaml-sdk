using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;

namespace TimeBarOverChartView
{
    public partial class MainPage : UserControl
    {
        private double sliderActualHeight;

        public MainPage()
        {
            InitializeComponent();
            this.DataContext = this.GetData();

            this.Dispatcher.BeginInvoke(() =>
            {
                var slider = Telerik.Windows.Controls.ChildrenOfTypeExtensions.FindChildByType<RadSlider>(this.timeBar1);
                this.sliderActualHeight = slider.ActualHeight;
                slider.SelectionChanged += this.slider_SelectionChanged;
                slider.SizeChanged += this.slider_SizeChanged;
            });
        }

        private List<PlotInfo> GetData()
        {
            Random r = new Random();
            var data = new List<PlotInfo>();

            for (int i = 0; i < 100; i++)
            {
                data.Add(new PlotInfo { Date = new DateTime(2014, 10, 3).AddDays(i), Val = r.Next(0, 500), });
            }

            return data;
        }

        private void chart1_PlotAreaClipChanged(object sender, EventArgs e)
        {
            this.UpdateTimeBarMargin();
        }

        private void slider_SelectionChanged(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            var slider = (RadSlider)sender;
            double range = slider.Maximum - slider.Minimum;
            if (range != 0)
            {
                this.chart1.HorizontalZoomRangeStart = (slider.SelectionStart - slider.Minimum) / range;
                this.chart1.HorizontalZoomRangeEnd = (slider.SelectionEnd - slider.Minimum) / range;
            }
        }

        private void timeBarContent_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.UpdateChartMargin();
        }

        private void slider_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.sliderActualHeight = e.NewSize.Height;
            this.UpdateChartMargin();
        }

        private void UpdateChartMargin()
        {
            double topMargin = this.timeBar1.ActualHeight - (this.timeBarContent1.ActualHeight + this.sliderActualHeight);
            this.chart1.Margin = new Thickness(0, topMargin, 0, this.sliderActualHeight);
        }

        private void UpdateTimeBarMargin()
        {
            double verticalAxisWidth = this.chart1.PlotAreaClip.X + this.chart1.PanOffset.X;
            this.timeBar1.Margin = new Thickness(verticalAxisWidth, 0, 0, 0);
        }
    }
}