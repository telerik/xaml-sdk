using System.Windows;

namespace CartesianChartGrid
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			this.chart.Series[0].ItemsSource = new double[] { 20, 40, 35, 40, 30, 50 };
		}
	}
}
