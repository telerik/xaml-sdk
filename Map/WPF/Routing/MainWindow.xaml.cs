using System.Windows;
using System.Windows.Media;
using BingRest = Telerik.Windows.Controls.DataVisualization.Map.BingRest;
using Telerik.Windows.Controls.Map;
using System;
using System.Windows.Threading;

namespace Routing
{
    public partial class MainWindow : Window
    {
        private LocationCollection wayPoints = new LocationCollection();
        private BingRestMapProvider provider;
        private BingRestRouteRequest request;
        private bool isProviderInitializedFailed;
        DispatcherTimer timer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();      
            this.InitializeRequestAndOptions();
        }

        private void InitializeRequestAndOptions()
        {
            this.request = new BingRestRouteRequest()
            {
                Culture = new System.Globalization.CultureInfo("en-US"),
            };        
            this.wayPointsLayer.ItemsSource = this.wayPoints;
            this.modeCombo.ItemsSource = Enum.GetValues(typeof(BingRestTravelMode));
            this.routeOptimizatioCombo.ItemsSource = Enum.GetValues(typeof(BingRestRouteOptimization));
            this.routeAvoidanceCombo.ItemsSource = Enum.GetValues(typeof(BingRestRouteAvoidance));
        }

        private void Provider_CalculateRouteError(object sender, BingRestCalculateRouteErrorEventArgs e)
        {
            MessageBox.Show(e.Error.ToString());
        }

        private void Provider_CalculateRouteCompleted(object sender, BingRestRoutingCompletedEventArgs e)
        {
            this.findRouteButton.IsEnabled = true;

            BingRest.Route route = e.Route;
            if (route != null)
            {
                PolylineData routeLine = this.CreateNewPolyline(Colors.Blue, 3);

                // Building the polyline representing the route.
                foreach (double[] coordinatePair in route.RoutePath.Line.Coordinates)
                {
                    Location point = new Location(coordinatePair[0], coordinatePair[1]);
                    routeLine.Points.Add(point);
                }

                this.routeLayer.Items.Add(routeLine);

                // Bringing the route into view.
                double[] bbox = e.Route.BoundingBox;
                LocationRect rect = new LocationRect(new Location(bbox[2], bbox[1]), new Location(bbox[0], bbox[3]));
                this.radMap.SetView(rect);
            }
        }

        private PolylineData CreateNewPolyline(Color color, double thickness)
        {
            PolylineData routeLine = new PolylineData()
            {
                ShapeFill = new MapShapeFill()
                {
                    Stroke = new SolidColorBrush(color),
                    StrokeThickness = thickness,   
                    StrokeDashArray = this.request.Options.Mode == BingRestTravelMode.Walking ? new DoubleCollection(){2, 1} : new DoubleCollection(),
                },
                Points = new LocationCollection(),                
            };
            return routeLine;
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
            this.request.Waypoints.Clear();
        }

        private void FindRouteClicked(object sender, RoutedEventArgs e)
        {
            this.routeLayer.Items.Clear();
            this.request.Waypoints.Clear();
            this.request.Options.Mode = (BingRestTravelMode)this.modeCombo.SelectedValue;
            this.request.Options.Optimization = (BingRestRouteOptimization)this.routeOptimizatioCombo.SelectedValue;
            this.request.Options.RouteAvoidance = (BingRestRouteAvoidance)this.routeAvoidanceCombo.SelectedValue;
            this.request.Options.RouteAttributes = BingRestRouteAttributes.RoutePath;

            if (this.wayPoints.Count > 1)
            {
                this.findRouteButton.IsEnabled = false;

                foreach (Location location in this.wayPoints)
                {
                    this.request.Waypoints.Add(location.ToString());
                }
                this.provider.CalculateRouteAsync(request);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.isProviderInitializedFailed = false;

            this.timer.Interval = new TimeSpan(0, 0, 1);
            this.timer.Tick += Timer_Tick;
            this.timer.Start();

            this.provider  = new BingRestMapProvider(MapMode.Road, true, this.BingMapsKey.Text);
            this.provider.InitializationFaulted += this.Provider_InitializationFaulted;
            this.radMap.Provider = this.provider;
            this.provider.CalculateRouteCompleted += this.Provider_CalculateRouteCompleted;
            this.provider.CalculateRouteError += this.Provider_CalculateRouteError;
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
    }
}
