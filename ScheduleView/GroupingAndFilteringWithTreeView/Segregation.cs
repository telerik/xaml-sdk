using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace GroupingAndFilteringWithTreeView
{
    public class Segregation
    {
        public string Name { get; set; }

        public ObservableCollection<Process> Processes { get; set; }

        public bool IsExpanded { get; set; }

        public Segregation()
        {
            this.Processes = new ObservableCollection<Process>();
            this.IsExpanded = false;
        }
    }
}