using System.Collections.Generic;
using System.Linq;

namespace Selection
{
    public class Category
    {
        private string categoryName;
        private List<Product> products;
        private double categoryTotalValue;

        public Category(string categoryName, List<Product> products)
        {
            this.categoryName = categoryName;
            this.products = products;
            this.categoryTotalValue = this.Products.Sum(product => product.TotalValue);
        }

        public string CategoryName
        {
            get
            {
                return this.categoryName;
            }
        }

        public List<Product> Products
        {
            get
            {
                return this.products;
            }
        }

        public double CategoryTotalValue
        {
            get
            {
                return this.categoryTotalValue;
            }
        }
    }
}
