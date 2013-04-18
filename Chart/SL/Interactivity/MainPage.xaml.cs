using System.Windows;
using System.Windows.Controls;

namespace Interactivity
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			InitializeComponent();
			this.DataContext = TradeData.GetWeeklyData();
		}

		private void ChartArea_ItemClick(object sender, Telerik.Windows.Controls.Charting.ChartItemClickEventArgs e)
		{
			TradeData td = e.DataPoint.DataItem as TradeData;
			MessageBox.Show(
				string.Format(
					"Trading details for {0:d}:\n\nOpen\t: {1:c}\nHigh\t: {2:c}\nLow\t: {3:c}\nClose\t: {4:c}\n\nVolume\t: {5}",
					td.FromDate, td.Open, td.High, td.Low, td.Close, td.Volume),
				td.Emission,
				MessageBoxButton.OK);
		}
	}
}
