using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;

namespace ComboboxColumnItemsSourceBinding
{
    public class Team : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string name;
        private int driverId;
        private ObservableCollection<Pilot> pilots;

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

        public int DriverID
        {
            get { return this.driverId; }
            set
            {
                if (value != this.driverId)
                {
                    this.driverId = value;
                    this.OnPropertyChanged("DriverID");
                    this.OnPropertyChanged("DriverName");//Tells the UI to update the DriverName bound columns
                }
            }
        }

        /// <summary>
        /// Driver name is required for filtering
        /// </summary>
        public string DriverName
        {
            get
            {
                var selectedPilot = pilots.Where(p => p.ID == DriverID).FirstOrDefault();
                return selectedPilot != null ? selectedPilot.Name : string.Empty;
            }
        }

        public ObservableCollection<Pilot> Pilots
        {
            get
            {
                if (null == this.pilots)
                {
                    this.pilots = new ObservableCollection<Pilot>();
                }

                return this.pilots;
            }
        }

        public Team()
        {

        }

        public Team(string name, int driverId)
        {
            this.name = name;
            this.driverId = driverId;
        }

        public Team(string name, int driverId, ObservableCollection<Pilot> pilots)
            : this(name, driverId)
        {
            this.pilots = pilots;
        }

        public static ObservableCollection<Team> GetTeams()
        {
            ObservableCollection<Team> teams = new ObservableCollection<Team>();
            Team team;

            // Ferrari
            team = new Team("Ferrari", 0);
            team.Pilots.Add(new Pilot("K.Raikkonen", 0));
            team.Pilots.Add(new Pilot("F. Massa", 1));
            teams.Add(team);

            // Renault
            team = new Team("Renault", 1);
            team.Pilots.Add(new Pilot("S. Vettel", 0));
            team.Pilots.Add(new Pilot("M. Webber", 1));
            teams.Add(team);

            // Mercedes
            team = new Team("Mercedes", 0);
            team.Pilots.Add(new Pilot("J. Button", 0));
            team.Pilots.Add(new Pilot("R. Barrichello", 1));
            teams.Add(team);


            return teams;
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
