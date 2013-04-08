using System.Windows;

namespace XAxis
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
			InitializeComponent();
			this.DataContext = ChartData.GetSampleData();
		}
	}
}
