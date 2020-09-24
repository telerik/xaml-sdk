using System;
using System.Collections.Generic;

namespace CreatePdfUsingRadFixedDocumentEditor
{
    public class Product
    {
        public Product(string name, params double[] q)
        {
            this.Name = name;
            this.Q = q;
        }

        public string Name
        {
            get; set;
        }
        public double[] Q
        {
            get; set;
        }

        public static List<Product> GetProducts(int count)
        {
            List<Product> products = new List<Product>();

            Random r = new Random();

            Func<int> getRandom = () =>
            {
                return r.Next(7000, 30000);
            };

            for (int i = 0; i < count; i++)
            {
                products.Add(new Product(String.Format("Product{0}", i + 1), getRandom(), getRandom(), getRandom(), getRandom()));
            }

            return products;
        }
    }
}
