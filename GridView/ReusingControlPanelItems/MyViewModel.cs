using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace ReusingControlPanelItems
{
	public class MyViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private ObservableCollection<Club> firstGridClubs;
		private ObservableCollection<Club> secondGridClubs;
		private object selectedItem;

		public ObservableCollection<Club> FirstGridClubs
		{
			get
			{
				if (this.firstGridClubs == null)
				{
					this.firstGridClubs = Club.GetClubs();
				}

				return this.firstGridClubs;
			}
		}

		public ObservableCollection<Club> SecondGridClubs
		{
			get
			{
				if (this.secondGridClubs == null)
				{
					this.secondGridClubs = Club.GetClubs();
				}

				return this.secondGridClubs;
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
