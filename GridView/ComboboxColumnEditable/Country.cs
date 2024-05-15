using System;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace ComboboxColumn
{
    public class Country: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string name;
        private int id;

        public string Name
        {
            get { return this.name; }
            set
            {
                if (value != this.name)
                {
                    this.name = value;
                    this.OnPropertyChanged("Name");
                }
            }
        }

        public int Id
        {
            get { return this.id; }
            set
            {
                if (value != this.id)
                {
                    this.id = value;
                    this.OnPropertyChanged("Id");
                }
            }
        }

        public Country()
        {

        }

        public Country(string name, int id)
        {
            this.name = name;
            this.id = id;
        }

        public static ObservableCollection<Country> GetCountries()
        {
            ObservableCollection<Country> countries = new ObservableCollection<Country>();
            Country country;

            country = new Country("Germany", 0);
            countries.Add(country);

            country = new Country("Spain", 1);
            countries.Add(country);

            country = new Country("UK", 2);
            countries.Add(country);

            return countries;
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
