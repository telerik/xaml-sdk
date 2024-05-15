using System.Collections.ObjectModel;
using System.Windows.Input;
using Telerik.Windows.Controls;

namespace BoundSelectColumn
{
    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<Club> clubs;

        public ICommand SelectItemsCommand { get; set; }

        public MainViewModel()
        {
            this.SelectItemsCommand = new DelegateCommand(SelectItems);
        }

        public ObservableCollection<Club> Clubs
        {
            get
            {
                if (this.clubs == null)
                {
                    this.clubs = Club.GetClubs();
                }

                return this.clubs;
            }
        }

        private void SelectItems(object obj)
        {
            this.Clubs[2].IsSelected = true;
            this.Clubs[3].IsSelected = true;
        }
    }
}
