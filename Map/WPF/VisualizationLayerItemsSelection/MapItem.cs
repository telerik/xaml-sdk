using System;
using System.ComponentModel;
using System.Windows.Media;
using Telerik.Windows.Controls.Map;

namespace VisualizationLayerItemsSelection
{
	public class MapItem : INotifyPropertyChanged
	{
		private static SolidColorBrush RegularBrush = new SolidColorBrush(Colors.Green);
		private static SolidColorBrush SelectedBrush = new SolidColorBrush(Colors.Red);

		private string caption = string.Empty;
		private Location location = Location.Empty;
		private bool isSelected = false;
		private SolidColorBrush background = RegularBrush;

		public MapItem(
			string caption,
			Location location)
		{
			this.Caption = caption;
			this.Location = location;
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public SolidColorBrush Background
		{
			get
			{
				return this.background;
			}

			set
			{
				this.background = value;
				this.OnPropertyChanged("Background");
			}
		}

		public string Caption
		{
			get
			{
				return this.caption;
			}

			set
			{
				this.caption = value;
				this.OnPropertyChanged("Caption");
			}
		}

		public bool IsSelected
		{
			get
			{
				return this.isSelected;
			}

			set
			{
				this.isSelected = value;
				this.Background = this.isSelected ? SelectedBrush : RegularBrush;
				this.OnPropertyChanged("IsSelected");
			}
		}

		public Location Location
		{
			get
			{
				return this.location;
			}

			set
			{
				this.location = value;
				this.OnPropertyChanged("Location");
			}
		}

		private void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(
					this,
					new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
