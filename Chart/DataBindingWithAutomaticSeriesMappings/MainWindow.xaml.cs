using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;

namespace DataBindingWithAutomaticSeriesMappings
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			List<Manufacturer> data = new List<Manufacturer>();
			data.Add(new Manufacturer("Toyota", 215, 462));
			data.Add(new Manufacturer("General Motors", 192, 345));
			data.Add(new Manufacturer("Volkswagen", 151, 310));
			data.Add(new Manufacturer("Ford", 125, 340));
			data.Add(new Manufacturer("Honda", 91, 201));
			data.Add(new Manufacturer("Nissan", 79, 145));
			data.Add(new Manufacturer("PSA", 79, 175));
			data.Add(new Manufacturer("Hyundai", 64, 133));

			this.telerikChart.ItemsSource = data;
		}
	}
}
