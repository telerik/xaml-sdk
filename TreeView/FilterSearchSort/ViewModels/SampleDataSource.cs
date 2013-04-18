using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace FilterSearchSort.ViewModels
{
    public class SampleDataSource : ObservableCollection<Category>
    {
        public SampleDataSource()
        {
            Category cat;
            cat = new Category("Beverages");
            cat.Products.Add(new Product("Chai"));
            cat.Products.Add(new Product("Chang"));
            cat.Products.Add(new Product("Guaraná Fantástica"));
            cat.Products.Add(new Product("Sasquatch Ale"));
            cat.Products.Add(new Product("Steeleye Stout"));
            cat.Products.Add(new Product("Côte de Blaye"));
            cat.Products.Add(new Product("Chartreuse verte"));
            cat.Products.Add(new Product("Ipoh Coffee"));
            cat.Products.Add(new Product("Laughing Lumberjack Lager"));
            cat.Products.Add(new Product("Outback Lager"));
            this.Add(cat);
            cat = new Category("Condiments");
            cat.Products.Add(new Product("Aniseed Syrop"));
            cat.Products.Add(new Product("Genen Shoyyu"));
            cat.Products.Add(new Product("Gula Malacca"));
            cat.Products.Add(new Product("Chef Anton's Cajun Seasoning"));
            cat.Products.Add(new Product("Chef Anton's Gumbo Mix"));
            cat.Products.Add(new Product("Grandma's Boysenberry Spread"));
            cat.Products.Add(new Product("Northwoods Cranberry Sauce"));
            cat.Products.Add(new Product("Luisiana Fiery Hot Pepper Sauce"));
            cat.Products.Add(new Product("Luisiana Hot Spiced Okra"));
            this.Add(cat);
            cat = new Category("Produce");
            cat.Products.Add(new Product("Uncle bob's Organic Dried Pears"));
            cat.Products.Add(new Product("Tofu"));
            cat.Products.Add(new Product("Longlife Tofu"));
            cat.Products.Add(new Product("Manjimup Dried Apples"));
            cat.Products.Add(new Product("Rössle Sauerkraut"));
            this.Add(cat);
            cat = new Category("Meat/Poultry");
            cat.Products.Add(new Product("Mishi Kobe Niku"));
            cat.Products.Add(new Product("Alice Mutton"));
            cat.Products.Add(new Product("Perth Pasties"));
            this.Add(cat);
            cat = new Category("Seafood");
            cat.Products.Add(new Product("Ikura"));
            cat.Products.Add(new Product("Konbu"));
            cat.Products.Add(new Product("Carnarvon Tigers"));
            cat.Products.Add(new Product("Nord-Ost Matjeshering"));
            cat.Products.Add(new Product("Inlagd Sill"));
            cat.Products.Add(new Product("Gravad Iax"));
            cat.Products.Add(new Product("Boston Crab Meat"));
            cat.Products.Add(new Product("Rogede slid"));
            cat.Products.Add(new Product("Spegeslid"));
            this.Add(cat);
            cat = new Category("Dairy Products");
            cat.Products.Add(new Product("Queso Cabrales"));
            cat.Products.Add(new Product("Queso Manchego La Pastora"));
            cat.Products.Add(new Product("Gorgonzola Telino"));
            cat.Products.Add(new Product("Geitost"));
            cat.Products.Add(new Product("Flotemoysost"));
            cat.Products.Add(new Product("Raclette Courdavault"));
            this.Add(cat);
            cat = new Category("Confections");
            cat.Products.Add(new Product("Sir Rodney's Marmalade"));
            cat.Products.Add(new Product("Sir Rodney's Scones"));
            cat.Products.Add(new Product("Chocolade"));
            cat.Products.Add(new Product("Maxilaku"));
            cat.Products.Add(new Product("Tarte au sucre"));
            cat.Products.Add(new Product("Zaanse koeken"));
            cat.Products.Add(new Product("Teatime Chocolate Biscuits"));
            this.Add(cat);
            cat = new Category("Grains/Cereals");
            cat.Products.Add(new Product("Filo Mix"));
            cat.Products.Add(new Product("Singaporean Hokkien Fried Mee"));
            cat.Products.Add(new Product("Tunnbröd"));
            cat.Products.Add(new Product("Gnocchi di nonna Alice"));
            cat.Products.Add(new Product("Ravioli Angelo"));
            this.Add(cat);
        }
    }
}
