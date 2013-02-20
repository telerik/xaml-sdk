using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace DataBinding
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			InitializeComponent();
			var startDate = new DateTime(2010, 1, 1);
			var endDate = new DateTime(2012, 2, 1);

			var items = new List<Item>();
			Random r = new Random();
			for (DateTime i = startDate; i < endDate; i = i.AddMonths(1))
			{
				items.Add(new Item() { Date = i, Duration = TimeSpan.FromDays(r.Next(50, 100)) });
			}

			for (int i = 0; i < 15; i++)
			{
				items.Add(new Item()
				{
					Date = startDate.AddMonths(r.Next(0, 25)).AddDays(15)
				});
			}

			this.DataContext = new Product() { Data = items, StartDate = startDate, EndDate = endDate, };
		}
	}
}
