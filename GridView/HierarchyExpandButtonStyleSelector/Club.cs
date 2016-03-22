using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace HierarchyExpandButtonStyleSelector
{
	public class Club : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private string name;
		private DateTime established;
		private int stadiumCapacity;
		private ObservableCollection<Player> players;

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

		public ObservableCollection<Player> Players
		{
			get
			{
				if (null == this.players)
				{
					this.players = new ObservableCollection<Player>();
				}

				return this.players;
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

		public Club(string name, DateTime established, int stadiumCapacity, ObservableCollection<Player> players)
			: this(name, established, stadiumCapacity)
		{
			this.players = players;
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
			club.Players.Add(new Player("Pepe Reina", 25, Position.GK, "Spain"));
			club.Players.Add(new Player("Jamie Carragher", 23, Position.DF, "England"));
			club.Players.Add(new Player("Steven Gerrard", 8, Position.MF, "England"));
			club.Players.Add(new Player("Fernando Torres", 9, Position.FW, "Spain"));
			clubs.Add(club);

			// Manchester Utd.
			club = new Club("Manchester Utd.", new DateTime(1878, 1, 1), 76212);
			club.Players.Add(new Player("Edwin van der Sar", 1, Position.GK, "Netherlands"));
			club.Players.Add(new Player("Rio Ferdinand", 5, Position.DF, "England"));
			club.Players.Add(new Player("Ryan Giggs", 11, Position.MF, "Wales"));
			club.Players.Add(new Player("Wayne Rooney", 10, Position.FW, "England"));
			clubs.Add(club);

			// Chelsea
			club = new Club("Chelsea", new DateTime(1905, 1, 1), 42055);
			club.Players.Add(new Player("Petr Čech", 1, Position.GK, "Czech Republic"));
			club.Players.Add(new Player("John Terry", 26, Position.DF, "England"));
			club.Players.Add(new Player("Frank Lampard", 8, Position.MF, "England"));
			club.Players.Add(new Player("Nicolas Anelka", 39, Position.FW, "France"));
			clubs.Add(club);

			// Arsenal
			club = new Club("Arsenal", new DateTime(1886, 1, 1), 60355);
			club.Players.Add(new Player("Manuel Almunia", 1, Position.GK, "Spain"));
			club.Players.Add(new Player("Gaël Clichy", 22, Position.DF, "France"));
			club.Players.Add(new Player("Cesc Fàbregas", 4, Position.MF, "Spain"));
			club.Players.Add(new Player("Robin van Persie", 11, Position.FW, "Netherlands"));
			clubs.Add(club);

			return clubs;
		}
	}
}