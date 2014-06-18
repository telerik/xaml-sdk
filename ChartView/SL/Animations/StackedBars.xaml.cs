using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Animations
{
    public partial class StackedBars : UserControl
    {
        public StackedBars()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (var series in this.chart.Series)
            {
                ChartAnimationUtilities.SetCartesianAnimation(series, CartesianAnimation.DropWithDelay);
            }
            ChartAnimationUtilities.DispatchRunAnimations(this.chart);
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            foreach (var series in this.chart.Series)
            {
                ChartAnimationUtilities.SetCartesianAnimation(series, CartesianAnimation.StackedBars);
            }
            ChartAnimationUtilities.DispatchRunAnimations(this.chart);
        }
    }
}
