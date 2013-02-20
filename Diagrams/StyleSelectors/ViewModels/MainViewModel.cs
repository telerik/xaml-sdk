using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StyleSelectors.ViewModels
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class MainViewModel
    {
        public GraphSource DiagramSource { get; set; }

        public MainViewModel()
        {
            this.DiagramSource = new GraphSource();
            this.DiagramSource.PopulateGraphSource();
        }
    }
}
