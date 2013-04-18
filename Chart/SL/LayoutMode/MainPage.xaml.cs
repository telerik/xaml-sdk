using System.Collections.Generic;
using System.Windows.Controls;

namespace LayoutMode
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			InitializeComponent();
			var barSeriesData = new List<ChartData>()
			{
				new ChartData { XCat = 1, YVal = 21.6 },
				new ChartData { XCat = 2, YVal = 21.2 },
				new ChartData { XCat = 3, YVal = 21.5 },
				new ChartData { XCat = 4, YVal = 21.6 },
				new ChartData { XCat = 5, YVal = 21.55 },
				new ChartData { XCat = 6, YVal = 21.4 },
				new ChartData { XCat = 7, YVal = 21 },
				new ChartData { XCat = 8, YVal = 21.2 },
				new ChartData { XCat = 9, YVal = 21.9 },
				new ChartData { XCat = 10, YVal = 22.1 },
				new ChartData { XCat = 11, YVal = 22 },
				new ChartData { XCat = 12, YVal = 21.9 },
				new ChartData { XCat = 13, YVal = 21.8 },
				new ChartData { XCat = 14, YVal = 21.7 },
				new ChartData { XCat = 15, YVal = 21.8 },
				new ChartData { XCat = 16, YVal = 21.6 },
				new ChartData { XCat = 17, YVal = 21 },
				new ChartData { XCat = 18, YVal = 21.6 },
				new ChartData { XCat = 19, YVal = 21.5 },
				new ChartData { XCat = 20, YVal = 21.7 },
				new ChartData { XCat = 21, YVal = 21.8 },
			};

			var lineSeriesData = new List<ChartData>()
			{
				new ChartData { XCat = 1, YVal = 23.6 },
				new ChartData { XCat = 2, YVal = 23.2 },
				new ChartData { XCat = 3, YVal = 23.5 },
				new ChartData { XCat = 4, YVal = 23.6 },
				new ChartData { XCat = 5, YVal = 23.55 },
				new ChartData { XCat = 6, YVal = 23.4 },
				new ChartData { XCat = 7, YVal = 23 },
				new ChartData { XCat = 8, YVal = 23.2 },
				new ChartData { XCat = 9, YVal = 23.9 },
				new ChartData { XCat = 10, YVal = 24.1 },
				new ChartData { XCat = 11, YVal = 24 },
				new ChartData { XCat = 12, YVal = 23.9 },
				new ChartData { XCat = 13, YVal = 23.8 },
				new ChartData { XCat = 14, YVal = 23.7 },
				new ChartData { XCat = 15, YVal = 23.8 },
				new ChartData { XCat = 16, YVal = 23.6 },
				new ChartData { XCat = 17, YVal = 23 },
				new ChartData { XCat = 18, YVal = 23.6 },
				new ChartData { XCat = 19, YVal = 23.5 },
				new ChartData { XCat = 20, YVal = 23.7 },
				new ChartData { XCat = 21, YVal = 23.8 },
			};

			this.DataContext = new SimpleViewModel { BarSeriesData = barSeriesData, LineSeriesData = lineSeriesData };
		}
	}
}
