using System;
using System.ComponentModel;

namespace CustomContextMenuBehavior
{
    public class Order : INotifyPropertyChanged
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
                    OnPropertyChanged("Date");
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
                    OnPropertyChanged("Product");
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
                    OnPropertyChanged("Quantity");
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
                    OnPropertyChanged("Net");
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
                    OnPropertyChanged("Promotion");
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
                    OnPropertyChanged("Advertisement");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
