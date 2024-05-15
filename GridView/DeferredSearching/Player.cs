using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DeferredSearching
{
	public class Player : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private string name;
		private int number;
		private string country;

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

		public int Number
		{
			get { return this.number; }
			set
			{
				if (value != this.number)
				{
					this.number = value;
					this.OnPropertyChanged("Number");
				}
			}
		}

		public string Country
		{
			get { return this.country; }
			set
			{
				if (value != this.country)
				{
					this.country = value;
					this.OnPropertyChanged("Country");
				}
			}
		}

		public Player()
		{

		}

		public Player(string name, int number, string country)
		{
			this.name = name;
			this.number = number;
			this.country = country;
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

		public static ObservableCollection<Player> GetPlayers()
		{
			var players = new ObservableCollection<Player>();
			players.Add(new Player("Pepe Reina", 25, "Spain"));
			players.Add(new Player("Jamie Carragher", 23, "England"));
			players.Add(new Player("Steven Gerrard", 8, "England"));
			players.Add(new Player("Fernando Torres", 9, "Spain"));
			players.Add(new Player("Edwin van der Sar", 1, "Netherlands"));
			players.Add(new Player("Rio Ferdinand", 5, "England"));
			players.Add(new Player("Ryan Giggs", 11, "Wales"));
			players.Add(new Player("Wayne Rooney", 10, "England"));
			players.Add(new Player("Petr Čech", 1, "Czech Republic"));
			players.Add(new Player("John Terry", 26, "England"));
			players.Add(new Player("Frank Lampard", 8, "England"));
			players.Add(new Player("Nicolas Anelka", 39, "France"));
			players.Add(new Player("Manuel Almunia", 1, "Spain"));
			players.Add(new Player("Gaël Clichy", 22, "France"));
			players.Add(new Player("Cesc Fàbregas", 4, "Spain"));
			players.Add(new Player("Robin van Persie", 11, "Netherlands"));
			return players;
		}
	}
}
