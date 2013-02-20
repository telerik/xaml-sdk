using System.ComponentModel;
using System.Windows.Media;

namespace HowToDisplayInformationFromCustomShapefile
{
    public class TicketPriceInfo : INotifyPropertyChanged
    {
        private Brush _colorCode;
        private string _sector;
        private double _adultPrice;
        private double _seniorPrice;
        private double _juniorPrice;

        public event PropertyChangedEventHandler PropertyChanged;

        public TicketPriceInfo(Brush c, string s, double ap, double sp, double jp)
        {
            this.ColorCode = c;
            this.Sector = s;
            this.AdultPrice = ap;
            this.SeniorPrice = sp;
            this.JuniorPrice = jp;
        }

        public Brush ColorCode
        {
            get
            {
                return this._colorCode;
            }
            set
            {
                if (this._colorCode != value)
                {
                    this._colorCode = value;
                    this.OnPropertyChanged(new PropertyChangedEventArgs("ColorCode"));
                }
            }
        }

        public string Sector
        {
            get
            {
                return this._sector;
            }
            set
            {
                if (this._sector != value)
                {
                    this._sector = value;
                    this.OnPropertyChanged(new PropertyChangedEventArgs("Sector"));
                }
            }
        }

        public double AdultPrice
        {
            get
            {
                return this._adultPrice;
            }
            set
            {
                if (this._adultPrice != value)
                {
                    this._adultPrice = value;
                    this.OnPropertyChanged(new PropertyChangedEventArgs("AdultPrice"));
                }
            }
        }

        public double SeniorPrice
        {
            get
            {
                return this._seniorPrice;
            }
            set
            {
                if (this._seniorPrice != value)
                {
                    this._seniorPrice = value;
                    this.OnPropertyChanged(new PropertyChangedEventArgs("SeniorPrice"));
                }
            }
        }

        public double JuniorPrice
        {
            get
            {
                return this._juniorPrice;
            }
            set
            {
                if (this._juniorPrice != value)
                {
                    this._juniorPrice = value;
                    this.OnPropertyChanged(new PropertyChangedEventArgs("JuniorPrice"));
                }
            }
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, args);
        }
    }
}
