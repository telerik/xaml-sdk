using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;


namespace ComboboxColumnItemsSourceBinding
{
    public class MyViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Team> teams;
        private ObservableCollection<Pilot> players;
        private object selectedItem;

        public ObservableCollection<Team> Teams
        {
            get
            {
                if (this.teams == null)
                {
                    this.teams = Team.GetTeams();
                }

                return this.teams;
            }
        }

        public ObservableCollection<Pilot> Pilots
        {
            get
            {
                if (this.players == null)
                {
                    this.players = Pilot.GetPilots();
                }

                return this.players;
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

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, args);
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }
    }
}
