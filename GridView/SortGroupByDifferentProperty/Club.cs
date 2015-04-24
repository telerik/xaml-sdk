using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace SortGroupByDifferentProperty
{
    public class Club
    {
        public Club(string name, int stadiumCapacity)
        {
            this.Name = name;
            this.StadiumCapacity = stadiumCapacity;
        }
        public string Name
        {
            get;
            set;
        }

        public int StadiumCapacity
        {
            get;
            set;
        }
        public static IEnumerable<Club> GetClubs()
        {
            ObservableCollection<Club> clubs = new ObservableCollection<Club>();
            clubs.Add(new Club("Liverpool", 45362));
            clubs.Add(new Club("Manchester Utd.", 76212));
            clubs.Add(new Club("Chelsea", 42055));
            clubs.Add(new Club("Arsenal", 60355));
            return clubs;
        }
    }
}
