using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace DragToSelect
{
    public partial class MainPage : UserControl
    {
        public MainPage()
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
    }
}
