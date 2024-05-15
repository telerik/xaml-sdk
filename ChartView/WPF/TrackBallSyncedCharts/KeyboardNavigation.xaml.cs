using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;

namespace TrackBallSyncedCharts
{
    public partial class KeyboardNavigation : UserControl
    {
        public KeyboardNavigation()
        {
            InitializeComponent();

            this.KeyDown += this.KeyboardNavigation_KeyDown;

            this.DataContext = new List<PlotInfo> 
            { 
                new PlotInfo { Date = new DateTime(2013, 11, 21), YVal = 40, },
                new PlotInfo { Date = new DateTime(2013, 11, 22), YVal = 50, },
                new PlotInfo { Date = new DateTime(2013, 11, 23), YVal = 90, },
                new PlotInfo { Date = new DateTime(2013, 11, 24), YVal = 90, },
                new PlotInfo { Date = new DateTime(2013, 11, 25), YVal = 40, },
                new PlotInfo { Date = new DateTime(2013, 11, 26), YVal = 30, },
                new PlotInfo { Date = new DateTime(2013, 11, 27), YVal = 40, },
                new PlotInfo { Date = new DateTime(2013, 11, 28), YVal = 40, },
            };
        }

        private void KeyboardNavigation_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                ChartUtilities.MoveTrackBall(this.behav1, ChartUtilities.NavigateDirection.Left);
                e.Handled = true;
            }
            else if (e.Key == Key.Right)
            {
                ChartUtilities.MoveTrackBall(this.behav1, ChartUtilities.NavigateDirection.Right);
                e.Handled = true;
            }
        }
    }
}
