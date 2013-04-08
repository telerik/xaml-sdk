using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace DateTimeGrouping
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
			InitializeComponent();
			var itemsSource = new List<ChartData>()
            {
                new ChartData { Date = new DateTime(2008, 06, 8), Value = 145 },
                new ChartData { Date = new DateTime(2008, 07, 8), Value = 132 },
                new ChartData { Date = new DateTime(2008, 08, 8), Value = 164 },
                new ChartData { Date = new DateTime(2008, 09, 8), Value = 187 },
                new ChartData { Date = new DateTime(2008, 10, 8), Value = 186 },
                new ChartData { Date = new DateTime(2008, 12, 8), Value = 131 },
                new ChartData { Date = new DateTime(2009, 01, 8), Value = 173 },
                new ChartData { Date = new DateTime(2009, 02, 8), Value = 172 },
                new ChartData { Date = new DateTime(2009, 03, 8), Value = 140 },
                new ChartData { Date = new DateTime(2009, 04, 8), Value = 129 },
                new ChartData { Date = new DateTime(2009, 05, 8), Value = 149 },
                new ChartData { Date = new DateTime(2009, 06, 8), Value = 158 },
                new ChartData { Date = new DateTime(2009, 07, 8), Value = 145 },
                new ChartData { Date = new DateTime(2009, 08, 8), Value = 132 },
                new ChartData { Date = new DateTime(2009, 09, 8), Value = 164 },
                new ChartData { Date = new DateTime(2009, 10, 8), Value = 187 },
                new ChartData { Date = new DateTime(2009, 11, 8), Value = 186 },
                new ChartData { Date = new DateTime(2009, 12, 8), Value = 131 },
                new ChartData { Date = new DateTime(2010, 01, 8), Value = 173 },
                new ChartData { Date = new DateTime(2010, 02, 8), Value = 172 },
                new ChartData { Date = new DateTime(2010, 03, 8), Value = 140 },
                new ChartData { Date = new DateTime(2010, 04, 8), Value = 129 },
                new ChartData { Date = new DateTime(2010, 05, 8), Value = 158 },
                new ChartData { Date = new DateTime(2010, 06, 8), Value = 149 },
            };
			this.radChart.ItemsSource = itemsSource;
			this.radChart.DefaultView.ChartArea.AxisX.DefaultLabelFormat = "MMM";
			this.radChart.DefaultView.ChartArea.AxisX.LabelRotationAngle = 45;
		}
	}
}
