using System;

namespace Colorizers_CategoricalDefinition
{
	public class MonthlyTemp
	{
		public DateTime Time { get; set; }
		public double Temp { get; set; }
		public MonthlyTemp(DateTime time, double temp)
		{
			this.Time = time;
			this.Temp = temp;
		}
	}
}
