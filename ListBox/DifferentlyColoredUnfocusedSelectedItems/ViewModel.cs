using System.Collections.ObjectModel;
using System.Windows.Input;
using Telerik.Windows.Controls;

namespace DifferentlyColoredUnfocusedSelectedItems
{
    public class ViewModel : ViewModelBase
    {
        public ObservableCollection<string> CountryList { get; set; }

#if SILVERLIGHT
        public ICommand FocusChangedCommand { get; set; }

        private bool isListBoxFocused;

        public bool IsListBoxFocused
        {
            get
            {
                return this.isListBoxFocused;
            }
            set
            {
                if (this.isListBoxFocused != value)
                {
                    this.isListBoxFocused = value;
                    this.OnPropertyChanged(() => this.IsListBoxFocused);
                }
            }
        }
#endif

        public ViewModel()
        {
            this.InitializaCountryList();

#if SILVERLIGHT
            this.FocusChangedCommand = new DelegateCommand(OnFocusChangedCommandExecuted);
#endif
        }

        private void InitializaCountryList()
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

#if SILVERLIGHT
        private void OnFocusChangedCommandExecuted(object obj)
        {
            var eventName = obj.ToString();

            switch (eventName)
            {
                case "GotFocus":
                    this.IsListBoxFocused = true;
                    break;
                case "LostFocus":
                    this.IsListBoxFocused = false;
                    break;
            }
        }
#endif
    }
}
