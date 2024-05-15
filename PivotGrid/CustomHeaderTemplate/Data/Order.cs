using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RadPivotGrid_Data
{
    public class Order
    {
        public DateTime Date
        {
            get;
            set;
        }

        public string Product
        {
            get;
            set;
        }

        public int Quantity
        {
            get;
            set;
        }

        public double Net
        {
            get;
            set;
        }

        public string Promotion
        {
            get;
            set;
        }

        public string Advertisement
        {
            get;
            set;
        }
    }
}
