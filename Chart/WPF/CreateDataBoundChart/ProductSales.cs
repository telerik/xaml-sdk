using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CreateDataBoundChart
{
    public class ProductSales
    {
        public ProductSales(int quantity, int month, string monthName)
        {
            this.Quantity = quantity;
            this.Month = month;
            this.MonthName = monthName;
        }

        public int Quantity { get; set; }

        public int Month { get; set; }
        
        public string MonthName { get; set; }
    }
}