using System;
using System.Collections.ObjectModel;
using System.Linq;
using Telerik.Windows.Controls;

namespace CustomSettingsPane.ViewModels
{
	public class CustomGallery : ViewModelBase
	{
		public string Header { get; set; }
		public ObservableCollection<ShapeViewModel> Shapes { get; set; }
		public CustomGallery()
		{
			this.Shapes = new ObservableCollection<ShapeViewModel>();
		}

		private bool isSelected;
		public bool IsSelected
		{
			get { return this.isSelected; }
			set
			{
				if (this.isSelected != value)
				{
					this.isSelected = value;
					this.OnPropertyChanged("IsSelected");
				}
			}
		}
		
	}
}