using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Telerik.Windows.Controls.TreeMap;

namespace Selection.Views
{
    public partial class TreeMapSelection : UserControl
    {
        public TreeMapSelection()
        {
            InitializeComponent();

            List<Product> books = new List<Product>
            {
                new Product { CategoryName = "Book", ProductName = "Book-A", UnitPrice = 10, UnitsInStock = 120, },
                new Product { CategoryName = "Book", ProductName = "Book-B", UnitPrice = 4, UnitsInStock = 20, },
                new Product { CategoryName = "Book", ProductName = "Book-C", UnitPrice = 5, UnitsInStock = 60, },
                new Product { CategoryName = "Book", ProductName = "Book-D", UnitPrice = 15, UnitsInStock = 20, },
            };

            List<Product> laptops = new List<Product>
            {
                new Product { CategoryName = "Laptop", ProductName = "Laptop-A", UnitPrice = 120, UnitsInStock = 20, },
                new Product { CategoryName = "Laptop", ProductName = "Laptop-B", UnitPrice = 190, UnitsInStock = 12, },
                new Product { CategoryName = "Laptop", ProductName = "Laptop-C", UnitPrice = 210, UnitsInStock = 4, },
                new Product { CategoryName = "Laptop", ProductName = "Laptop-D", UnitPrice = 530, UnitsInStock = 5, },
            };

            List<Product> comics = new List<Product>
            {
                new Product { CategoryName = "Comics", ProductName = "Comics-A", UnitPrice = 20, UnitsInStock = 20, },
                new Product { CategoryName = "Comics", ProductName = "Comics-B", UnitPrice = 70, UnitsInStock = 12, },
                new Product { CategoryName = "Comics", ProductName = "Comics-C", UnitPrice = 10, UnitsInStock = 4, },
                new Product { CategoryName = "Comics", ProductName = "Comics-D", UnitPrice = 30, UnitsInStock = 5, },
            };

            this.DataContext = new List<Category> 
            {
                new Category("Books", books),
                new Category("Laptops", laptops),
                new Category("Comics", comics),
            };
        }
    
        private void RadioButtonSingleSelection_Checked(object sender, RoutedEventArgs e)
        {
            this.treemap1.SelectionMode = SelectionMode.Single;
        }

        private void RadioButtonExtendedSelection_Checked(object sender, RoutedEventArgs e)
        {
            this.treemap1.SelectionMode = SelectionMode.Extended;
        }

        private void RadioButtonMultipleSelection_Checked(object sender, RoutedEventArgs e)
        {
            this.treemap1.SelectionMode = SelectionMode.Multiple;
        }
    }
}
