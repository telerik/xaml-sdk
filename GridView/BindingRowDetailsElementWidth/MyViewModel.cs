using System;
using System.Collections.ObjectModel;

namespace BindingRowDetailsElementWidth
{
	public class MyViewModel
	{
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
	}
}
