using System.Collections.Generic;
using System.Linq;
using Telerik.Windows.Controls;

namespace DrillDown.ChartUtilities
{
    public class Company : ViewModelBase
    {
        public string CompanyName { get; set; }
        public List<Product> Products { get; set; }
        public double Revenue { get { return this.Products.Sum(p => p.CurrentPrice); } }
    }
}
