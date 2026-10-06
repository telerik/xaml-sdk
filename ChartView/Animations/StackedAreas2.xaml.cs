using System.Windows;
using System.Windows.Controls;

namespace Animations
{
    /// <summary>
    /// Interaction logic for StackedAreas2.xaml
    /// </summary>
    public partial class StackedAreas2 : UserControl
    {
        public StackedAreas2()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ChartAnimationUtilities.DispatchRunAnimations(this.chart);
        }
    }
}
