using System.Linq;
using System.Windows;
using System.Windows.Media;
using Telerik.Windows.Controls.Map;

namespace Search
{
    public partial class MainWindow : Window
    {
        private BingSearchProvider searchProvider;
        private MapItemsCollection itemCollection = new MapItemsCollection();

        public MainWindow()
        {
            InitializeComponent();

            this.searchProvider = new BingSearchProvider();
            this.searchProvider.ApplicationId = "AqaPuZWytKRUA8Nm5nqvXHWGL8BDCXvK8onCl2PkC581Zp3T_fYAQBiwIphJbRAK";
            this.searchProvider.MapControl = this.radMap;
            this.searchProvider.SearchCompleted += new System.EventHandler<SearchCompletedEventArgs>(Provider_SearchCompleted);

            this.itemsLayer.ItemsSource = this.itemCollection;
        }

        private void SearchHandler(object sender, RoutedEventArgs e)
        {
            string query = this.SearchCondition.Text;

            if (!string.IsNullOrEmpty(query))
            {
                SearchRequest request = new SearchRequest();
                request.Culture = new System.Globalization.CultureInfo("en-US");
                request.Query = query;

                this.searchProvider.SearchAsync(request);
            }
        }

        private void Provider_SearchCompleted(object sender, SearchCompletedEventArgs args)
        {
            this.itemCollection.Clear();
            SearchResultCollection results = args.Response.ResultSets.First().Results;
            if (results.Count > 0)
            {
                foreach (SearchResultBase result in results)
                {
                    MapItem item = new MapItem()
                    {
                        Title = result.Name,
                        Location = result.LocationData.Locations[0]
                    };
                    this.itemCollection.Add(item);
                }
                this.radMap.SetView(args.Response.ResultSets[0].SearchRegion.GeocodeLocation.BestView);
            }

            this.itemCollection.Clear();

            SearchResponse response = args.Response;
            if (response != null)
            {
                if (response.Error == null)
                {
                    if (response.ResultSets.Count > 0)
                    {
                        this.AddResultsToItemsCollection(response);

                        if (response.ResultSets[0].SearchRegion != null)
                        {
                            // Set map viewport to the best view returned in the search result.
                            this.SetBestView(response);

                            // Show map shape around bounding area
                            this.ShowBoundingArea(response);

                            this.SearchRegionToItemsCollection(response);
                        }
                    }
                }
                else
                {
                    // Show error info.
                    MessageBox.Show(
                        response.Error.ToString(),
                        "Error",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
        }

        private void AddResultsToItemsCollection(SearchResponse response)
        {
            if (response.ResultSets[0].Results.Count > 0)
            {
                foreach (SearchResultBase result in response.ResultSets[0].Results)
                {
                    MapItem item = new MapItem()
                    {
                        Title = result.Name,
                        Location = result.LocationData.Locations[0]
                    };
                    this.itemCollection.Add(item);
                }
            }
        }




        private void SearchRegionToItemsCollection(SearchResponse response)
        {
            if (response.ResultSets[0].SearchRegion.GeocodeLocation != null
                && response.ResultSets[0].SearchRegion.GeocodeLocation.Address != null
                && response.ResultSets[0].SearchRegion.GeocodeLocation.Locations.Count > 0)
            {
                foreach (Location location in response.ResultSets[0].SearchRegion.GeocodeLocation.Locations)
                {
                    MapItem item = new MapItem()
                    {
                        Title = response.ResultSets[0].SearchRegion.GeocodeLocation.Address.FormattedAddress,
                        Location = location
                    };
                    this.itemCollection.Add(item);
                }
            }
        }


        private void SetBestView(SearchResponse response)
        {
            if (response.ResultSets[0].SearchRegion.GeocodeLocation != null)
            {
                this.radMap.SetView(response.ResultSets[0].SearchRegion.GeocodeLocation.BestView);
            }
            else if (response.ResultSets[0].SearchRegion.BoundingArea != null)
            {
                this.radMap.SetView(response.ResultSets[0].SearchRegion.BoundingArea.GeographicalBounds);
            }
        }

        private void ShowBoundingArea(SearchResponse response)
        {
            if (response.ResultSets[0].SearchRegion.BoundingArea != null)
            {   
                MapShapeData boundingArea = response.ResultSets[0].SearchRegion.BoundingAreaData;
                boundingArea.ShapeFill = new MapShapeFill()
                {
                    Stroke = new SolidColorBrush(Colors.Red),
                    StrokeThickness = 1
                };
                this.regionLayer.Items.Add(boundingArea);
            }
        }
    }
}
