using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace GroupingAndFilteringWithTreeView
{
    public class Airline
    {
        public string Name { get; set; }

        public ObservableCollection<Segregation> Segregations { get; set; }

        public bool IsExpanded { get; set; }

        public Airline()
        {
            this.Segregations = new ObservableCollection<Segregation>();
            this.IsExpanded = false;
        }
    }
}