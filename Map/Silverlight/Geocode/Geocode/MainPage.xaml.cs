using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls.Map;

namespace Geocode
{
    public partial class MainPage : UserControl
    {
        BingRestMapProvider restProvider;

        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string bingMapsKey = this.BingMapsKey.Text;

            this.restProvider = new BingRestMapProvider(MapMode.Aerial, true, bingMapsKey);
            this.radMap.Provider = this.restProvider;
            this.restProvider.SearchLocationCompleted += this.restProvider_SearchLocationCompleted;
            this.restProvider.SearchLocationError += this.restProvider_SearchLocationError;
            this.GeocodeButton.IsEnabled = true;
        }

        private void restProvider_SearchLocationError(object sender, BingRestSearchLocationErrorEventArgs e)
        {
            MessageBox.Show(e.Error.ToString());
        }

        private void restProvider_SearchLocationCompleted(object sender, BingRestSearchLocationCompletedEventArgs e)
        {
            double[] bbox = e.Locations[0].BoundingBox;
            LocationRect rect = new LocationRect(new Location(bbox[2], bbox[1]), new Location(bbox[0], bbox[3]));
            this.radMap.SetView(rect);

            foreach (Telerik.Windows.Controls.DataVisualization.Map.BingRest.Location location in e.Locations)
            {
                var coordinates = location.GeocodePoints[0].Coordinates;
                double latitude = coordinates[0];
                double longitude = coordinates[1];
                MessageBox.Show(string.Format("Longitude: {0}, Latitude: {1}, Address: {2}", longitude, latitude, location.Address.FormattedAddress));
            }
        }

        private void GeocodeButton_Click(object sender, RoutedEventArgs e)
        {
            // Search Location by string Query. Geocode.
            BingRestSearchLocationRequest request = new BingRestSearchLocationRequest();
            request.Query = this.InputBox.Text;
            this.restProvider.SearchLocationAsync(request);
        }

        private void radMap_MapMouseDoubleClick(object sender, MapMouseRoutedEventArgs e)
        {
            // Search Address by Location. Reversed geocode.
            BingRestSearchLocationRequest reverseRequest = new BingRestSearchLocationRequest();
            reverseRequest.Query = e.Location.ToString();
            this.restProvider.SearchLocationAsync(reverseRequest);
        }
    }
}
