using System.Windows;

namespace Introduction
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			this.chart.Series[0].ItemsSource = new double[] { 20, 30, 50, 10, 60, 40, 20, 80 };
		}
	}
}
