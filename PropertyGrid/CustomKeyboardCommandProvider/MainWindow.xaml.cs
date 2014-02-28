using System;
using System.Linq;
using System.Windows;
using KeyboardCommandProvider;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            PropertyGrid1.Item = new Order()
            {
                ShipAddress = "Luisenstr. 48",
                ShipCountry = "Germany",
                ShipName = "Toms Spezialitaten",
                ShipPostalCode = "44087",
                Employee = new Employee()
                {
                    FirstName = "Nancy",
                    LastName = "Davolio",
                    Title = "Sales Representative"
                },

            };
        }
    }
}
