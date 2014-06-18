using System.Windows;
using System.Windows.Controls;

namespace Animations
{
    /// <summary>
    /// Interaction logic for StackedBars.xaml
    /// </summary>
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
