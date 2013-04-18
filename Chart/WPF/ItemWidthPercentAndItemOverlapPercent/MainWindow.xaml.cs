using System.Collections.Generic;
using System.Windows;

namespace ItemWidthPercentAndItemOverlapPercent
{
	public partial class MainWindow : Window
	{
		public MainWindow()
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
