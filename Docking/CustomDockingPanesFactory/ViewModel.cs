using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Windows.Controls;

namespace CustomDockingPanesFactory
{
    public class ViewModel : ViewModelBase
    {
        public List<RadPane> Panes { get; set; }

        public ViewModel()
        {
            this.Panes = new List<RadPane>()
            {
                new RadPane() { Header = "Bottom Pane 1", Tag = "Bottom" },
                new RadPane() { Header = "Bottom Pane 2", Tag = "Bottom" },
                new RadPane() { Header = "Left Pane", Tag = "Left" }
            };
        }
    }
}
