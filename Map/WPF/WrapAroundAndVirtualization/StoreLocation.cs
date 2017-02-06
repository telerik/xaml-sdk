using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Map;

namespace UI_Virtualization_And_Wraparound
{
    public class StoreLocation : ViewModelBase
    {
        private double _lattitude;
        private double _longitude;
        private string _storeName;
        private StoreType _storeType;

        /// <summary>
        /// Initializes a new instance of the StoreLocation class.
        /// </summary>
        public StoreLocation()
		{
		}

		/// <summary>
		/// Initializes a new instance of the StoreLocation class.
		/// </summary>
		/// <param name="latitude">Latitude.</param>
		/// <param name="longitude">Longitude.</param>
		/// <param name="storeName">Store name.</param>
		/// <param name="storeType">Store type.</param>
		public StoreLocation(double latitude, double longitude, string storeName, StoreType storeType)
		{
			this.Latitude = latitude;
			this.Longitude = longitude;
			this.StoreName = storeName;
			this.StoreType = storeType;
		}

        public double Latitude
        {
            get
            {
                return this._lattitude;
            }
            set
            {
                Location old = this.Location;
                this._lattitude = value;
				this.OnPropertyChanged("Latitude");
				this.OnPropertyChanged("Location");
			}
        }

        public double Longitude
        {
            get
            {
                return this._longitude;
            }
            set
            {
                Location old = this.Location;

                this._longitude = value;
				this.OnPropertyChanged("Longitude");
				this.OnPropertyChanged("Location");
            }
        }

		public Location Location
		{
			get
			{
				return new Location(this.Latitude, this.Longitude);
			}
		}

        public string StoreName
        {
            get
            {
                return this._storeName;
            }
            set
            {
                this._storeName = value;
				this.OnPropertyChanged("StoreName");
			}
        }

        public StoreType StoreType
        {
            get
            {
                return this._storeType;
            }
            set
            {
                this._storeType = value;
				this.OnPropertyChanged("StoreType");
			}
        }
    }

    public enum StoreType
    {
        Area,
        Market,
        Store
    }
}
