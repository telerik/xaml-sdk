using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows.Documents.Layout;
using Telerik.Windows.Documents.Model;

namespace CreateCustomMergeField
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.radRichTextBox.Document.MailMergeDataSource.ItemsSource = new List<Customer>()
            {
                new Customer()
                {
                    FirstName = "John",
                    LastName = "Doe",
                    Orders = new List<Order>()
                    {
                        new Order() { ProductName = "Product 1" },
                        new Order() { ProductName = "Product 2" },
                    }
                },
                new Customer()
                {
                    FirstName = "Sara",
                    LastName = "Doe",
                    Orders = new List<Order>()
                    {
                        new Order() { ProductName = "Product 3" },
                        new Order() { ProductName = "Product 4" },
                    }
                }
            };

            this.radRichTextBox.InsertField(new MergeField() { PropertyPath = "FirstName" });
            this.radRichTextBox.Insert(FormattingSymbolLayoutBox.ENTER);
            this.radRichTextBox.InsertField(new OrdersMergeField() { PropertyPath = "Orders" });
        }

        private void RadButton_Click(object sender, RoutedEventArgs e)
        {
            var docuemnt = radRichTextBox.MailMerge();
            radRichTextBox.Document = docuemnt;
        }
    }
}
