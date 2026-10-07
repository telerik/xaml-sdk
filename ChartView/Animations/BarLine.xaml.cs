using System.Windows;
using System.Windows.Controls;

namespace Animations
{
    /// <summary>
    /// Interaction logic for BarLine.xaml
    /// </summary>
    public partial class BarLine : UserControl
    {
        public BarLine()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ChartAnimationUtilities.DispatchRunAnimations(this.chart);
        }
    }
}
