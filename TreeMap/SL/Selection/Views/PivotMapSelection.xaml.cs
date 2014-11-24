using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls.TreeMap;

namespace Selection.Views
{
    public partial class PivotMapSelection : UserControl
    {
        public PivotMapSelection()
        {
            InitializeComponent();

            var products = new List<Product>
            {
                new Product { CategoryName = "Books", ProductName = "Book-A", UnitPrice = 10, UnitsInStock = 120, },
                new Product { CategoryName = "Books", ProductName = "Book-B", UnitPrice = 4, UnitsInStock = 20, },
                new Product { CategoryName = "Books", ProductName = "Book-C", UnitPrice = 5, UnitsInStock = 60, },
                new Product { CategoryName = "Books", ProductName = "Book-D", UnitPrice = 15, UnitsInStock = 20, },
                new Product { CategoryName = "Laptops", ProductName = "Laptop-A", UnitPrice = 120, UnitsInStock = 20, },
                new Product { CategoryName = "Laptops", ProductName = "Laptop-B", UnitPrice = 190, UnitsInStock = 12, },
                new Product { CategoryName = "Laptops", ProductName = "Laptop-C", UnitPrice = 210, UnitsInStock = 4, },
                new Product { CategoryName = "Laptops", ProductName = "Laptop-D", UnitPrice = 530, UnitsInStock = 5, },
                new Product { CategoryName = "Comics", ProductName = "Comics-A", UnitPrice = 20, UnitsInStock = 20, },
                new Product { CategoryName = "Comics", ProductName = "Comics-B", UnitPrice = 70, UnitsInStock = 12, },
                new Product { CategoryName = "Comics", ProductName = "Comics-C", UnitPrice = 10, UnitsInStock = 4, },
                new Product { CategoryName = "Comics", ProductName = "Comics-D", UnitPrice = 30, UnitsInStock = 5, },
            };

            this.DataContext = products;
        }

        private void RadioButtonSingleSelection_Checked(object sender, RoutedEventArgs e)
        {
            this.treemap1.SelectionMode = SelectionMode.Single;
        }

        private void RadioButtonExtendedSelection_Checked(object sender, RoutedEventArgs e)
        {
            if (this.treemap1 != null)
            {
                this.treemap1.SelectionMode = SelectionMode.Extended;
            }
        }

        private void RadioButtonMultipleSelection_Checked(object sender, RoutedEventArgs e)
        {
            this.treemap1.SelectionMode = SelectionMode.Multiple;
        }
    }
}
