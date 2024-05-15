using System;
using System.Collections.ObjectModel;
using Telerik.Windows.Controls;

namespace BoundSelectColumn
{
    public class Club : ViewModelBase
    {
        private string name;
        private DateTime established;
        private int stadiumCapacity;
        private bool isSelected;

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

        public bool IsSelected
        {
            get { return this.isSelected; }
            set
            {
                if (value != this.isSelected)
                {
                    this.isSelected = value;
                    this.OnPropertyChanged("IsSelected");
                }
            }
        }

        public Club()
        {

        }

        public Club(string name, DateTime established, int stadiumCapacity, bool isSelected)
        {
            this.name = name;
            this.established = established;
            this.stadiumCapacity = stadiumCapacity;
            this.isSelected = isSelected;
        }

        public override string ToString()
        {
            return this.Name;
        }

        public static ObservableCollection<Club> GetClubs()
        {
            ObservableCollection<Club> clubs = new ObservableCollection<Club>();

            clubs.Add(new Club("Liverpool", new DateTime(1892, 1, 1), 45362, false));
            clubs.Add(new Club("Manchester Utd.", new DateTime(1878, 1, 1), 76212, true));
            clubs.Add(new Club("Chelsea", new DateTime(1905, 1, 1), 42055, false));
            clubs.Add(new Club("Arsenal", new DateTime(1886, 1, 1), 60355, false));

            return clubs;
        }
    }
}
