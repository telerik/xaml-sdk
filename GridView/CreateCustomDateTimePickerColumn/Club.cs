using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace CreateCustomDateTimePickerColumn
{
	public class Club: INotifyPropertyChanged
	{
        private string name;
        private DateTime? established;
        private int stadiumCapacity;

		public Club(string name, DateTime? established, int stadiumCapacity)
		{
            this.name = name;
            this.established = established;
            this.stadiumCapacity = stadiumCapacity;
		}

		public String Name 
        {
            get { return this.name; }
            set 
            {
                if (this.name != value)
                {
                    this.name = value;
                    this.OnPropertyChanged("Name");
                }
            }
        }

		public DateTime? Established 
        {
            get { return this.established; }
            set 
            {
                if (this.established != value)
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
                if (this.stadiumCapacity != value)
                {
                    this.stadiumCapacity = value;
                    this.OnPropertyChanged("StadiumCapacity");
                }
            }
        }

		public static IEnumerable<Club> GetClubs()
		{
			ObservableCollection<Club> clubs = new ObservableCollection<Club>();
			clubs.Add(new Club("Liverpool", new DateTime(1892, 1, 1, 13, 35, 15), 45362));
			clubs.Add(new Club("Manchester Utd.", new DateTime(1878, 1, 1, 18, 45, 25), 76212));
			clubs.Add(new Club("Chelsea", new DateTime(1905, 1, 1, 23, 45, 35), 42055));
			clubs.Add(new Club("Arsenal", new DateTime(1886, 1, 1, 4, 55, 45), 60355));
			return clubs;
		}

        public event PropertyChangedEventHandler PropertyChanged;

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
    }
}