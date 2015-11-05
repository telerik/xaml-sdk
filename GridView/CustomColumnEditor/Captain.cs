using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace CustomColumnEditor
{
    public class Captain : INotifyPropertyChanged
    {
        private string name;
        private Position position;

        public Captain(string name, Position position)
        {
            this.name = name;
            this.position = position;
        }

        public string Name
        {
            get { return this.name; }
            set
            {
                if (this.name != value)
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
                if (this.position != value)
                {
                    this.position = value;
                    this.OnPropertyChanged("Position");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

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
