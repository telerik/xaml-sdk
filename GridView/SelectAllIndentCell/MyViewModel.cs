using System.Collections.ObjectModel;
using Telerik.Windows.Controls;

namespace SelectAllIndentCell
{
    public class MyViewModel : ViewModelBase
    {
        private ObservableCollection<Club> clubs;

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
    }
}
