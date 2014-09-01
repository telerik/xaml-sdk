using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Telerik.Windows.Data;
using Telerik.Windows.Controls.GridView;


namespace NewRowPosition
{
	public class MyViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private ObservableCollection<Club> clubs;
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

		private IEnumerable<EnumMemberViewModel> newRowPositions;
		
		public IEnumerable<EnumMemberViewModel> NewRowPositions
		{
			get
			{
				if (this.newRowPositions == null)
				{
					this.newRowPositions = Telerik.Windows.Data.EnumDataSource.FromType<GridViewNewRowPosition>();
				}

				return this.newRowPositions;
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
