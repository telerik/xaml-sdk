using System.Collections.ObjectModel;
using Telerik.Windows.Controls;

namespace ExcelEditing
{
	public class MainViewModel : ViewModelBase
	{
		private ObservableCollection<Player> players;

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
	}
}
