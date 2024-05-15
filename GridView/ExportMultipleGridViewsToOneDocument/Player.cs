using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace ExportMultipleGridViewsToOneDocument
{
    public class Player : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string name;
        private Position position;

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

        public Position Position
        {
            get { return this.position; }
            set
            {
                if (value != this.position)
                {
                    this.position = value;
                    this.OnPropertyChanged("Position");
                }
            }
        }

        public Player(string name, Position position)
        {
            this.name = name;
            this.position = position;
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            if (null != this.PropertyChanged)
            {
                this.PropertyChanged(this, args);
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        internal static ObservableCollection<Player> GetPlayers()
        {
            var players = new ObservableCollection<Player>();

            players.Add(new Player("Pepe Reina", Position.GK));
            players.Add(new Player("Jamie Carragher", Position.DF));
            players.Add(new Player("Steven Gerrard", Position.MF));
            players.Add(new Player("Fernando Torres", Position.FW));

            players.Add(new Player("Edwin van der Sar", Position.GK));
            players.Add(new Player("Rio Ferdinand", Position.DF));
            players.Add(new Player("Ryan Giggs", Position.MF));
            players.Add(new Player("Wayne Rooney", Position.FW));

            players.Add(new Player("Petr Čech", Position.GK));
            players.Add(new Player("John Terry", Position.DF));
            players.Add(new Player("Frank Lampard", Position.MF));
            players.Add(new Player("Nicolas Anelka", Position.FW));

            return players;
        }
    }

    public enum Position
    {
        /// <summary>
        /// In Silverlight, you can use the DisplayAttribute.ShortName data annotation as well.
        /// </summary>
        [Description("Goalkeeper")]
        GK,

        [Description("Defender")]
        DF,

        [Description("Midfield")]
        MF,

        [Description("Forward")]
        FW
    }
}
