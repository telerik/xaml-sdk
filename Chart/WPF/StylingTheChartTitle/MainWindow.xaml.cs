using System.Windows;

namespace StylingTheChartTitle
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			this.DataContext = new int[] { 1, 5, 6, 9, 5, 7 };
		}
	}
}
