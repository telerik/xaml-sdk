using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls.Map;

namespace Geocode
{
    public partial class MainPage : UserControl
    {
        BingGeocodeProvider geocodeProvider;

        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string bingMapsKey = this.BingMapsKey.Text;

            this.radMap.Provider = new BingMapProvider(MapMode.Aerial, true, bingMapsKey);

            this.geocodeProvider = new BingGeocodeProvider();
            this.geocodeProvider.ApplicationId = (this.radMap.Provider as BingMapProvider).ApplicationId;
            this.geocodeProvider.MapControl = this.radMap;
            this.geocodeProvider.GeocodeCompleted += this.geocodeProvider_GeocodeCompleted;

            this.GeocodeButton.IsEnabled = true;
        }

        private void geocodeProvider_GeocodeCompleted(object sender, GeocodeCompletedEventArgs e)
        {
            foreach (GeocodeResult result in e.Response.Results)
            {
                MessageBox.Show(string.Format("Longitude: {0}, Latitude: {1}, Address: {2}", result.Locations.First().Longitude, result.Locations.First().Latitude, result.DisplayName));
            }
        }

        private void GeocodeButton_Click(object sender, RoutedEventArgs e)
        {
            GeocodeRequest request = new GeocodeRequest();
            request.Query = this.InputBox.Text;
            this.geocodeProvider.GeocodeAsync(request);
        }

        private void radMap_MapMouseDoubleClick(object sender, MapMouseRoutedEventArgs e)
        {
            ReverseGeocodeRequest reverseRequest = new ReverseGeocodeRequest();
            reverseRequest.Location = e.Location;
            this.geocodeProvider.ReverseGeocodeAsync(reverseRequest);
        }
    }
}
