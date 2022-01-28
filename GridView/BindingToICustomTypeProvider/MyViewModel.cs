using BindingToICustomTypeProvider.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Telerik.Windows.Controls;

namespace BindingToICustomTypeProvider
{
	public class MyViewModel : ViewModelBase
	{
		private ObservableCollection<Club> clubs;

		public ObservableCollection<Club> Clubs
		{
			get
			{
				if (this.clubs == null)
				{
					this.clubs = this.GenerateClubs();
				}

				return this.clubs;
			}
		}

		private ObservableCollection<Club> GenerateClubs()
		{
			ObservableCollection<Club> clubs = new ObservableCollection<Club>();

			Club.AddProperty("Name", typeof(string));
			Club.AddProperty("Stadium", typeof(Stadium));
			Club.AddProperty("Players", typeof(Dictionary<string, List<Player>>));

			Stadium.AddProperty("Name", typeof(string));
			Stadium.AddProperty("Capacity", typeof(int));

			Player.AddProperty("Name", typeof(string));
			Player.AddProperty("Position", typeof(string));

			Club club = new Club();
			club.SetPropertyValue("Name", "Liverpool");
			Stadium stadium = new Stadium();
			stadium.SetPropertyValue("Name", "Anfield");
			stadium.SetPropertyValue("Capacity", 45362);
			club.SetPropertyValue("Stadium", stadium);
			List<Player> players = new List<Player>();
			Player gk = new Player();
			gk.SetPropertyValue("Name", "Alisson Becker");
			gk.SetPropertyValue("Position", "GK");
			players.Add(gk);
			Dictionary<string, List<Player>> playersByPosition = new Dictionary<string, List<Player>>();
			playersByPosition["GK"] = players;
			club.SetPropertyValue("Players", playersByPosition);
			clubs.Add(club);

			club = new Club();
			club.SetPropertyValue("Name", "Chelsea");
			stadium = new Stadium();
			stadium.SetPropertyValue("Name", "Stamford Bridge");
			stadium.SetPropertyValue("Capacity", 42055);
			club.SetPropertyValue("Stadium", stadium);
			players = new List<Player>();
			gk = new Player();
			gk.SetPropertyValue("Name", "Kepa Arrizabalaga");
			gk.SetPropertyValue("Position", "GK");
			players.Add(gk);
			playersByPosition = new Dictionary<string, List<Player>>();
			playersByPosition["GK"] = players;
			club.SetPropertyValue("Players", playersByPosition);

			clubs.Add(club);

			return clubs;
		}
	}
}
