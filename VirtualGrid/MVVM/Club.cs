using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MVVM_VirtualGrid
{
    public class Club : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string name;
        private DateTime established;
        private int stadiumCapacity;
        private int primaryKey;

        public int PrimaryKey
        {
            get { return this.primaryKey; }
            set
            {
                this.primaryKey = value;
                this.OnPropertyChanged("PrimaryKey");
            }
        }
        public string Name
        {
            get { return this.name; }
            set
            {
                if (value != this.name)
                {
                    this.name = value;
                    this.OnPropertyChanged("Name");
                }
            }
        }

        public DateTime Established
        {
            get { return this.established; }
            set
            {
                if (value != this.established)
                {
                    this.established = value;
                    this.OnPropertyChanged("Established");
                }
            }
        }

        public int StadiumCapacity
        {
            get { return this.stadiumCapacity; }
            set
            {
                if (value != this.stadiumCapacity)
                {
                    this.stadiumCapacity = value;
                    this.OnPropertyChanged("StadiumCapacity");
                }
            }
        }

        public Club()
        {

        }

        public Club(string name, DateTime established, int stadiumCapacity)
        {
            this.name = name;
            this.established = established;
            this.stadiumCapacity = stadiumCapacity;
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, args);
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return this.Name;
        }

        public static ObservableCollection<Club> GetClubs()
        {
            ObservableCollection<Club> clubs = new ObservableCollection<Club>();
            Club club;
            int id = 0;

            // Liverpool
            club = new Club("Liverpool", new DateTime(1892, 1, 1), 45362) { PrimaryKey = id};
            clubs.Add(club);

            id++;
            // Manchester Utd.
            club = new Club("Manchester Utd.", new DateTime(1878, 1, 1), 76212) {PrimaryKey = id};
            clubs.Add(club);

            id++;
            // Chelsea
            club = new Club("Chelsea", new DateTime(1905, 1, 1), 42055) { PrimaryKey = id };
            clubs.Add(club);

            id++;
            // Arsenal
            club = new Club("Arsenal", new DateTime(1886, 1, 1), 60355) { PrimaryKey = id };
            clubs.Add(club);

            return clubs;
        }
    }
}
