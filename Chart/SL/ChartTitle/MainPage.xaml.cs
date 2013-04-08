using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace ChartTitle
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
			InitializeComponent();
			var items = new List<ChartData>()
			{
				new ChartData { Date = new DateTime(2012, 1, 1), Turnover = 154, Expenses = 45 },
				new ChartData { Date = new DateTime(2012, 2, 1), Turnover = 138, Expenses = 48 },
				new ChartData { Date = new DateTime(2012, 3, 1), Turnover = 143, Expenses = 53 },
				new ChartData { Date = new DateTime(2012, 4, 1), Turnover = 120, Expenses = 41 },
				new ChartData { Date = new DateTime(2012, 5, 1), Turnover = 135, Expenses = 32 },
				new ChartData { Date = new DateTime(2012, 6, 1), Turnover = 125, Expenses = 28 },
				new ChartData { Date = new DateTime(2012, 7, 1), Turnover = 179, Expenses = 63 },
				new ChartData { Date = new DateTime(2012, 8, 1), Turnover = 170, Expenses = 74 },
				new ChartData { Date = new DateTime(2012, 9, 1), Turnover = 198, Expenses = 77 },
				new ChartData { Date = new DateTime(2012, 10, 1), Turnover = 187, Expenses = 85 },
				new ChartData { Date = new DateTime(2012, 11, 1), Turnover = 193, Expenses = 89 },
				new ChartData { Date = new DateTime(2012, 12, 1), Turnover = 176, Expenses = 80 },
			};

			this.DataContext = items;
		}
	}
}
