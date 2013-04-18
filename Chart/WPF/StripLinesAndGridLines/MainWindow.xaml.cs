using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StripLinesAndGridLines
{
	public partial class MainWindow : Window
	{
		public MainWindow()
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
