using System.Windows;
using System.Windows.Media;
using Telerik.Windows.Controls.Map;

namespace Routing
{
    public partial class MainWindow : Window
    {
        BingRouteProvider routeProvider;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string bingMapsKey = this.BingMapsKey.Text;

            this.radMap.Provider = new BingMapProvider(MapMode.Aerial, true, bingMapsKey);

            this.routeProvider = new BingRouteProvider();
            this.routeProvider.ApplicationId = (this.radMap.Provider as BingMapProvider).ApplicationId;
            this.routeProvider.MapControl = this.radMap;
            this.routeProvider.RoutingCompleted += this.routeProvider_RoutingCompleted;

            this.ExecuteRouting();
        }

        private void routeProvider_RoutingCompleted(object sender, RoutingCompletedEventArgs e)
        {
            RouteResponse response = e.Response as RouteResponse;
            if (response != null)
            {
                if (response.Result.RoutePath != null)
                {
                    MapPolyline route = new MapPolyline();
                    route.Points = response.Result.RoutePath.Points;
                    route.Stroke = new SolidColorBrush(Colors.Red);
                    route.StrokeThickness = 3;
                    this.informationLayer.Items.Add(route);
                }
            }
        }

        private void ExecuteRouting()
        {
            RouteRequest request = new RouteRequest();
            request.Options.RoutePathType = RoutePathType.Points;
            Location sofia = new Location(42.7072638273239, 23.3318710327148);
            Location munich = new Location(48.1364169716835, 11.577525883913);
            Location amsterdam = new Location(52.3712052404881, 4.8920676112175);
            request.Waypoints.Add(sofia);
            request.Waypoints.Add(munich);
            request.Waypoints.Add(amsterdam);
            this.routeProvider.CalculateRouteAsync(request);
        }
    }
}
