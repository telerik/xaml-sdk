using System.Collections.Generic;
using System.Windows.Controls;

namespace CreateDataBoundChart
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
          InitializeComponent();
            this.radChart1.ItemsSource = this.CreateData();
            this.radChart2.ItemsSource = this.CreateData();
        }

        private List<ProductSales> CreateData()
        {
            List<ProductSales> persons = new List<ProductSales>();
            persons.Add(new ProductSales(154, 1, "January"));
            persons.Add(new ProductSales(138, 2, "February"));
            persons.Add(new ProductSales(143, 3, "March"));
            persons.Add(new ProductSales(120, 4, "April"));
            persons.Add(new ProductSales(135, 5, "May"));
            persons.Add(new ProductSales(125, 6, "June"));
            persons.Add(new ProductSales(179, 7, "July"));
            persons.Add(new ProductSales(170, 8, "August"));
            persons.Add(new ProductSales(198, 9, "September"));
            persons.Add(new ProductSales(187, 10, "October"));
            persons.Add(new ProductSales(193, 11, "November"));
            persons.Add(new ProductSales(212, 12, "December"));
            return persons;
        }
    }
}
