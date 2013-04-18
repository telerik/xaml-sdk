using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace CustomizingScatterPoints
{
	public class ChartData
	{
		private readonly Brush Red = new SolidColorBrush(Colors.Red);
		private readonly Brush Orange = new SolidColorBrush(Colors.Orange);
		private readonly Brush Green = new SolidColorBrush(Colors.Green);

		public ChartData(double x, double y)
		{
			this.XValue = x;
			this.YValue = y;
		}

		public double XValue { get; set; }

		public double YValue { get; set; }

		public Brush Brush
		{
			get
			{
				if (this.YValue < 102)
				{
					return Red;
				}
				else if (this.YValue < 105)
				{
					return Orange;
				}
				else
				{
					return Green;
				}
			}
		}
	}
}