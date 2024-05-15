using System;
using System.Linq;
using System.Windows.Controls;
using System.Collections.Generic;
using Telerik.Windows.Controls;
using Telerik.Windows;
using System.ComponentModel;
using CascadingComboBoxColumns;

namespace SilverlightApplication1
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();

            List<Location> locations = new List<Location>();

            locations.Add(new Location() { ContinentCode = "EU", CountryID = 2 });


            ((GridViewComboBoxColumn)this.radGridView.Columns[0]).ItemsSource = Locations.Continents;
            this.radGridView.ItemsSource = locations;

            //We need to sense when the combo selection is changed and submit the value immediately
            //otherwise the value will be submited on leaving the cell which is too late for our needs. 
            this.AddHandler(RadComboBox.SelectionChangedEvent, new Telerik.Windows.Controls.SelectionChangedEventHandler(comboSelectionChanged));
        }
        void comboSelectionChanged(object sender, RadRoutedEventArgs args)
        {
            RadComboBox comboBox = (RadComboBox)args.OriginalSource;

            if (comboBox.SelectedValue == null
                || comboBox.SelectedValuePath != "Code") // we take action only if the continent combo is changed
                return;

            Location location = comboBox.DataContext as Location;
            location.ContinentCode = (string)comboBox.SelectedValue;//we submit the value immediately rather than waiting the cell to lose focus.
        }
    }


    public class Location : INotifyPropertyChanged
    {

        private string continentCode;
        public string ContinentCode
        {
            get
            {
                return this.continentCode;
            }
            set
            {
                if (value != this.continentCode)
                {
                    this.continentCode = value;
                    this.OnPropertyChanged("ContinentCode");
                    this.CountryID = null;
                }
            }
        }
        private int? countryID;
        public int? CountryID
        {
            get
            {
                return this.countryID;
            }
            set
            {
                this.countryID = value;
                this.OnPropertyChanged("CountryID");
            }
        }

        public IEnumerable<Country> AvailableCountries
        {
            get
            {
                return from c in Locations.Countries
                       where c.ContinentCode == this.ContinentCode
                       select c;
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}