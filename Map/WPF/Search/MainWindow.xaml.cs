using System.Linq;
using System.Windows;
using Telerik.Windows.Controls.Map;

namespace Search
{
    public partial class MainWindow : Window
    {
        BingSearchProvider searchProvider;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string bingMapsKey = this.BingMapsKey.Text;

            this.radMap.Provider = new BingMapProvider(MapMode.Aerial, true, bingMapsKey);

            this.searchProvider = new BingSearchProvider();
            searchProvider.ApplicationId = (this.radMap.Provider as BingMapProvider).ApplicationId;
            searchProvider.MapControl = this.radMap;
            searchProvider.SearchCompleted += this.searchProvider_SearchCompleted;

            this.SearchButton.IsEnabled = true;
        }

        private void searchProvider_SearchCompleted(object sender, SearchCompletedEventArgs e)
        {
            this.informationLayer.Items.Clear();

            SearchResultCollection results = e.Response.ResultSets.First().Results;

            if (results.Count > 0)
            {
                foreach (SearchResultBase result in results)
                {
                    MapItem item = new MapItem();
                    item.Caption = result.Name;
                    item.Location = result.LocationData.Locations.First();
                    item.BaseZoomLevel = 9;
                    item.ZoomRange = new ZoomRange(5, 12);
                    this.informationLayer.Items.Add(item);
                }
            }
            else
            {
                SearchRegion region = e.Response.ResultSets.First().SearchRegion;

                if (region != null)
                {
                    this.radMap.SetView(region.GeocodeLocation.BestView);

                    if (region.GeocodeLocation.Address != null && region.GeocodeLocation.Locations.Count > 0)
                    {
                        foreach (Location location in region.GeocodeLocation.Locations)
                        {
                            MapItem item = new MapItem();
                            item.Caption = region.GeocodeLocation.Address.FormattedAddress;
                            item.Location = location;
                            item.BaseZoomLevel = 5;
                            item.ZoomRange = new ZoomRange(5, 12);
                            this.informationLayer.Items.Add(item);
                        }
                    }
                }
            }

            SearchRegionCollection alternateRegions = e.Response.ResultSets.First().AlternateSearchRegions;

            if (alternateRegions.Count > 0)
            {
                foreach (SearchRegion region in alternateRegions)
                {
                    if (region.GeocodeLocation.Address != null && region.GeocodeLocation.Locations.Count > 0)
                    {
                        foreach (Location location in region.GeocodeLocation.Locations)
                        {
                            MapItem item = new MapItem();
                            item.Caption = region.GeocodeLocation.Address.FormattedAddress;
                            item.Location = location;
                            item.BaseZoomLevel = 5;
                            item.ZoomRange = new ZoomRange(5, 12);
                            this.SuggestionsListBox.Items.Add(item);
                        }
                    }
                }
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchRequest request = new SearchRequest();
            request.Query = this.SearchBox.Text;
            this.searchProvider.SearchAsync(request);
        }
    }
}
