using System;
using System.Collections;
using System.Linq;
using System.Windows;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Diagrams.Extensions;

namespace DiagramDesignToolBox
{
	public class MainViewModel : ViewModelBase
	{
		private double zoomLevel;
		private bool isSnapEnabled = true;
		private int snapValue = 20;
		private Size cellSize = new Size(20, 20);
		private readonly IEnumerable items;

		public MainViewModel()
		{
			this.zoomLevel = 1d;
			this.items = new HierarchicalGalleryItemsCollection();
		}

		public IEnumerable Items
		{
			get
			{
				return this.items;
			}
		}
		public double ZoomLevel
		{
			get
			{
				return this.zoomLevel;
			}
			set
			{
				var coercedValue = Math.Round(value, 2);
				if (this.zoomLevel != coercedValue)
				{
					this.zoomLevel = coercedValue;
					this.OnPropertyChanged("ZoomLevel");
				}
			}
		}
		public bool IsSnapEnabled
		{
			get
			{
				return this.isSnapEnabled;
			}
			set
			{
				if (this.isSnapEnabled != value)
				{
					this.isSnapEnabled = value;
					this.OnPropertyChanged("IsSnapEnabled");
				}
			}
		}
		public string SnapValue
		{
			get
			{
				return this.snapValue.ToString();
			}
			set
			{
				int posibleValue;
				var result = int.TryParse(value, out posibleValue);
				if (result)
				{
					this.snapValue = posibleValue;
					this.OnPropertyChanged("SnapValue");
				}
			}
		}
		public Size CellSize
		{
			get
			{
				return this.cellSize;
			}
			set
			{
				if (this.cellSize != value)
				{
					this.cellSize = value;
					this.OnPropertyChanged("CellSize");
				}
			}
		}
	}
}