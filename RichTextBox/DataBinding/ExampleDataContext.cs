using System;
using System.ComponentModel;
using System.Linq;

namespace DataBinding
{
    public class ExampleDataContext : INotifyPropertyChanged
    {
        private string xamlData;

        public string XamlData
        {
            get
            {
                return this.xamlData;
            }
            set
            {
                if (value != this.xamlData)
                {
                    this.xamlData = value;
                    OnPropertyChanged("XamlData");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
