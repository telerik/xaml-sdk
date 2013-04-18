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

namespace ItemWidthPercentAndItemOverlapPercent
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			InitializeComponent();
			this.DataContext = new List<List<ChartData>>()
			{
				new List<ChartData>()
				{
					new ChartData { Quarter = 1, Sales = 15 },
					new ChartData { Quarter = 2, Sales = 5 },
					new ChartData { Quarter = 3, Sales = 34 },
					new ChartData { Quarter = 4, Sales = 11 },
				},
				new List<ChartData>()
				{
					new ChartData { Quarter = 1, Sales = 1 },
					new ChartData { Quarter = 2, Sales = 6 },
					new ChartData { Quarter = 3, Sales = 38 },
					new ChartData { Quarter = 4, Sales = 12 },
				}
			};
		}
	}
}
