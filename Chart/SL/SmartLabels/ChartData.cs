using System;
using System.Collections.Generic;

namespace SmartLabels
{
	public class ChartData
	{
		public int XVal { get; set; }
		public int YVal { get; set; }

		public static IEnumerable<ChartData> GetSampleData()
		{
			Random r = new Random(0);
			for (int i = 0; i < 20; i++)
			{
				yield return new ChartData { XVal = i, YVal = r.Next(0, 100) };
			}
		}
	}
}
