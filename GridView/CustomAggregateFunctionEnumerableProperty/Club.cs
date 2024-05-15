using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace CustomAggregateFunctionEnumerableProperty
{
    /// <summary>
    /// A football club.
    /// </summary>
    public class Club : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string name;
        private DateTime established;
        private int stadiumCapacity;
        private ObservableCollection<Period> period;

        public ObservableCollection<Period> Period
        {
            get 
            {
                return this.period; 
            }
            set
            {
                this.period = value;
                this.OnPropertyChanged("Period");
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

        public Club(string name, DateTime established, int stadiumCapacity, ObservableCollection<Period> period)
        {
            this.name = name;
            this.established = established;
            this.stadiumCapacity = stadiumCapacity;
            this.period = period;
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

            // Liverpool
            club = new Club("Liverpool", new DateTime(1892, 1, 1), 45362, new ObservableCollection<Period> { new Period() { HalfSeason = HalfSeason.First, NumberOfGoals = 44 }, new Period() { HalfSeason = HalfSeason.Second, NumberOfGoals = 39 } });
            clubs.Add(club);

            // Manchester Utd.
            club = new Club("Manchester Utd.", new DateTime(1878, 1, 1), 76212, new ObservableCollection<Period> { new Period() { HalfSeason = HalfSeason.First, NumberOfGoals = 52 }, new Period() { HalfSeason = HalfSeason.Second, NumberOfGoals = 47 } });
            clubs.Add(club);

            // Chelsea
            club = new Club("Chelsea", new DateTime(1905, 1, 1), 42055, new ObservableCollection<Period> { new Period() { HalfSeason = HalfSeason.First, NumberOfGoals = 29 }, new Period() { HalfSeason = HalfSeason.Second, NumberOfGoals = 51 } });
            clubs.Add(club);

            // Arsenal
            club = new Club("Arsenal", new DateTime(1886, 1, 1), 60355, new ObservableCollection<Period> { new Period() { HalfSeason = HalfSeason.First, NumberOfGoals = 33 }, new Period() { HalfSeason = HalfSeason.Second, NumberOfGoals = 49 } });
            clubs.Add(club);

            return clubs;
        }
    }
}
