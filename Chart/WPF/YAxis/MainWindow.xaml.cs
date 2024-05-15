using System.Windows;

namespace YAxis
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			this.DataContext = ChartData.GetSampleData();
		}
	}
}
