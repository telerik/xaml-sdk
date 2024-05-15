using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ShowToolTipOnTrimmedText
{
	/// <summary>
	/// A football player.
	/// </summary>
	public class Player : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private string name;
		private int number;
		private Position position;
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

		public Position Position
		{
			get { return this.position; }
			set
			{
				if (value != this.position)
				{
					this.position = value;
					this.OnPropertyChanged("Position");
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

		public Player(string name, int number, Position position, string country)
		{
			this.name = name;
			this.number = number;
			this.position = position;
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

			players.Add(new Player("Manuel Almunia", 1, Position.GK, "Spain"));
			players.Add(new Player("Gaël Clichy", 22, Position.DF, "France"));
			players.Add(new Player("Cesc Fàbregas", 4, Position.MF, "Spain"));
			players.Add(new Player("Robin van Persie", 11, Position.FW, "Netherlands"));
			players.Add(new Player("Pepe Reina", 25, Position.GK, "Spain"));
			players.Add(new Player("Jamie Carragher", 23, Position.DF, "England"));
			players.Add(new Player("Steven Gerrard", 8, Position.MF, "England"));
			players.Add(new Player("Fernando Torres", 9, Position.FW, "Spain"));
			players.Add(new Player("Edwin van der Sar", 1, Position.GK, "Netherlands"));
			players.Add(new Player("Rio Ferdinand", 5, Position.DF, "England"));
			players.Add(new Player("Ryan Giggs", 11, Position.MF, "Wales"));
			players.Add(new Player("Wayne Rooney", 10, Position.FW, "England"));
			players.Add(new Player("Petr Čech", 1, Position.GK, "Czech Republic"));
			players.Add(new Player("John Terry", 26, Position.DF, "England"));
			players.Add(new Player("Frank Lampard", 8, Position.MF, "England"));
			players.Add(new Player("Nicolas Anelka", 39, Position.FW, "France"));

			return players;
		}
	}
}
