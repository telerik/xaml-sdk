using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;

namespace ComboboxColumnItemsSourceBinding
{
    public class Pilot : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string name;
        private int id;

        public string Name
        {
            get { return this.name; }
            set
            {
                if (value != this.name)
                {
                    this.name = value;
                    this.OnPropertyChanged("Name");
                }
            }
        }

        public int ID
        {
            get { return this.id; }
            set
            {
                if (value != this.id)
                {
                    this.id = value;
                    this.OnPropertyChanged("ID");
                }
            }
        }

        public Pilot()
        {

        }

        public Pilot(string name, int id)
        {
            this.name = name;
            this.id = id;
        }

        public static ObservableCollection<Pilot> GetPilots()
        {
            return new ObservableCollection<Pilot>(Team.GetTeams().SelectMany(c => c.Pilots));
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
