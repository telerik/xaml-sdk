using System.Collections.Generic;

namespace DrillDown.ChartUtilities
{
    public class Product
    {
        public string ProductName { get; set; }
        public double CurrentPrice { get; set; }

        public List<PriceInfo> PricesInfo { get; set; }
    }
}
