using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Telerik.Windows.Controls.Map;

namespace Routing
{
    public partial class MainPage : UserControl
    {
        private BingRouteProvider routeProvider;
        private LocationCollection wayPoints = new LocationCollection();

        public MainPage()
        {
            InitializeComponent();

            this.routeProvider = new BingRouteProvider();
            this.routeProvider.ApplicationId = "AqaPuZWytKRUA8Nm5nqvXHWGL8BDCXvK8onCl2PkC581Zp3T_fYAQBiwIphJbRAK";
            this.routeProvider.MapControl = this.radMap;
            this.routeProvider.RoutingCompleted += this.RouteProvider_RoutingCompleted;

            this.wayPointsLayer.ItemsSource = this.wayPoints;
        }

        private void MapMouseClick(object sender, MapMouseRoutedEventArgs eventArgs)
        {
            this.wayPoints.Add(eventArgs.Location);
        }

        private void ClearRouteClicked(object sender, RoutedEventArgs e)
        {
            this.findRouteButton.IsEnabled = true;

            this.wayPoints.Clear();
            this.routeLayer.Items.Clear();
        }

        private void FindRouteClicked(object sender, RoutedEventArgs e)
        {
            this.routeLayer.Items.Clear();

            RouteRequest routeRequest = new RouteRequest();
            routeRequest.Culture = new System.Globalization.CultureInfo("en-US");
            routeRequest.Options.RoutePathType = RoutePathType.Points;

            if (this.wayPoints.Count > 1)
            {
                this.findRouteButton.IsEnabled = false;

                foreach (Location location in this.wayPoints)
                {
                    routeRequest.Waypoints.Add(location);
                }
                this.routeProvider.CalculateRouteAsync(routeRequest);
            }
        }

        private void RouteProvider_RoutingCompleted(object sender, RoutingCompletedEventArgs e)
        {
            this.findRouteButton.IsEnabled = true;

            RouteResponse routeResponse = e.Response as RouteResponse;
            if (routeResponse != null
                && routeResponse.Error == null)
            {
                if (routeResponse.Result != null
                    && routeResponse.Result.RoutePath != null)
                {
                    PolylineData routeLine = new PolylineData()
                    {
                        Points = routeResponse.Result.RoutePath.Points,
                        ShapeFill = new MapShapeFill()
                        {
                            Stroke = new SolidColorBrush(Colors.Red),
                            StrokeThickness = 2
                        }
                    };

                    this.routeLayer.Items.Add(routeLine);
                }
            }
        }
    }
}
