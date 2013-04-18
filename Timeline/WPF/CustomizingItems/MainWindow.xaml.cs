using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;

namespace CustomizingItems
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			List<Product> dataSource = new List<Product>();
			var startDate = new DateTime(2011, 1, 1);
			var endDate = new DateTime(2011, 6, 1);

			Random r = new Random();
			for (DateTime i = startDate; i < endDate; i = i.AddMonths(1))
			{
				dataSource.Add(new Product() { Date = i, Duration = TimeSpan.FromDays(r.Next(50, 100)) });
			}

			for (int i = 0; i < 15; i++)
			{
				dataSource.Add(new Product() { Date = startDate.AddMonths(r.Next(0, 5)).AddDays(15) });
			}
			RadTimeline1.ItemsSource = dataSource;
		}
	}
}
