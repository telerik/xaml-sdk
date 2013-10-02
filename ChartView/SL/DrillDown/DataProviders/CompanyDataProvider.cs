using System.Collections.Generic;

namespace DrillDown.ChartUtilities
{
    public class CompanyDataProvider
    {
        public static Company GetCompany1Data()
        {
            Product p1 = new Product();
            p1.ProductName = "Pc";
            p1.CurrentPrice = 100;
            p1.PricesInfo = new List<PriceInfo> 
            { 
                new PriceInfo { Year = 2005, Price = 80 },
                new PriceInfo { Year = 2006, Price = 95 },
                new PriceInfo { Year = 2007, Price = 93 },
                new PriceInfo { Year = 2008, Price = 104 },
                new PriceInfo { Year = 2009, Price = 99 },
                new PriceInfo { Year = 2010, Price = 108 },
                new PriceInfo { Year = 2011, Price = 107 },
                new PriceInfo { Year = 2012, Price = 101 },
            };

            Product p2 = new Product();
            p2.ProductName = "Laptop";
            p2.CurrentPrice = 200;
            p2.PricesInfo = new List<PriceInfo> 
            { 
                new PriceInfo { Year = 2005, Price = 180 },
                new PriceInfo { Year = 2006, Price = 195 },
                new PriceInfo { Year = 2007, Price = 193 },
                new PriceInfo { Year = 2008, Price = 184 },
                new PriceInfo { Year = 2009, Price = 199 },
                new PriceInfo { Year = 2010, Price = 228 },
                new PriceInfo { Year = 2011, Price = 217 },
                new PriceInfo { Year = 2012, Price = 201 },
            };

            Product p3 = new Product();
            p3.ProductName = "Monitor";
            p3.CurrentPrice = 600;
            p3.PricesInfo = new List<PriceInfo> 
            { 
                new PriceInfo { Year = 2005, Price = 580 },
                new PriceInfo { Year = 2006, Price = 595 },
                new PriceInfo { Year = 2007, Price = 593 },
                new PriceInfo { Year = 2008, Price = 584 },
                new PriceInfo { Year = 2009, Price = 599 },
                new PriceInfo { Year = 2010, Price = 628 },
                new PriceInfo { Year = 2011, Price = 617 },
                new PriceInfo { Year = 2012, Price = 601 },
            };

            Company company1 = new Company();
            company1.CompanyName = "X Corporation";
            company1.Products = new List<Product> { p1, p2, p3 };

            return company1;
        }

        public static Company GetCompany2Data()
        {
            Product p1 = new Product();
            p1.ProductName = "Bike";
            p1.CurrentPrice = 10;
            p1.PricesInfo = new List<PriceInfo> 
            { 
                new PriceInfo { Year = 2005, Price = 10 },
                new PriceInfo { Year = 2006, Price = 15 },
                new PriceInfo { Year = 2007, Price = 13 },
                new PriceInfo { Year = 2008, Price = 14 },
                new PriceInfo { Year = 2009, Price = 19 },
                new PriceInfo { Year = 2010, Price = 18 },
                new PriceInfo { Year = 2011, Price = 17 },
                new PriceInfo { Year = 2012, Price = 11 },
            };

            Product p2 = new Product();
            p2.ProductName = "Car";
            p2.CurrentPrice = 400;
            p2.PricesInfo = new List<PriceInfo> 
            { 
                new PriceInfo { Year = 2005, Price = 380 },
                new PriceInfo { Year = 2006, Price = 395 },
                new PriceInfo { Year = 2007, Price = 393 },
                new PriceInfo { Year = 2008, Price = 384 },
                new PriceInfo { Year = 2009, Price = 399 },
                new PriceInfo { Year = 2010, Price = 328 },
                new PriceInfo { Year = 2011, Price = 317 },
                new PriceInfo { Year = 2012, Price = 361 },
            };

            Product p3 = new Product();
            p3.ProductName = "Airplane";
            p3.CurrentPrice = 160;
            p3.PricesInfo = new List<PriceInfo> 
            { 
                new PriceInfo { Year = 2005, Price = 158 },
                new PriceInfo { Year = 2006, Price = 159 },
                new PriceInfo { Year = 2007, Price = 159 },
                new PriceInfo { Year = 2008, Price = 158 },
                new PriceInfo { Year = 2009, Price = 159 },
                new PriceInfo { Year = 2010, Price = 162 },
                new PriceInfo { Year = 2011, Price = 161 },
                new PriceInfo { Year = 2012, Price = 160 },
            };

            Company company2 = new Company();
            company2.CompanyName = "Air Airlines";
            company2.Products = new List<Product> { p1, p2, p3 };

            return company2;
        }

        public static Company GetCompany3Data()
        {
            Product p1 = new Product();
            p1.ProductName = "Wood";
            p1.CurrentPrice = 70;
            p1.PricesInfo = new List<PriceInfo> 
            { 
                new PriceInfo { Year = 2005, Price = 60 },
                new PriceInfo { Year = 2006, Price = 65 },
                new PriceInfo { Year = 2007, Price = 63 },
                new PriceInfo { Year = 2008, Price = 64 },
                new PriceInfo { Year = 2009, Price = 69 },
                new PriceInfo { Year = 2010, Price = 68 },
                new PriceInfo { Year = 2011, Price = 67 },
                new PriceInfo { Year = 2012, Price = 61 },
            };

            Product p2 = new Product();
            p2.ProductName = "Metal";
            p2.CurrentPrice = 200;
            p2.PricesInfo = new List<PriceInfo> 
            { 
                new PriceInfo { Year = 2005, Price = 180 },
                new PriceInfo { Year = 2006, Price = 195 },
                new PriceInfo { Year = 2007, Price = 193 },
                new PriceInfo { Year = 2008, Price = 184 },
                new PriceInfo { Year = 2009, Price = 199 },
                new PriceInfo { Year = 2010, Price = 128 },
                new PriceInfo { Year = 2011, Price = 117 },
                new PriceInfo { Year = 2012, Price = 161 },
            };

            Product p3 = new Product();
            p3.ProductName = "Plastic";
            p3.CurrentPrice = 130;
            p3.PricesInfo = new List<PriceInfo> 
            { 
                new PriceInfo { Year = 2005, Price = 120 },
                new PriceInfo { Year = 2006, Price = 125 },
                new PriceInfo { Year = 2007, Price = 123 },
                new PriceInfo { Year = 2008, Price = 124 },
                new PriceInfo { Year = 2009, Price = 129 },
                new PriceInfo { Year = 2010, Price = 128 },
                new PriceInfo { Year = 2011, Price = 127 },
                new PriceInfo { Year = 2012, Price = 121 },
            };

            Company company3 = new Company();
            company3.CompanyName = "Solid Chairs";
            company3.Products = new List<Product> { p1, p2, p3 };

            return company3;
        }
    }
}
