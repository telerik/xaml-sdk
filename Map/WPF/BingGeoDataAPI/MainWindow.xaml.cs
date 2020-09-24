using System;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Map;

namespace BingGeoDataAPI
{
    public partial class MainWindow : Window
    {
        private static Random rndNumberGenerator = new Random();
        private Location lastSearchLoaction;
        private Brush[] brushes = new Brush[] { Brushes.Red, Brushes.Green, Brushes.DeepSkyBlue, Brushes.DeepPink, Brushes.Yellow, Brushes.LimeGreen, Brushes.Blue, Brushes.Aqua, Brushes.Aquamarine };

        public MainWindow()
        {
            StyleManager.ApplicationTheme = new MaterialTheme();
            InitializeComponent();
            this.bingProvider.SearchLocationCompleted += BingProvider_SearchLocationCompleted;
            this.bingProvider.SearchLocationError += BingProvider_SearchLocationError;
        }

        private void BingProvider_SearchLocationError(object sender, BingRestSearchLocationErrorEventArgs e)
        {
            MessageBox.Show(e.Error.ToString());
        }

        private void BingProvider_SearchLocationCompleted(object sender, BingRestSearchLocationCompletedEventArgs e)
        {
            if (e.Locations.Count() == 0)
                return;

            Telerik.Windows.Controls.DataVisualization.Map.BingRest.Location foundLocation = e.Locations[0];
            Location mapLocation = new Location(foundLocation.Point.Coordinates[0], foundLocation.Point.Coordinates[1]);
            this.lastSearchLoaction = mapLocation;

            SearchArea area = new SearchArea(foundLocation.Name, mapLocation, this.bingProvider.ApplicationId, "'" + foundLocation.EntityType + "'");
            this.SearchByArea(area);

            if (this.searchBarPanel.IsEnabled == false)
            {
                this.searchBarPanel.IsEnabled = true;
            }
        }
        
        private void Client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null)
                return;

            this.vizLayer.Items.Clear();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(e.Result);

            XmlNodeList listOfShapes = xmlDoc.GetElementsByTagName("d:Shape");
            foreach (XmlNode shapeNode in listOfShapes)
            {
                LocationCollection list;
                GeodataHelper.TryParseEncodedValue(shapeNode.InnerText.Split(',').LastOrDefault(), out list);

                if (list != null && list.Count > 0)
                {
                    this.AddPolygon(list, listOfShapes.Count > 0);
                }
            }

            XmlNodeList bestViews = xmlDoc.GetElementsByTagName("d:BestMapViewBox");
            if (bestViews.Count > 0)
            {
                XmlNode bestView = bestViews[0];
                LocationRect bestViewRect = GeodataHelper.GetBestViewFromXMLString(bestView.InnerText);
                this.radMap.SetView(bestViewRect);
            }
         
            Ellipse ellipse = new Ellipse() { Width = 20, Height = 20, Fill = Brushes.Transparent, Stroke = Brushes.Orange, StrokeThickness = 6 };
            MapLayer.SetLocation(ellipse, this.lastSearchLoaction);
            this.vizLayer.Items.Add(ellipse);
        }

        private void OnSetIdClick(object sender, RoutedEventArgs e)
        {
            this.radMap.Provider = null;
            this.bingProvider.ApplicationId = this.appIdInput.Text;
            this.radMap.Provider = this.bingProvider;

            if (string.IsNullOrEmpty(this.textInput.Text))
            {
                this.textInput.Text = "Sofia";
            }
            
            this.StartSearch();
        }

        private void OnSearchClick(object sender, RoutedEventArgs e)
        {
            this.StartSearch();
        }

        private void AddPolygon(LocationCollection list, bool useMultiColors)
        {
            PolygonData data = new PolygonData();
            if (useMultiColors)
            {
                data.ShapeFill = new MapShapeFill()
                {
                    Fill = new SolidColorBrush(Color.FromArgb(80, (byte)rndNumberGenerator.Next(0, 255), (byte)rndNumberGenerator.Next(0, 255), (byte)rndNumberGenerator.Next(0, 255))),
                    Stroke = brushes[rndNumberGenerator.Next(0, brushes.Length)],
                    StrokeThickness = 2,
                };
            }
            data.Points = list;
            this.vizLayer.Items.Add(data);
        }

        private void SearchByArea(SearchArea area)
        {
            int getAllPollygons = area.GetAllPolygons ? 1 : 0;
            int getMetadata = area.GetEntityMetadata ? 1 : 0;

            string searchURL = string.Format(GeodataHelper.BaseURL,
                                             area.Location.ToString(),
                                             area.LevelOfDetail,
                                             area.EntityType,
                                             getAllPollygons,
                                             getMetadata,
                                             area.Culture,
                                             area.UserRegion,
                                             area.BingKey);

            WebClient client = new WebClient();
            client.DownloadStringCompleted += Client_DownloadStringCompleted;
            client.DownloadStringAsync(new Uri(searchURL));
        }

        private void StartSearch()
        {
            BingRestSearchLocationRequest request = new BingRestSearchLocationRequest();
            request.Query = this.textInput.Text;
            this.bingProvider.SearchLocationAsync(request);
        }
    }   
}
