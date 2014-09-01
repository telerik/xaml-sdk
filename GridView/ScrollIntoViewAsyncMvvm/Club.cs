using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace ScrollIntoViewAsyncMvvm
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

		public static ObservableCollection<Club> GetClubs()
		{
			ObservableCollection<Club> clubs = new ObservableCollection<Club>();

			for (int index = 0; index < 100; index++)
			{
				Club club;

				club = new Club("Club " + index, new DateTime(1892, 1, 1).AddDays(index), 45362 + (index * 5));
				clubs.Add(club);
			}

			return clubs;
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

		
	}
}
