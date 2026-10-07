using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Animations
{
    /// <summary>
    /// Interaction logic for Pie.xaml
    /// </summary>
    public partial class Pie2 : UserControl
    {
        public Pie2()
        {
            InitializeComponent();

            this.DataContext = new List<PlotInfo> 
            { 
                new PlotInfo { YVal1 = 43, },
                new PlotInfo { YVal1 = 33, },
                new PlotInfo { YVal1 = 16, },
                new PlotInfo { YVal1 = 11, },
                new PlotInfo { YVal1 = 9, },
                new PlotInfo { YVal1 = 5, },
                new PlotInfo { YVal1 = 3, },
                new PlotInfo { YVal1 = 3, },
            };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ChartAnimationUtilities.DispatchRunAnimations(this.chart);
        }
    }
}
