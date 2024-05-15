using System;
using Telerik.Windows.Controls;

namespace BindingLocalDataSourceProviderInMVVM
{
    public class Order : ViewModelBase
    {
        private DateTime date;
        private string product;
        private int quantity;
        private double net;
        private string promotion;
        private string advertisement;

        public DateTime Date
        {
            get
            {
                return this.date;
            }
            set
            {
                if (this.date != value)
                {
                    this.date = value;
                    this.OnPropertyChanged("Date");
                }
            }
        }

        public string Product
        {
            get
            {
                return this.product;
            }
            set
            {
                if (this.product != value)
                {
                    this.product = value;
                    this.OnPropertyChanged("Product");
                }
            }
        }

        public int Quantity
        {
            get
            {
                return this.quantity;
            }
            set
            {
                if (this.quantity != value)
                {
                    this.quantity = value;
                    this.OnPropertyChanged("Quantity");
                }
            }
        }

        public double Net
        {
            get
            {
                return this.net;
            }
            set
            {
                if (this.net != value)
                {
                    this.net = value;
                    this.OnPropertyChanged("Net");
                }
            }
        }

        public string Promotion
        {
            get
            {
                return this.promotion;
            }
            set
            {
                if (this.promotion != value)
                {
                    this.promotion = value;
                    this.OnPropertyChanged("Promotion");
                }
            }
        }

        public string Advertisement
        {
            get
            {
                return this.advertisement;
            }
            set
            {
                if (this.advertisement != value)
                {
                    this.advertisement = value;
                    this.OnPropertyChanged("Advertisement");
                }
            }
        }
    }
}
