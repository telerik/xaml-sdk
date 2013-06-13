using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace DenyDropVisualFeedback.ViewModels
{
    public class Division
    {
        public Division(string name)
        {
            this.Name = name;
            this.Teams = new ObservableCollection<Team>();
        }
        public string Name
        {
            get;
            set;
        }
        public ObservableCollection<Team> Teams
        {
            get;
            set;
        }
    }
}
