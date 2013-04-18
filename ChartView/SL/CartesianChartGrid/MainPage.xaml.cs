using System.Windows.Controls;

namespace CartesianChartGrid
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			InitializeComponent();
			this.chart.Series[0].ItemsSource = new double[] { 20, 40, 35, 40, 30, 50 };
		}
	}
}
