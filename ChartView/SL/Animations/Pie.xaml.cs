using System.Windows;
using System.Windows.Controls;

namespace Animations
{
    public partial class Pie : UserControl
    {
        public Pie()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ChartAnimationUtilities.DispatchRunAnimations(this.chart);
        }
    }
}
