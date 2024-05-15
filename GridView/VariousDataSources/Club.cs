using System.Collections.ObjectModel;
using System.Collections.Generic;
using System;

namespace VariousDataSources
{
    public class Club
    {
        public Club(string name, DateTime established, int stadiumCapacity)
        {
            this.Name = name;
            this.Established = established;
            this.StadiumCapacity = stadiumCapacity;
        }
        public string Name
        {
            get;
            set;
        }
        public DateTime? Established
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
            clubs.Add(new Club("Liverpool", new DateTime(1892, 1, 1), 45362));
            clubs.Add(new Club("Manchester Utd.", new DateTime(1878, 1, 1), 76212));
            clubs.Add(new Club("Chelsea", new DateTime(1905, 1, 1), 42055));
            clubs.Add(new Club("Arsenal", new DateTime(1886, 1, 1), 60355));
            return clubs;
        }
    }
}
