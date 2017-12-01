using System.Collections.ObjectModel;
using System.ComponentModel;
using Telerik.Windows.Controls;

namespace InsertNewRowOnEnterAndScroll
{
	public class MyViewModel : ViewModelBase
	{
		private ObservableCollection<Club> clubs;
		private ObservableCollection<Player> players;
		private object selectedItem;

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

		public object SelectedItem
		{
			get { return this.selectedItem; }
			set
			{
				if (value != this.selectedItem)
				{
					this.selectedItem = value;
					this.OnPropertyChanged("SelectedItem");
				}
			}
		}
	}
}
