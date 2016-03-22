using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace ValidateINotifyDataErrorInfo
{
    public class Club : ViewModelBase
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
                if (value != this.name || value == null)
                {
                    this.name = value;
                    this.OnPropertyChanged("Name");
                    this.ValidateName();
                }
            }
        }
        private void ValidateName()
        {
            var error = "Name must be at least 5 chars!";

            if (this.Name.Length < 5)
            {
                this.AddError("Name", error);
            }
            else
            {
                this.RemoveError("Name", error);
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
                    this.ValidateEstablished();
                }
            }
        }

        private void ValidateEstablished()
        {
            var error = "Established date cannot be before 1800!";

            if (this.Established.Year < 1800)
            {
                this.AddError("Established", error);
            }
            else
            {
                this.RemoveError("Established", error);
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
                    this.ValidateStadiumCapacity();
                }
            }
        }

        private void ValidateStadiumCapacity()
        {
            var error = "Capacity cannot be smaller than 30,000!";

            if (this.StadiumCapacity < 30000)
            {
                this.AddError("StadiumCapacity", error);
            }
            else
            {
                this.RemoveError("StadiumCapacity", error);
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

            // Liverpool
            club = new Club("Liverpool", new DateTime(1892, 1, 1), 45362);
            clubs.Add(club);

            // Manchester Utd.
            club = new Club("Manchester Utd.", new DateTime(1878, 1, 1), 76212);
            clubs.Add(club);

            // Chelsea
            club = new Club("Chelsea", new DateTime(1905, 1, 1), 42055);
            clubs.Add(club);

            // Arsenal
            club = new Club("Arsenal", new DateTime(1886, 1, 1), 60355);
            clubs.Add(club);

            return clubs;
        }
    }
}
