using System;
using System.Collections.ObjectModel;
using System.Windows.Navigation;
using System.Windows.Threading;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;

namespace PreserveSelectedItemScrollPosition
{
    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<Club> clubs;
        private Club selectedItem;
        private static Random random = new Random();
        private DispatcherTimer timer;
        private int counter = 1;

        public MainViewModel()
        {
            this.Clubs = Club.GetClubs();
            this.timer = new DispatcherTimer();
            this.timer.Interval = TimeSpan.FromSeconds(0.1);
            this.timer.Tick += (s, e) =>
            {
                this.AddItem();
                this.RemoveItem();
            };

            this.timer.Start();
        }

        private void AddItem()
        {
            var index = random.Next(0, this.Clubs.Count);
            this.Clubs.Insert(index, new Club("New Club " + counter++, DateTime.Now, 500));
        }

        private void RemoveItem()
        {
            var index = random.Next(0, this.Clubs.Count);
            if (index != this.Clubs.IndexOf(this.SelectedItem))
            {
                this.Clubs.RemoveAt(index);
            }
        }

        public ObservableCollection<Club> Clubs
        {
            get
            {
                return this.clubs;
            }
            set
            {
                if (this.clubs != value)
                {
                    this.clubs = value;
                    this.OnPropertyChanged("Clubs");
                }
            }
        }

        public Club SelectedItem
        {
            get { return this.selectedItem; }
            set
            {
                if (value != this.selectedItem)
                {
                    this.selectedItem = value;
                    this.OnPropertyChanged("SelectedItem");
                }
            }
        }
    }
}
