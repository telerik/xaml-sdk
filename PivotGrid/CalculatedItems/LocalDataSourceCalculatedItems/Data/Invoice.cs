using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows;

namespace LocalDataSourceCalculatedItems.Data
{
    public class Invoice
    {
        public string CustomerID { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Salesperson { get; set; }

        public uint OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public DayOfWeek Weekday { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime ShippedDate { get; set; }

        public string Shipper { get; set; }
        public uint ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public uint Quantity { get; set; }
        public decimal Discount { get; set; }
        public decimal ExtendedPrice { get; set; }
        public decimal Freight { get; set; }

        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public string ShipRegion { get; set; }
        public string ShipPostalCode { get; set; }
        public string ShipCountry { get; set; }
        public string CategoryName { get; set; }

        public static List<Invoice> GetInvoices()
        {
            List<Invoice> list = new List<Invoice>();

            Uri uri = new Uri(@"/LocalDataSourceCalculatedItems;component/Data/PivotTables10.txt", UriKind.Relative);
            var sri = Application.GetResourceStream(uri);
            var s = sri.Stream;
            var streamReader = new StreamReader(s);
            string file = streamReader.ReadToEnd();
            using (var reader = new StringReader(file))
            {
                while (reader.Peek() != -1)
                {
                    string[] items = reader.ReadLine().Split('\t');
                    int index = 0;
                    Invoice invoice = new Invoice()
                    {
                        CustomerID = items[index++],
                        CompanyName = items[index++],
                        Address = items[index++],
                        City = items[index++],
                        Region = items[index++],
                        PostalCode = items[index++],
                        Country = items[index++],
                        Salesperson = items[index++],
                        OrderID = uint.Parse(items[index++], CultureInfo.InvariantCulture),
                        OrderDate = DateTime.Parse(items[index++], CultureInfo.InvariantCulture),
                        Weekday = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), items[index++], true),
                        RequiredDate = DateTime.Parse(items[index++], CultureInfo.InvariantCulture),
                        ShippedDate = DateTime.Parse(items[index++], CultureInfo.InvariantCulture),
                        Shipper = items[index++],
                        ProductID = uint.Parse(items[index++], CultureInfo.InvariantCulture),
                        ProductName = items[index++],
                        UnitPrice = decimal.Parse(items[index++], CultureInfo.InvariantCulture),
                        Quantity = uint.Parse(items[index++], CultureInfo.InvariantCulture),
                        Discount = decimal.Parse(items[index++], CultureInfo.InvariantCulture),
                        ExtendedPrice = decimal.Parse(items[index++], CultureInfo.InvariantCulture),
                        Freight = decimal.Parse(items[index++], CultureInfo.InvariantCulture),
                        ShipName = items[index++],
                        ShipAddress = items[index++],
                        ShipCity = items[index++],
                        ShipRegion = items[index++],
                        ShipPostalCode = items[index++],
                        ShipCountry = items[index++],
                        CategoryName = items[index++],
                    };
                    list.Add(invoice);
                }
            }

            return list;
        }
    }
}
