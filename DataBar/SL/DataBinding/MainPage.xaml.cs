using System.Collections.Generic;
using System.Windows.Controls;

namespace DataBinding
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			InitializeComponent();
			var items = new List<Item>() 
			{
				new Item{ Val = 9, Name = "nine", },
				new Item{ Val = 10, Name = "ten", },
				new Item{ Val = 11, Name = "eleven", },
				new Item{ Val = 20, Name = "twenty", }, 
				new Item{ Val = 22, Name = "twenty two", }, 
				new Item{ Val = 90, Name = "ninety", }, 

				new Item{ Val = -9, Name = "-nine", },
				new Item{ Val = -10, Name = "-ten", },
				new Item{ Val = -11, Name = "-eleven", },
				new Item{ Val = -20, Name = "-twenty", }, 
				new Item{ Val = -100, Name = "-hundred", }, 
			};

			this.DataContext = new Product()
			{
				Value1 = 20,
				Value2 = 30,
				Ints = new List<int>() { 5, 6, 7, 8, 9, },
				Items = items
			};
		}
	}
}
