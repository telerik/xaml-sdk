using System;
using System.Collections.Generic;
using System.Windows;
using Telerik.Windows.Controls.ChartView;

namespace DragToSelect
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
                new PlotInfo { XCat = new DateTime(2014, 2, 3), YVal = 5, },
                new PlotInfo { XCat = new DateTime(2014, 2, 4), YVal = 15, },
                new PlotInfo { XCat = new DateTime(2014, 2, 5), YVal = 15, },
                new PlotInfo { XCat = new DateTime(2014, 2, 6), YVal = 10, },
                new PlotInfo { XCat = new DateTime(2014, 2, 7), YVal = 5, },
                new PlotInfo { XCat = new DateTime(2014, 2, 8), YVal = 10, },
                new PlotInfo { XCat = new DateTime(2014, 2, 9), YVal = 5, },
                new PlotInfo { XCat = new DateTime(2014, 2, 10), YVal = 15, },
                new PlotInfo { XCat = new DateTime(2014, 2, 11), YVal = 10, },
                new PlotInfo { XCat = new DateTime(2014, 2, 12), YVal = 5, },
            };
        }

        private void RadioButtonDragToZoom_Checked(object sender, RoutedEventArgs e)
        {
            this.panZoomBehavior.DragMode = ChartDragMode.Zoom;
            ChartUtilities.SetIsDragToSelectEnabled(this.chart1, false);
        }

        private void RadioButtonDragToSelect_Checked(object sender, RoutedEventArgs e)
        {
            this.panZoomBehavior.DragMode = ChartDragMode.None;
            ChartUtilities.SetIsDragToSelectEnabled(this.chart1, true);
        }
    }
}
