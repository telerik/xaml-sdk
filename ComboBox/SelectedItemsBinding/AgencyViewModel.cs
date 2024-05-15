using System.Collections.ObjectModel;
using Telerik.Windows.Controls;

namespace SelectedItemsBinding
{
	public class AgencyViewModel : ViewModelBase
    {
        private ObservableCollection<string> agencies;
        private ObservableCollection<string> selectedAgencies;

        public AgencyViewModel()
        {
            this.SelectedAgencies = new ObservableCollection<string>() { this.Agencies[0], this.Agencies[1] };
        }

        public ObservableCollection<string> SelectedAgencies
        {
            get
            {
                return this.selectedAgencies;
            }
            set
            {
                if (this.selectedAgencies != value)
                {
                    this.selectedAgencies = value;
                    this.OnPropertyChanged(() => this.SelectedAgencies);
                }
            }
        }

        public ObservableCollection<string> Agencies
        {
            get
            {
                if (this.agencies == null)
                {
                    this.agencies = new ObservableCollection<string>();

                    this.agencies.Add("Item 1");
                    this.agencies.Add("Item 2");
                    this.agencies.Add("Item 3");
                    this.agencies.Add("Item 4");
                    this.agencies.Add("Item 5");
                    this.agencies.Add("Item 6");
                    this.agencies.Add("Item 7");
                    this.agencies.Add("Item 8");
                    this.agencies.Add("Item 9");
                }

                return this.agencies;
            }
        }
    }
}
