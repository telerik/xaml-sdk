using System;
using System.Collections.ObjectModel;

namespace Tooltips
{
	public class Product
	{
		public TimeSpan Duration { get; set; }
		public DateTime Date { get; set; }

		public static ObservableCollection<Product> GetData(int count)
		{
			var startDate = new DateTime(2010, 1, 1);
			var endDate = new DateTime(2012, 2, 1);
			Random r = new Random();
			ObservableCollection<Product> result = new ObservableCollection<Product>();

			for (DateTime i = startDate; i < endDate; i = i.AddMonths(1))
			{
				result.Add(new Product() { Date = i, Duration = TimeSpan.FromDays(r.Next(50, 100)) });
			}

			for (int i = 0; i < 15; i++)
			{
				result.Add(new Product()
				{
					Date = startDate.AddMonths(r.Next(0, 25)).AddDays(15)
				});
			}

			return result;
		}
	}
}
