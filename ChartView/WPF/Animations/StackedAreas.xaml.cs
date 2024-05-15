using System.Windows;
using System.Windows.Controls;

namespace Animations
{
    /// <summary>
    /// Interaction logic for StackedAreas.xaml
    /// </summary>
    public partial class StackedAreas : UserControl
    {
        public StackedAreas()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ChartAnimationUtilities.DispatchRunAnimations(this.chart);
        }
    }
}
