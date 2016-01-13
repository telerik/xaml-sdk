using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace ExportMultipleGridViewsToOneDocument
{
    public class Club : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string name;
        private DateTime established;
        private int stadiumCapacity;

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

        internal static ObservableCollection<Club> GetClubs()
        {
            ObservableCollection<Club> clubs = new ObservableCollection<Club>();
            Club club;

            club = new Club("Liverpool", new DateTime(1892, 1, 1), 45362);
            clubs.Add(club);

            club = new Club("Manchester Utd.", new DateTime(1878, 1, 1), 76212);
            clubs.Add(club);

            club = new Club("Chelsea", new DateTime(1905, 1, 1), 42055);
            clubs.Add(club);

            club = new Club("Arsenal", new DateTime(1886, 1, 1), 60355);
            clubs.Add(club);

            return clubs;
        }
    }
}