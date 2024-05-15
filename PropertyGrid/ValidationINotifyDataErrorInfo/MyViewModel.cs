using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Telerik.Windows.Controls;

namespace INotifyDataErrorInfoSupport
{
    public class MyViewModel : ViewModelBase
    {
        private ObservableCollection<Club> clubs;
        private object selectedItem;

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

        public object SelectedItem
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
