using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;


namespace ComboboxColumn
{
    public class MyViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Pilot> pilots;
        private ObservableCollection<Country> countries;

        public ObservableCollection<Pilot> Pilots
        {
            get
            {
                if (this.pilots == null)
                {
                    this.pilots = Pilot.GetPilots();
                }

                return this.pilots;
            }
        }

        public ObservableCollection<Country> Countries
        {
            get
            {
                if (this.countries == null)
                {
                    this.countries = Country.GetCountries();
                }

                return this.countries;
            }
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, args);
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }
    }
}
