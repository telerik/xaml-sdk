using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.TimeBar;

namespace CreateTimeBarCodeBehind
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			RadAreaSparkline sparkline = new RadAreaSparkline();
			Random r = new Random();
			List<int> data = new List<int>();
			for (DateTime currentDate = DateTime.Today; currentDate < DateTime.Today.AddDays(100); currentDate = currentDate.AddDays(1))
			{
				data.Add(r.Next(0, 60));
			}
			sparkline.ItemsSource = data;

			RadTimeBar timebar = new RadTimeBar();
			timebar.BeginInit();
			timebar.Width = 950;
			timebar.Height = 250;
			timebar.PeriodStart = new DateTime(2010, 1, 1);
			timebar.PeriodEnd = new DateTime(2012, 1, 1);
			timebar.VisiblePeriodStart = new DateTime(2010, 2, 14);
			timebar.VisiblePeriodEnd = new DateTime(2011, 6, 2);
			timebar.SelectionStart = new DateTime(2010, 5, 14);
			timebar.SelectionEnd = new DateTime(2010, 6, 14);
			timebar.Intervals.Add(new MonthInterval());
			timebar.Intervals.Add(new WeekInterval());
			timebar.Intervals.Add(new DayInterval());
			timebar.EndInit();
			timebar.Content = sparkline;
			this.Content = timebar;
		}
	}
}
