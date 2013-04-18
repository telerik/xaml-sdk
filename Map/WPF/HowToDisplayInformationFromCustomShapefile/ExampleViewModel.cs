using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Media;
using Telerik.Windows.Controls;

namespace HowToDisplayInformationFromCustomShapefile
{
    public class ExampleViewModel : ViewModelBase
    {
        private const string ShapeRelativeUriFormat = "/HowToDisplayInformationFromCustomShapefile;component/Resources/{0}";
        private const string StadiumViewImageUriFormat = "/HowToDisplayInformationFromCustomShapefile;component/Images/{0}";

        private Uri _stadiumViewImageUri;
        private Uri _stadiumSeatsSourceUri;
        private Uri _stadiumSeatsDataSourceUri;
        private Uri _playgroundLinesSourceUri;
        private Uri _playgroundGrassSourceUri;
        private Uri _playgroundFieldSourceUri;
        private ObservableCollection<TicketPriceInfo> _ticketPriceData;

        public ExampleViewModel()
        {
            this.StadiumSeatsSourceUri = new Uri(string.Format(ShapeRelativeUriFormat, "stadium_pol.shp"), UriKind.Relative);
            this.StadiumSeatsDataSourceUri = new Uri(string.Format(ShapeRelativeUriFormat, "stadium_pol.dbf"), UriKind.Relative);

            this.PlaygroundGrassSourceUri = new Uri(string.Format(ShapeRelativeUriFormat, "grass_pol.shp"), UriKind.Relative);
            this.PlaygroundLinesSourceUri = new Uri(string.Format(ShapeRelativeUriFormat, "playground_lin.shp"), UriKind.Relative);
            this.PlaygroundFieldSourceUri = new Uri(string.Format(ShapeRelativeUriFormat, "field_pol.shp"), UriKind.Relative);

            this.UpdateViewModel(null);
            this.InitializeData();
        }

        private void InitializeData()
        {
            ObservableCollection<TicketPriceInfo> dataCollection = new ObservableCollection<TicketPriceInfo>();
            dataCollection.Add(new TicketPriceInfo(new SolidColorBrush(Color.FromArgb(255, 22, 157, 215)), "North / South", 30, 15, 10));
            dataCollection.Add(new TicketPriceInfo(new SolidColorBrush(Color.FromArgb(255, 58, 181, 74)), "East / West", 25, 12, 7));
            dataCollection.Add(new TicketPriceInfo(new SolidColorBrush(Color.FromArgb(255, 247, 154, 33)), "NW / SE", 15, 10, 5));
            dataCollection.Add(new TicketPriceInfo(new SolidColorBrush(Color.FromArgb(255, 220, 87, 78)), "NE / SW", 15, 10, 5));

            this.Data = dataCollection;
        }

        public Uri StadiumSeatsSourceUri
        {
            get
            {
                return this._stadiumSeatsSourceUri;
            }
            set
            {
                if (this._stadiumSeatsSourceUri != value)
                {
                    this._stadiumSeatsSourceUri = value;
                    this.OnPropertyChanged("StadiumSeatsSourceUri");
                }
            }
        }

        public Uri StadiumSeatsDataSourceUri
        {
            get
            {
                return this._stadiumSeatsDataSourceUri;
            }
            set
            {
                if (this._stadiumSeatsDataSourceUri != value)
                {
                    this._stadiumSeatsDataSourceUri = value;
                    this.OnPropertyChanged("StadiumSeatsDataSourceUri");
                }
            }
        }

        public Uri PlaygroundLinesSourceUri
        {
            get
            {
                return this._playgroundLinesSourceUri;
            }
            set
            {
                if (this._playgroundLinesSourceUri != value)
                {
                    this._playgroundLinesSourceUri = value;
                    this.OnPropertyChanged("PlaygroundLinesSourceUri");
                }
            }
        }

        public Uri PlaygroundGrassSourceUri
        {
            get
            {
                return this._playgroundGrassSourceUri;
            }
            set
            {
                if (this._playgroundGrassSourceUri != value)
                {
                    this._playgroundGrassSourceUri = value;
                    this.OnPropertyChanged("PlaygroundGrassSourceUri");
                }
            }
        }

        public Uri PlaygroundFieldSourceUri
        {
            get
            {
                return this._playgroundFieldSourceUri;
            }
            set
            {
                if (this._playgroundFieldSourceUri != value)
                {
                    this._playgroundFieldSourceUri = value;
                    this.OnPropertyChanged("PlaygroundFieldSourceUri");
                }
            }
        }

        public Uri StadiumViewImageUri
        {
            get
            {
                return this._stadiumViewImageUri;
            }
            set
            {
                if (this._stadiumViewImageUri != value)
                {
                    this._stadiumViewImageUri = value;
                    this.OnPropertyChanged("StadiumViewImageUri");
                }
            }
        }

        public ObservableCollection<TicketPriceInfo> Data
        {
            get
            {
                return this._ticketPriceData;
            }
            set
            {
                if (this._ticketPriceData != value)
                {
                    this._ticketPriceData = value;
                    this.OnPropertyChanged("Data");
                }
            }
        }

        private TicketPriceInfo _selectedItem;
        public TicketPriceInfo SelectedItem
        {
            get
            {
                return this._selectedItem;
            }
            set
            {
                if (this._selectedItem != value)
                {
                    this._selectedItem = value;
                    this.OnPropertyChanged("SelectedItem");
                }
            }
        }

        public void UpdateViewModel(string sector)
        {
            this.UpdateStadiumViewImage(sector);
            this.UpdateSelectedItem(sector);
        }

        private void UpdateStadiumViewImage(string sector)
        {
            switch (sector)
            {
                case "north":
                case "south":
                    this.StadiumViewImageUri = new Uri(string.Format(StadiumViewImageUriFormat, "stadium_long_side.jpg"), UriKind.Relative);
                    break;
                case "east":
                case "west":
                    this.StadiumViewImageUri = new Uri(string.Format(StadiumViewImageUriFormat, "stadium_short_side.jpg"), UriKind.Relative);
                    break;
                case "SE angle":
                case "NE angle":
                    this.StadiumViewImageUri = new Uri(string.Format(StadiumViewImageUriFormat, "stadium_angle_1.jpg"), UriKind.Relative);
                    break;
                case "SW angle":
                case "NW angle":
                    this.StadiumViewImageUri = new Uri(string.Format(StadiumViewImageUriFormat, "stadium_angle_2.jpg"), UriKind.Relative);
                    break;
                default:
                    this.StadiumViewImageUri = new Uri(string.Format(StadiumViewImageUriFormat, "default.jpg"), UriKind.Relative);
                    break;
            }
        }

        private void UpdateSelectedItem(string sector)
        {
            switch (sector)
            {
                case "north":
                case "south":
                    this.SelectedItem = this.Data.Where(priceInfo => priceInfo.Sector == "North / South").FirstOrDefault();
                    break;
                case "east":
                case "west":
                    this.SelectedItem = this.Data.Where(priceInfo => priceInfo.Sector == "East / West").FirstOrDefault();
                    break;
                case "SE angle":
                case "NW angle":
                    this.SelectedItem = this.Data.Where(priceInfo => priceInfo.Sector == "NW / SE").FirstOrDefault();
                    break;
                case "SW angle":
                case "NE angle":
                    this.SelectedItem = this.Data.Where(priceInfo => priceInfo.Sector == "NE / SW").FirstOrDefault();
                    break;
                default:
                    this.SelectedItem = null;
                    break;
            }
        }
    }
}
