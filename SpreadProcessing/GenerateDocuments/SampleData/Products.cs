using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenerateDocuments.SampleData
{
    public class Products
    {
        static readonly string[] names = new string[] { "Côte de Blaye", "Boston Crab Meat", 
            "Singaporean Hokkien Fried Mee", "Gula Malacca", "Rogede sild", 
            "Spegesild", "Zaanse koeken", "Chocolade", "Maxilaku", "Valkoinen suklaa" };

        static readonly double[] prizes = new double[] { 23.2500, 9.0000, 45.6000, 32.0000, 
            14.0000, 19.0000, 263.5000, 18.4000, 3.0000, 14.0000 };

        static readonly DateTime[] dates = new DateTime[] { new DateTime(2007, 5, 10), new DateTime(2008, 9, 13), 
            new DateTime(2008, 2, 22), new DateTime(2009, 1, 2), new DateTime(2007, 4, 13), 
            new DateTime(2008, 5, 12), new DateTime(2008, 1, 19), new DateTime(2008, 8, 26), 
            new DateTime(2008, 7, 31), new DateTime(2007, 7, 16) };


        public IEnumerable<Product> GetData(int itemCount)
        {
            Random rnd = new Random();

            return from i in Enumerable.Range(1, itemCount)
                   select new Product(i,
                       names[rnd.Next(9)],
                       prizes[rnd.Next(9)],
                       rnd.Next(1, 9),
                       dates[rnd.Next(9)]);
        }
    }
}