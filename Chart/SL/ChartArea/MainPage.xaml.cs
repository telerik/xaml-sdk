using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace ChartArea
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
			InitializeComponent();
			this.DataContext = new List<ChartData>()
			{
				new ChartData { Date = new DateTime(2009, 1, 1), Value = 154000000 },
				new ChartData { Date = new DateTime(2009, 2, 1), Value = 138000000 },
				new ChartData { Date = new DateTime(2009, 3, 1), Value = 143000000 },
				new ChartData { Date = new DateTime(2009, 4, 1), Value = 120000000 },
				new ChartData { Date = new DateTime(2009, 5, 1), Value = 135000000 },
				new ChartData { Date = new DateTime(2009, 6, 1), Value = 125000000 },
				new ChartData { Date = new DateTime(2009, 7, 1), Value = 179000000 },
				new ChartData { Date = new DateTime(2009, 8, 1), Value = 170000000 },
				new ChartData { Date = new DateTime(2009, 9, 1), Value = 198000000 },
				new ChartData { Date = new DateTime(2009, 10, 1), Value = 187000000 },
				new ChartData { Date = new DateTime(2009, 11, 1), Value = 193000000 },
				new ChartData { Date = new DateTime(2009, 12, 1), Value = 176000000 },
			};
		}
	}
}
