using System;
using System.Collections.Generic;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Map;
using System.Xml;
using System.Windows;
using System.Globalization;
using System.Windows.Resources;

namespace UI_Virtualization_And_Wraparound
{
    public class ExampleViewModel : ViewModelBase
    {
        private MapProviderBase mapProvider;

        public ExampleViewModel()
        {
            SetProvider();

            this.ZoomLevel = 3;
            this.Center = new Location(37.684297, -99.06924);
        }

        private void SetProvider()
        {
            OpenStreetMapProvider provider = new OpenStreetMapProvider()
            {
                // This user agent should be set per application.
                // Please specify different string in your application.
                StandardModeUserAgent = "Telerik UI for WPF SDK samples"
            };
            provider.Layer = OpenStreetMapLayer.Humanitarian;

            this.mapProvider = provider;
        }

        public MapProviderBase Provider
        {
            get
            {
                return this.mapProvider;
            }
        }

        private int _zoomLevel;
        public int ZoomLevel
        {
            get
            {
                return this._zoomLevel;
            }
            set
            {
                if (this._zoomLevel != value)
                {
                    this._zoomLevel = value;
                    this.OnPropertyChanged("ZoomLevel");
                }
            }
        }

        private Location _center;
        public Location Center
        {
            get
            {
                return this._center;
            }
            set
            {
                if (this._center != value)
                {
                    this._center = value;
                    this.OnPropertyChanged("Center");
                }
            }
        }        
        internal List<StoreLocation> GetStores(double upperLeftLat,double upperLeftLong, double lowerRightLat, double lowerRightLong, StoreType storeType)
        { 
            List<StoreLocation> locations = new List<StoreLocation>();

            StreamResourceInfo streamInfo = Application.GetResourceStream(
                new Uri("/UI_Virtualization_And_Wraparound;component/StoresLocation.xml", UriKind.Relative));

            XmlDocument document = new XmlDocument();
            document.Load(streamInfo.Stream);

            string latLonCondition = "[number(@Latitude) < " + Math.Round(upperLeftLat, 5).ToString(CultureInfo.InvariantCulture) +
                " and number(@Latitude) > " + Math.Round(lowerRightLat, 5).ToString(CultureInfo.InvariantCulture) +
                " and number(@Longitude) > " + Math.Round(upperLeftLong, 5).ToString(CultureInfo.InvariantCulture) +
                " and number(@Longitude) < " + Math.Round(lowerRightLong, 5).ToString(CultureInfo.InvariantCulture) + "]";

            switch (storeType)
            {
                case StoreType.Area:
                    {
                        XmlNodeList nodeList = document.SelectNodes("/StoresLocation/Area" + latLonCondition);
                        foreach (XmlNode node in nodeList)
                        {
                            XmlElement element = (XmlElement)node;

                            locations.Add(new StoreLocation(
                                Convert.ToDouble(element.GetAttribute("Latitude"), CultureInfo.InvariantCulture),
                                Convert.ToDouble(element.GetAttribute("Longitude"), CultureInfo.InvariantCulture),
                                element.GetAttribute("Name"), StoreType.Area));
                        }
                    }
                    break;

                case StoreType.Store:
                    {
                        XmlNodeList nodeList = document.SelectNodes("/StoresLocation/Area/*" + latLonCondition);
                        foreach (XmlNode node in nodeList)
                        {
                            XmlElement element = (XmlElement)node;

                            locations.Add(new StoreLocation(
                                Convert.ToDouble(element.GetAttribute("Latitude"), CultureInfo.InvariantCulture),
                                Convert.ToDouble(element.GetAttribute("Longitude"), CultureInfo.InvariantCulture),
                                element.GetAttribute("Name"),
                                element.LocalName == "Market" ? StoreType.Market : StoreType.Store));
                        }
                    }
                    break;
            }

            return locations;
        }

        /// <summary>
        /// Callback of the GetStores async call.
        /// The method uses the web service response to building objects on the dynamic layer.
        /// </summary>
		internal void SetStores(List<StoreLocation> list, MapItemsRequestEventArgs request)
        {
            if (list.Count == 0)
                return;
 
            foreach (StoreLocation storeLocation in list)
            {
                // Shifts the store to the current portion of the request.
                storeLocation.Longitude = this.TryGetLongitudeMatchInRange(storeLocation.Longitude, request.UpperLeft.Longitude, request.LowerRight.Longitude);
            }
            
			request.CompleteItemsRequest(list);
        }

        private double TryGetLongitudeMatchInRange(double longitude, double left, double right)
        {
            if (left <= longitude && longitude <= right)
                return longitude;

            double range = this.mapProvider.SpatialReference.MaxLongitude - this.mapProvider.SpatialReference.MinLongitude;

            while (longitude < left)
            {
                longitude += range;
            }
            while (longitude > right)
            {
                longitude -= range;
            }

            return longitude;
        }
    }
}
