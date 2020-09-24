using System;

namespace GenerateDocuments.SampleData
{
    public class Product
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
                }
            }
        }

        public double SubTotal
        {
            get
            {
                return this.subTotal;
            }
            set
            {
                if (this.subTotal != value)
                {
                    this.subTotal = value;
                }
            }
        }
    }
}