using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace ChartExport
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
					new ChartData { XCat = 1, YVal = 24 },
					new ChartData { XCat = 2, YVal = 9 },
					new ChartData { XCat = 3, YVal = 18 },
					new ChartData { XCat = 4, YVal = 31 },
					new ChartData { XCat = 5, YVal = 25 },
					new ChartData { XCat = 6, YVal = 13 },
					new ChartData { XCat = 7, YVal = 17 },
					new ChartData { XCat = 8, YVal = 33 },
					new ChartData { XCat = 9, YVal = 21 },
					new ChartData { XCat = 10, YVal = 27 },
				},
				new List<ChartData>()
				{
					new ChartData { XCat = 1, YVal = 4 },
					new ChartData { XCat = 2, YVal = 19 },
					new ChartData { XCat = 3, YVal = 28 },
					new ChartData { XCat = 4, YVal = 11 },
					new ChartData { XCat = 5, YVal = 15 },
					new ChartData { XCat = 6, YVal = 31 },
					new ChartData { XCat = 7, YVal = 27 },
					new ChartData { XCat = 8, YVal = 14 },
					new ChartData { XCat = 9, YVal = 19 },
					new ChartData { XCat = 10, YVal = 21 },
				}
			};
		}

		private void Export_Excel_Click(object sender, RoutedEventArgs e)
		{
			SaveFileDialog dialog = new SaveFileDialog();
			dialog.DefaultExt = "*.xlsx";
			dialog.Filter = "Files(*.xlsx)|*.xlsx";
			if (!(bool)dialog.ShowDialog())
				return;
			Stream fileStream = dialog.OpenFile();
			radChart.ExportToExcelML(fileStream);
			fileStream.Close();
		}
	}
}
