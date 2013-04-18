using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace DataBindingWithAutomaticSeriesMappings
{
	public partial class MainPage : UserControl
	{
		public MainPage()
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
