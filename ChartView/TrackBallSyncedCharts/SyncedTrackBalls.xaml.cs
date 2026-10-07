using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Documents;

namespace TrackBallSyncedCharts
{
    public partial class SyncedTrackBalls : UserControl
    {
        public SyncedTrackBalls()
        {
            InitializeComponent();

            List<List<PlotInfo>> list = new List<List<PlotInfo>>();
            list.Add(new List<PlotInfo> 
            { 
                new PlotInfo { Date = new DateTime(2013, 11, 21), YVal = 40, },
                new PlotInfo { Date = new DateTime(2013, 11, 22), YVal = 50, },
                new PlotInfo { Date = new DateTime(2013, 11, 23), YVal = 90, },
                new PlotInfo { Date = new DateTime(2013, 11, 24), YVal = 90, },
                new PlotInfo { Date = new DateTime(2013, 11, 25), YVal = 40, },
                new PlotInfo { Date = new DateTime(2013, 11, 26), YVal = 30, },
                new PlotInfo { Date = new DateTime(2013, 11, 27), YVal = 40, },
                new PlotInfo { Date = new DateTime(2013, 11, 28), YVal = 40, },
            });
            list.Add(new List<PlotInfo> 
            { 
                new PlotInfo { Date = new DateTime(2013, 11, 23), YVal = 1100, },
                new PlotInfo { Date = new DateTime(2013, 11, 24), YVal = 1100, },
                new PlotInfo { Date = new DateTime(2013, 11, 25), YVal = 1200, },
                new PlotInfo { Date = new DateTime(2013, 11, 26), YVal = 1000, },
                new PlotInfo { Date = new DateTime(2013, 11, 27), YVal = 700, },
                new PlotInfo { Date = new DateTime(2013, 11, 28), YVal = 900, },
                new PlotInfo { Date = new DateTime(2013, 11, 29), YVal = 500, },
                new PlotInfo { Date = new DateTime(2013, 11, 30), YVal = 800, },
            });
            this.DataContext = list;
        }
    }
}
