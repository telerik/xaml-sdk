using System;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace FilterSearchSort.ViewModels
{
    public class Category : INotifyPropertyChanged
    {
        public Category(string name)
        {
            this.Name = name;
            this.products = new ObservableCollection<Product>();
        }
        public string Name { get; set; }

        public string Path
        {
            get
            {
                return this.Name.ToString();
            }
        }

        private ObservableCollection<Product> products;
        public ObservableCollection<Product> Products
        {
            get
            {
                return this.products;
            }
            set
            {
                this.products = value;
                this.OnPropertyChanged("Products");
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
