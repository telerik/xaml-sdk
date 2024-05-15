using System;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace ComboboxColumn
{
    public class Pilot : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string firstName;
        private string lastName;
        private int countryId;

        public string FirstName
        {
            get { return this.firstName; }
            set
            {
                if (value != this.firstName)
                {
                    this.firstName = value;
                    this.OnPropertyChanged("FirstName");
                }
            }
        }

        public string LastName
        {
            get { return this.lastName; }
            set
            {
                if (value != this.lastName)
                {
                    this.lastName = value;
                    this.OnPropertyChanged("LastName");
                }
            }
        }

        public int CountryId
        {
            get { return this.countryId; }
            set
            {
                if (value != this.countryId)
                {
                    this.countryId = value;
                    this.OnPropertyChanged("CountryId");
                }
            }
        }

        public Pilot()
        {

        }

        public Pilot(string firstName, string lastName, int countryId)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.countryId = countryId;
        }

        public static ObservableCollection<Pilot> GetPilots()
        {
            ObservableCollection<Pilot> pilots = new ObservableCollection<Pilot>();
            Pilot pilot;

            // Vettel
            pilot = new Pilot("Sebastian", "Vettel", 0);
            pilots.Add(pilot);

            // Alonso
            pilot = new Pilot("Fernando", "Alonso", 1);
            pilots.Add(pilot);

            // Button
            pilot = new Pilot("James", "Button", 2);
            pilots.Add(pilot);

            return pilots;
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
