using System.Collections.Generic;
using System.Windows.Controls;

namespace AxesOverview
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			InitializeComponent();
			this.DataContext = new List<ChartData>()
			{
				new ChartData { XVal = 1, YVal = 154 },
				new ChartData { XVal = 2, YVal = 138 },
				new ChartData { XVal = 3, YVal = 143 },
				new ChartData { XVal = 4, YVal = 120 },
				new ChartData { XVal = 5, YVal = 135 },
				new ChartData { XVal = 6, YVal = 125 },
				new ChartData { XVal = 7, YVal = 179 },
				new ChartData { XVal = 8, YVal = 170 },
				new ChartData { XVal = 9, YVal = 198 },
				new ChartData { XVal = 10, YVal = 187 },
				new ChartData { XVal = 11, YVal = 193 },
				new ChartData { XVal = 12, YVal = 176 },
			};
		}
	}
}
