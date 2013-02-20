using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Telerik.Windows.Controls.Map;

namespace InformationLayerClusteredDataSource
{
    public partial class MainPage : UserControl
    {
        private Random rnd = new Random();

        public MainPage()
        {
            InitializeComponent();

            LocationRect rect = new LocationRect(new Location(39, -120), new Location(32, -113));
            ObservableCollection<Location> items = this.CreatePointsInRect(1000, rect);

            ClusteredDataSource dataSource = new ClusteredDataSource();
            dataSource.GenerateClustersOnZoom = true;
            dataSource.ItemTemplate = this.Resources["ClusteredItemTemplate"] as DataTemplate;
            dataSource.ClusterItemTemplate = this.Resources["ClusterTemplate"] as DataTemplate;
            dataSource.ItemsSource = items;

            this.informationLayer.ClusteredDataSource = dataSource;
        }

        private ObservableCollection<Location> CreatePointsInRect(int count, LocationRect rect)
        {
            ObservableCollection<Location> collection = new ObservableCollection<Location>();

            rect.MapControl = this.radMap;

            for (int i = 0; i < count; i++)
            {
                Location loc = new Location(
                    rect.North - rect.GeoSize.Height * rnd.NextDouble(),
                    rect.West + rect.GeoSize.Width * rnd.NextDouble());

                collection.Add(loc);
            }

            return collection;
        }

        private void ClusterMouseClick(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            if (element != null)
            {
                ClusterItem cluster = element.DataContext as ClusterItem;
                if (cluster != null)
                {
                    if (cluster.ClusterState == ClusterState.ExpandedToPolygon
                        || cluster.ClusterState == ClusterState.Expanded)
                    {
                        cluster.ClusterState = ClusterState.Collapsed;
                    }
                    else
                    {
                        cluster.HideExpanded = false;
                        cluster.ClusterState = ClusterState.ExpandedToPolygon;
                    }
                }
            }
        }

        private void ClusterRightMouseClick(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            if (element != null)
            {
                ClusterItem cluster = element.DataContext as ClusterItem;
                if (cluster != null)
                {
                    cluster.HideExpanded = true;
                    cluster.ClusterState = ClusterState.Expanded;
                }
            }

            e.Handled = true;
        }

        private void ClusteredItemMouseClick(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            if (element != null)
            {
                ClusteredItem item = element.DataContext as ClusteredItem;
                if (item != null)
                {
                    ClusteredDataSource dataSource = item.DataSource;
                    if (dataSource != null)
                    {
                        dataSource.Collapse(item.Data);
                    }
                }
            }
        }
    }
}
