using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using Telerik.Windows.Controls.Map;

namespace Routing
{
    public partial class MainWindow : Window
    {
        private LocationCollection wayPoints = new LocationCollection();
        private AzureMapProvider provider;
        private bool isProviderInitializedFailed;
        DispatcherTimer timer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
            this.InitializeRequestAndOptions();
        }

        private void InitializeRequestAndOptions()
        {
            this.WayPointsLayer.ItemsSource = this.wayPoints;
        }

        private void MapMouseClick(object sender, MapMouseRoutedEventArgs eventArgs)
        {
            this.wayPoints.Add(eventArgs.Location);
        }

        private void ClearRouteClicked(object sender, RoutedEventArgs e)
        {
            this.findRouteButton.IsEnabled = true;
            this.wayPoints.Clear();
            this.RouteLayer.Items.Clear();
        }

        private async void FindRouteClicked(object sender, RoutedEventArgs e)
        {
            this.RouteLayer.Items.Clear();

            RouteInfo routeInfo = null;
            try
            {
                routeInfo = await AzureRoutingHelper.GetRouteDirections(this.wayPoints.First(), this.wayPoints.Last());
            }
            catch (Exception)
            {
                MessageBox.Show("Please, update the start or end location!", "Route calculation error.", MessageBoxButton.OK);
                return;
            }

            if (routeInfo != null)
            {
                PolylineData routeLine = this.CreateNewPolyline(routeInfo.Points, Colors.Red, 3);

                this.RouteLayer.Items.Add(routeLine);

                var routePointModels = new List<RoutePointModel>();
                for (int i = 0; i < routeInfo.WayPointInfos.Count; i++)
                {
                    var wayPointInfo = routeInfo.WayPointInfos[i];
                    RoutePointModel model = new RoutePointModel()
                    {
                        Instruction = wayPointInfo.Message,
                        Location = wayPointInfo.Location,
                        IsStartOrEnd = i == 0 || i == routeInfo.WayPointInfos.Count - 1,
                        Caption = i == 0 ? "A" : i == routeInfo.WayPointInfos.Count - 1 ? "B" : string.Empty
                    };
                    routePointModels.Add(model);
                }
            }

            var bestView = this.RouteLayer.GetBestView(this.RouteLayer.Items);
            this.radMap.SetView(bestView);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.isProviderInitializedFailed = false;

            this.timer.Interval = new TimeSpan(0, 0, 1);
            this.timer.Tick += Timer_Tick;
            this.timer.Start();

            this.provider = new AzureMapProvider(this.AzureMapsKey.Text, AzureTileSet.Road);
            AzureRoutingHelper.AzureMapsSubscriptionKey = this.AzureMapsKey.Text;

            this.provider.InitializationFaulted += this.Provider_InitializationFaulted;
            this.radMap.Provider = this.provider;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (!this.isProviderInitializedFailed)
            {
                this.initializationArea.Visibility = Visibility.Collapsed;
                this.timer.Tick -= this.Timer_Tick;
                this.timer.Stop();
            }
        }

        private void Provider_InitializationFaulted(object sender, InitializationFaultEventArgs e)
        {
            this.isProviderInitializedFailed = true;
        }

        private PolylineData CreateNewPolyline(IEnumerable<Location> directionPoints, Color color, double thickness)
        {
            PolylineData routeLine = new PolylineData()
            {
                ShapeFill = new MapShapeFill()
                {
                    Stroke = new SolidColorBrush(color),
                    StrokeThickness = thickness
                },
                Points = new LocationCollection(),
            };

            foreach (var point in directionPoints)
            {
                routeLine.Points.Add(point);
            }

            return routeLine;
        }
    }
}
