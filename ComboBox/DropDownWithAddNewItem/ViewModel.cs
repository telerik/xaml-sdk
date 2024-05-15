using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Telerik.Windows.Controls;

namespace DropDownWithAddNewItem
{
    public class ViewModel : ViewModelBase
    {
        private bool _isAddingItemEnabled = false;

        private string _currentText;

        private int _selectedIndex = -1;

        public ViewModel()
        {
            this.Countries = new ObservableCollection<string>()
            {
                "Germany",
                "England",
                "Belgium",
                "Spain",
                "Bulgaria",
                "Argentina",
                "Canada"
            };

            this.AddNewItemCommand = new DelegateCommand(AddNewItemCommandExecuted);
        }

        public ICommand AddNewItemCommand { get; set; }

        public ObservableCollection<string> Countries { get; set; }

        public bool IsAddingItemEnabled
        {
            get { return this._isAddingItemEnabled; }

            set
            {
                if( this._isAddingItemEnabled != value)
                {
                    this._isAddingItemEnabled = value;
                    this.OnPropertyChanged("IsAddingItemEnabled");
                }
            }
        }

        public string CurrentText
        {
            get { return this._currentText; }

            set
            {
                if (this._currentText != value)
                {
                    this._currentText = value;
                    if (this._currentText.Count() > 0 && this.SelectedIndex == -1)
                    {
                        this.IsAddingItemEnabled = true;
                    }
                    else this.IsAddingItemEnabled = false;

                }
            }
        }

        public int SelectedIndex
        {
            get { return this._selectedIndex; }

            set
            {
                if (this._selectedIndex != value)
                {
                    this._selectedIndex = value;
                }
            }
        }

        protected void AddNewItemCommandExecuted(object param)
        {
            this.Countries.Add(this.CurrentText);         
        }

    }
}
