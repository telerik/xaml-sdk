using System.Collections.Generic;
using System.Linq;

namespace DrillDown.ChartUtilities
{
    public class Company
    {
        public string CompanyName { get; set; }
        public List<Product> Products { get; set; }
        public double Revenue { get { return this.Products.Sum(p => p.CurrentPrice); } }
    }
}
