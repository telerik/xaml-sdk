using System.Windows;
using System.Windows.Controls;

namespace Animations
{
    /// <summary>
    /// Interaction logic for ClusteredBars.xaml
    /// </summary>
    public partial class ClusteredBars : UserControl
    {
        public ClusteredBars()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ChartAnimationUtilities.DispatchRunAnimations(this.chart);
        }
    }
}
