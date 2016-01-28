using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace WpfApplication1
{
    public class CustomString : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        string _value;

        public CustomString()
        {

        }

        public CustomString(string value)
        {
            this._value = value;
        }

        public static implicit operator string(CustomString s)
        {
            return s.ToString();
        }

        public static implicit operator CustomString(string s)
        {
            return new CustomString(s);
        }

        public string Value
        {
            get { return this._value; }
            set
            {
                if (this._value != value)
                {
                    this._value = value;
                    this.OnPropertyChanged("Value");
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

        public override string ToString()
        {
            return _value.ToString();
        }
    }
}
