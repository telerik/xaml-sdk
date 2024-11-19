using System.Collections.ObjectModel;
using Telerik.Windows.Controls;

namespace DifferentlyColoredUnfocusedSelectedItems
{
    public class ViewModel : ViewModelBase
    {
        public ObservableCollection<string> CountryList { get; set; }

        public ViewModel()
        {
            this.InitializeCountryList();
        }

        private void InitializeCountryList()
        {
            this.CountryList = new ObservableCollection<string>();
            this.CountryList.Add("Ukraine");
            this.CountryList.Add("Sweden");
            this.CountryList.Add("France");
            this.CountryList.Add("England");
            this.CountryList.Add("Netherland");
            this.CountryList.Add("Russia");
            this.CountryList.Add("Denmark");
            this.CountryList.Add("Croatia");
            this.CountryList.Add("Czech Republic");
        }
    }
}
