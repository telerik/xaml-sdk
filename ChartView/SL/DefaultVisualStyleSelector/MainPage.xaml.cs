using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace DefaultVisualStyleSelector
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();

            this.DataContext = new List<PlotInfo> 
            {
                new PlotInfo { Date = new DateTime(2009, 1, 1), YVal = 100, }, 
                new PlotInfo { Date = new DateTime(2010, 1, 1), YVal = 80, }, 
                new PlotInfo { Date = new DateTime(2011, 1, 1), YVal = 0, }, 
                new PlotInfo { Date = new DateTime(2012, 1, 1), YVal = -40, }, 
                new PlotInfo { Date = new DateTime(2013, 1, 1), YVal = -25, }, 
                new PlotInfo { Date = new DateTime(2014, 1, 1), YVal = 10, }, 
            };
        }
    }
}
