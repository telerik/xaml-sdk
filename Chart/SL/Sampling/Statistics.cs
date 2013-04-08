using System;
using System.Collections.Generic;
using System.Linq;

namespace Sampling
{
	public static class Statistics
	{
		public static double StdDev<TSource>(IEnumerable<TSource> source, Func<TSource, ChartData> selector)
		{
			return StdDev<TSource, ChartData>(source, selector);
		}

		public static double StdDev<TSource, TResult>(IEnumerable<TSource> source, Func<TSource, TResult> selector)
			where TResult : ChartData
		{
			int itemCount = source.Count();
			if (itemCount > 1)
			{
				IEnumerable<double> values = from i in source select Convert.ToDouble(selector(i).YVal);

				double sum = SumAvg(values);

				return Math.Sqrt(sum / (itemCount - 1));
			}

			return 0;
		}

		private static double SumAvg(IEnumerable<double> values)
		{
			double average = values.Average();
			double sum = 0;

			foreach (double item in values)
			{
				sum += Math.Pow(item - average, 2);
			}

			return sum;
		}
	}
}
