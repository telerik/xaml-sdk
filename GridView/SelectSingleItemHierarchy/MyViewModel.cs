using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace SelectSingleItemHierarchy
{
    public class MyViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private ObservableCollection<Club> clubs;
		private ObservableCollection<Player> players;

		public ObservableCollection<Club> Clubs
		{
			get
			{
				if (this.clubs == null)
				{
					this.clubs = Club.GetClubs();
				}

				return this.clubs;
			}
		}

		public ObservableCollection<Player> Players
		{
			get
			{
				if (this.players == null)
				{
					this.players = Player.GetPlayers();
				}

				return this.players;
			}
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
	}
}
