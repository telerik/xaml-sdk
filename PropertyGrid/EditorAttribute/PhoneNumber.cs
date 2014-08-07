using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace EditorAttribute
{
    public class PhoneNumber : INotifyPropertyChanged
    {
        private string countryCode;
        public string CountryCode
        {
            get
            {
                return this.countryCode;
            }
            set
            {
                if (this.countryCode != value)
                {
                    this.countryCode = value;
                    this.OnPropertyChanged("CountryCode");
                }
            }
        }

        private string number;
        public string Number
        {
            get
            {
                return this.number;
            }
            set
            {
                if (this.number != value)
                {
                    this.number = value;
                    this.OnPropertyChanged("Number");
                }
            }
        }

        private string regionCode;
        public string RegionCode
        {
            get
            {
                return this.regionCode;
            }
            set
            {
                if (this.regionCode != value)
                {
                    this.regionCode = value;
                    this.OnPropertyChanged("RegionCode");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
