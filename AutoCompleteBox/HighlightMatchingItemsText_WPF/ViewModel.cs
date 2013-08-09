using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Telerik.Windows.Controls;

namespace HighlightMatchingItemsText
{
    public class ViewModel : ViewModelBase
    {
        private ObservableCollection<Country> countries;

        public ViewModel()
        {
            this.Countries = new ObservableCollection<Country>()
            {
                new Country() { Name = "Australia", Capital = "Canberra" },
                new Country() { Name = "Bulgaria", Capital = "Sofia" },
                new Country() { Name = "Canada", Capital = "Ottawa" },
                new Country() { Name = "Denmark", Capital = "Copenhagen" },
                new Country() { Name = "France", Capital = "Paris" },
                new Country() { Name = "Germany", Capital = "Berlin" },
                new Country() { Name = "India", Capital = "New Delhi" },
                new Country() { Name = "Italy", Capital = "Rome" },
                new Country() { Name = "Norway", Capital = "Oslo" },
                new Country() { Name = "Russia", Capital = "Moscow" },
                new Country() { Name = "Spain ", Capital = "Madrid" },
                new Country() { Name = "United Kingdom", Capital = "London" },
                new Country() { Name = "United States", Capital = "Washington, D.C." },
            };
        }

        public ObservableCollection<Country> Countries
        {
            get { return this.countries; }
            set
            {
                if (this.countries != value)
                {
                    this.countries = value;
                    this.OnPropertyChanged(() => this.Countries);
                }
            }
        }
    }
}
