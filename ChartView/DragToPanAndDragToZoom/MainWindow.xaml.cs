using System.Collections.Generic;
using System.Windows;
using Telerik.Windows.Controls.ChartView;

namespace DragToPanAndDragToZoom
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
                new PlotInfo { XCat = "x1", YVal = 20, },
                new PlotInfo { XCat = "x2", YVal = 26, },
                new PlotInfo { XCat = "x3", YVal = 11, },
                new PlotInfo { XCat = "x4", YVal = 40, },
                new PlotInfo { XCat = "x5", YVal = 40, },
                new PlotInfo { XCat = "x6", YVal = 40, },
                new PlotInfo { XCat = "x7", YVal = 60, },
                new PlotInfo { XCat = "x8", YVal = 20, },
                new PlotInfo { XCat = "x9", YVal = 25, },
                new PlotInfo { XCat = "x10", YVal = 50, },
                new PlotInfo { XCat = "x11", YVal = 45, },
                new PlotInfo { XCat = "x12", YVal = 45, },
                new PlotInfo { XCat = "x13", YVal = 12, },
                new PlotInfo { XCat = "x14", YVal = 20, },
                new PlotInfo { XCat = "x15", YVal = 20, },
                new PlotInfo { XCat = "x16", YVal = 10, },
            };
        }
    }
}