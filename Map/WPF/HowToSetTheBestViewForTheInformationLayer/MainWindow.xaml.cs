using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Telerik.Windows.Controls.Map;

namespace HowToSetTheBestViewForTheInformationLayer
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.radMap.InitializeCompleted += radMap_InitializeCompleted;
        }

        private IEnumerable<object> GetIEnumerable(InformationLayer layer)
        {
            foreach (var item in layer.Items)
            {
                yield return item;
            }
        }

        void radMap_InitializeCompleted(object sender, EventArgs e)
        {
            this.informationLayer.ItemsSource = this.GetMapData();

            if (!this.informationLayer.IsArrangeValid)
            {
                this.radMap.LayoutUpdated += this.RadMap1_LayoutUpdated;
            }
        }

        private void RadMap1_LayoutUpdated(object sender, EventArgs e)
        {
            this.radMap.LayoutUpdated -= this.RadMap1_LayoutUpdated;

            this.SetBestView();
        }

        private void SetBestView()
        {
            LocationRect rect = this.informationLayer.GetBestView(this.informationLayer.Items.Cast<object>());
            rect.MapControl = this.radMap;
            this.radMap.Center = rect.Center;
            this.radMap.ZoomLevel = rect.ZoomLevel;
        }

        private ObservableCollection<MapItem> GetMapData()
        {
            ObservableCollection<MapItem> data = new ObservableCollection<MapItem>();
            data.Add(new MapItem("Sofia", new Location(42.6957539183824, 23.3327663758679), 5, new ZoomRange(5, 12)));
            data.Add(new MapItem("Plovdiv", new Location(42.1429369264591, 24.7498095849434), 5, new ZoomRange(5, 12)));
            data.Add(new MapItem("Burgas", new Location(42.5131732087098, 27.4611884843576), 5, new ZoomRange(5, 12)));
            data.Add(new MapItem("Varna", new Location(43.2073941930888, 27.9275176988258), 5, new ZoomRange(5, 12)));

            return data;
        }
    }
}
