using System.Windows;
using Telerik.Windows.Controls.ChartView;
using System.Collections.Generic;

namespace Animations
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
                new PlotInfo { XCat = "x1", YVal1 = 4, YVal2 = 7, YVal3 = 4, YVal4 = 5 },
                new PlotInfo { XCat = "x2", YVal1 = 6, YVal2 = 2, YVal3 = 3, YVal4 = 6 },
                new PlotInfo { XCat = "x3", YVal1 = 5, YVal2 = 7, YVal3 = 3, YVal4 = 4 },
                new PlotInfo { XCat = "x4", YVal1 = 6, YVal2 = 3, YVal3 = 2, YVal4 = 4 },
                new PlotInfo { XCat = "x5", YVal1 = 3, YVal2 = 4, YVal3 = 3, YVal4 = 2 },
            };
        }

        private void ButtonRunAll_Click(object sender, RoutedEventArgs e)
        {
            var charts = Telerik.Windows.Controls.ChildrenOfTypeExtensions.ChildrenOfType<RadChartBase>(this);

            foreach (var chart in charts)
            {
                ChartAnimationUtilities.DispatchRunAnimations(chart);
            }
        }
    }
}
