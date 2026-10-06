using System;
using System.Collections.Generic;

namespace XAxis
{
	public class ChartData
	{
		public DateTime Date { get; set; }
		public decimal Value { get; set; }

		public static IEnumerable<ChartData> GetSampleData()
		{
			Random rand = new Random(0);
			for (DateTime currentDate = new DateTime(2012, 1, 1); currentDate.Month != 4; currentDate = currentDate.AddDays(2))
			{
				yield return new ChartData { Date = currentDate, Value = rand.Next(100, 200) };
			}
		}
	}
}
