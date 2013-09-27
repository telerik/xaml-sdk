using System;
using System.Linq;

namespace EnableOnlyDropInsideItem.ViewModels
{
    public class Team
    {
        public Team(string name)
        {
            this.Name = name;
        }
        public string Name
        {
            get;
            set;
        }
    }
}
