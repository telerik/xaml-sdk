using System;
using System.Collections.Generic;

namespace ZoomingAndScrolling
{
	public class ChartData
	{
		public int XVal { get; set; }
		public int YVal { get; set; }

		public static IEnumerable<ChartData> GetSampleData()
		{
			Random r = new Random(0);
			for (int i = 0; i < 100; i++)
			{
				yield return new ChartData { XVal = i, YVal = r.Next(0, 100) };
			}
		}
	}
}
