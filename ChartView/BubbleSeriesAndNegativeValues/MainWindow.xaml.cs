using System.Collections.Generic;
using System.Windows;

namespace BubbleSeriesAndNegativeValues
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
                new PlotInfo { XCat = "x0", YVal = 690, Size = 53000, },
                new PlotInfo { XCat = "x1", YVal = 390, Size = -25000, },
                new PlotInfo { XCat = "x2", YVal = 1200, Size = 75000, },
                new PlotInfo { XCat = "x3", YVal = 470, Size = 66000, },
                new PlotInfo { XCat = "x4", YVal = 570, Size = -13000, },
                new PlotInfo { XCat = "x5", YVal = 170, Size = 33000, },
                new PlotInfo { XCat = "x6", YVal = 220, Size = 13000, },
                new PlotInfo { XCat = "x7", YVal = 420, Size = -23000, },
            };
        }
    }
}
