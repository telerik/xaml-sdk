using System;
using System.Linq;

namespace RadContextMenuAndRadGridViewMVVM
{
    public class Order
    {
        public DateTime Date { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
        public double Net { get; set; }
        public string Promotion { get; set; }
        public string Advertisement { get; set; }

        public override string ToString()
        {
            return this.Product + this.Promotion + this.Quantity;
        }
    }
}
