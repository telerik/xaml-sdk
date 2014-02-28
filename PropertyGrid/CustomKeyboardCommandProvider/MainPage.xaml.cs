using System;
using System.Linq;
using System.Windows.Controls;
using KeyboardCommandProvider;

namespace SilverlightApplication1
{
    public partial class MainPage : UserControl
    {
        public MainPage()
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