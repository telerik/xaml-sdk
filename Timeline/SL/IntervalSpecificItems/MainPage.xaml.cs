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
using Telerik.Windows.Controls;

namespace IntervalSpecificItems
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void RadTimeline1_ItemIntervalChanged(object sender, Telerik.Windows.Controls.TimeBar.DrillEventArgs e)
        {
            RadTimeline timeline = sender as RadTimeline;

            (timeline.DataContext as TimelineViewModel).CurrentInterval = timeline.CurrentItemInterval;
        }
    }
}
