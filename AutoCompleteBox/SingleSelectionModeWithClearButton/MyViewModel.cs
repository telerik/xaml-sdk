using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Telerik.Windows.Controls;

namespace SingleSelectionModeWithClearButton
{
    public class MyViewModel : ViewModelBase
    {      
        private Country selectedItem;
        private Visibility crossButtonVisibility;

        public MyViewModel()
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

            this.ClearSelectionCommand = new DelegateCommand(OnClearSelectionCommandExecuted);
            this.CrossButtonVisibility = Visibility.Collapsed;
        }

        public ObservableCollection<Country> Countries { get; set; }
        public ICommand ClearSelectionCommand { get; set; }       

        public Visibility CrossButtonVisibility
        {
            get
            {
                return this.crossButtonVisibility;
            }

            set
            {
                if (this.crossButtonVisibility != value)
                {
                    this.crossButtonVisibility = value;
                    this.OnPropertyChanged(() => this.CrossButtonVisibility);
                }
            }
        }

        public Country SelectedItem
        {
            get
            {
                return this.selectedItem;
            }

            set
            {
                if (this.selectedItem != value)
                {
                    this.selectedItem = value;
                    this.OnPropertyChanged(() => this.SelectedItem);

                    if (value != null)
                    {
                        this.CrossButtonVisibility = Visibility.Visible;
                    }
                    else
                    {
                        this.CrossButtonVisibility = Visibility.Collapsed;
                    }
                }
            }
        }

        private void OnClearSelectionCommandExecuted(object obj)
        {
            this.SelectedItem = null;
        }
    }
}
