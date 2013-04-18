using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace MultipleYAxes
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			InitializeComponent();
			var series1Items = new List<ChartData>()
			{
				new ChartData { Value = 1950000 },
				new ChartData { Value = 2200000 },
				new ChartData { Value = 1800000 },
				new ChartData { Value = 2180000 },
				new ChartData { Value = 2250000 },
				new ChartData { Value = 1700000 },
				new ChartData { Value = 2100000 },
				new ChartData { Value = 1900000 },
				new ChartData { Value = 1800000 },
				new ChartData { Value = 1850000 },
			};

			var series2Items = new List<ChartData>()
			{
				new ChartData { Value = 500000 },
				new ChartData { Value = 580000 },
				new ChartData { Value = 420000 },
				new ChartData { Value = 550000 },
				new ChartData { Value = 600000 },
				new ChartData { Value = 380000 },
				new ChartData { Value = 550000 },
				new ChartData { Value = 460000 },
				new ChartData { Value = 420000 },
				new ChartData { Value = 430000 },
			};

			this.DataContext = new List<IEnumerable<ChartData>>() { series1Items, series2Items };
		}
	}
}
