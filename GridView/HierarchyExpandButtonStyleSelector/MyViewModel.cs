using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Telerik.Windows.Data;

namespace HierarchyExpandButtonStyleSelector
{
	public class MyViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private ObservableCollection<Club> clubs;
		private ObservableCollection<Player> players;
		private object selectedItem;

		
		private IEnumerable<EnumMemberViewModel> rowDetailsVisibilityModes;
		public IEnumerable<EnumMemberViewModel> RowDetailsVisibilityModes
		{
			get
			{
				if (this.rowDetailsVisibilityModes == null)
				{
					this.rowDetailsVisibilityModes = Telerik.Windows.Data.EnumDataSource.FromType<Telerik.Windows.Controls.GridView.GridViewRowDetailsVisibilityMode>();
				}

				return this.rowDetailsVisibilityModes;
			}
		}
        
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
