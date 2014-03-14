using System;
using System.ComponentModel;
using System.Linq;

namespace GenerateDocuments.SampleData
{
    public class Product : INotifyPropertyChanged
    {
        private int id;
        private string name;
        private double unitPrice;
        private int quantity;
        private DateTime date;
        private double subTotal;

        public Product(int id, string name, double unitPrice, int quantity, DateTime date)
        {
            this.ID = id;
            this.Name = name;
            this.UnitPrice = unitPrice;
            this.Quantity = quantity;
            this.Date = date;
            this.SubTotal = this.quantity * this.unitPrice;
        }

        public int ID
        {
            get
            {
                return this.id;
            }
            set
            {
                if (this.id != value)
                {
                    this.id = value;
                    this.OnPropertyChanged("ID");
                }
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (this.name != value)
                {
                    this.name = value;
                    this.OnPropertyChanged("Name");
                }
            }
        }

        public double UnitPrice
        {
            get
            {
                return this.unitPrice;
            }
            set
            {
                if (this.unitPrice != value)
                {
                    this.unitPrice = value;
                    this.OnPropertyChanged("UnitPrice");
                }
            }
        }

        public int Quantity
        {
            get { return this.quantity; }
            set
            {
                if (this.quantity != value)
                {
                    this.quantity = value;
                    this.OnPropertyChanged("Quantity");
                }
            }
        }

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

        public double SubTotal
        {
            get { return this.subTotal; }
            set
            {
                if (this.subTotal != value)
                {
                    this.subTotal = value;
                    this.OnPropertyChanged("SubTotal");
                }
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