using System.Collections.Generic;
using System.Windows.Controls;

namespace StripLinesAndGridLines
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			InitializeComponent();
			var series1items = new List<ChartData>()
			{
				new ChartData { XCat = "Q1", YVal = 154 },
				new ChartData { XCat = "Q2", YVal = 138 },
				new ChartData { XCat = "Q3", YVal = 143 },
				new ChartData { XCat = "Q4", YVal = 120 },
			};
			var series2Items = new List<ChartData>()
			{
				new ChartData { XCat = "Q1", YVal = 134 },
				new ChartData { XCat = "Q2", YVal = 178 },
				new ChartData { XCat = "Q3", YVal = 105 },
				new ChartData { XCat = "Q4", YVal = 154 },
			};

			this.DataContext = new ViewModel { Series1Items = series1items, Series2Items = series2Items };
		}
	}
}
