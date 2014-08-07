using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using Telerik.Windows.Controls;

namespace EditorAttribute
{
    public class Player : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string name;
        private int number;
        private string country;
        private PhoneNumber phoneNumber;

        [Telerik.Windows.Controls.Data.PropertyGrid.Editor(typeof(TextBox), "Text", Telerik.Windows.Controls.Data.PropertyGrid.EditorStyle.Modal)]
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

        [Telerik.Windows.Controls.Data.PropertyGrid.Editor(typeof(RadNumericUpDown), "Value")]
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

        [Telerik.Windows.Controls.Data.PropertyGrid.Editor(typeof(PhoneEditorControl), Telerik.Windows.Controls.Data.PropertyGrid.EditorStyle.DropDown)]
        public PhoneNumber PhoneNumber
        {
            get
            {
                return this.phoneNumber;
            }
            set
            {
                if (this.phoneNumber != value)
                {
                    this.phoneNumber = value;
                    this.OnPropertyChanged("PhoneNumber");
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

    }
}
