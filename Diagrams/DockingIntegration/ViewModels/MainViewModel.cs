using System;
using System.Linq;
using Telerik.Windows.Controls;
using System.Windows;
using System.Collections;
using Telerik.Windows.Controls.Diagrams.Extensions;

namespace DockingIntegration.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private double zoomLevel;
        private bool isGridVisible = false;
        private bool isSnapEnabled = true;
        private int snapX = 20;
        private int snapY = 20;
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
        public bool IsGridVisible
        {
            get
            {
                return this.isGridVisible;
            }
            set
            {
                if (this.isGridVisible != value)
                {
                    this.isGridVisible = value;
                    this.OnPropertyChanged("IsGridVisible");
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
        public string SnapX
        {
            get
            {
                return this.snapX.ToString();
            }
            set
            {
                int posibleValue;
                var result = int.TryParse(value, out posibleValue);
                if (result)
                {
                    this.snapX = posibleValue;
                    this.OnPropertyChanged("SnapX");
                }
            }
        }
        public string SnapY
        {
            get
            {
                return this.snapY.ToString();
            }
            set
            {
                int posibleValue;
                var result = int.TryParse(value, out posibleValue);
                if (result)
                {
                    this.snapY = posibleValue;
                    this.OnPropertyChanged("SnapY");
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
