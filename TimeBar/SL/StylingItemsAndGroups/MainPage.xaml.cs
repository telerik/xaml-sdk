using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace StylingItemsAndGroups
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			InitializeComponent();
			Random r = new Random();
			List<int> data = new List<int>();
			for (DateTime currentDate = DateTime.Today; currentDate < DateTime.Today.AddDays(100);
			  currentDate = currentDate.AddDays(1))
			{
				data.Add(r.Next(0, 60));
			}
			this.sparkline.ItemsSource = data;
		}
	}
}
