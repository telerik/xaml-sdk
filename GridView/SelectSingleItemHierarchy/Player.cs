using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SelectSingleItemHierarchy
{
    public class Player : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string name;
        private int number;
        private string country;

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

        public int Number
        {
            get { return this.number; }
            set
            {
                if (value != this.number)
                {
                    this.number = value;
                    this.OnPropertyChanged("Number");
                }
            }
        }

        public string Country
        {
            get { return this.country; }
            set
            {
                if (value != this.country)
                {
                    this.country = value;
                    this.OnPropertyChanged("Country");
                }
            }
        }

        public Player()
        {

        }

        public Player(string name, int number, string country)
        {
            this.name = name;
            this.number = number;
            this.country = country;
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

        public override string ToString()
        {
            return this.Name;
        }

        public static ObservableCollection<Player> GetPlayers()
        {
            return new ObservableCollection<Player>(Club.GetClubs().SelectMany(c => c.Players));
        }
    }
}
