using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace DenyDropVisualFeedback.ViewModels
{
    public class League
    {
        public League(string name)
        {
            this.Name = name;
            this.Divisions = new ObservableCollection<Division>();
        }
        public string Name
        {
            get;
            set;
        }
        public ObservableCollection<Division> Divisions
        {
            get;
            set;
        }
    }
}
