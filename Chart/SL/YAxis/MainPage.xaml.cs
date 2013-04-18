using System.Windows.Controls;

namespace YAxis
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			InitializeComponent();
			this.DataContext = ChartData.GetSampleData();
		}
	}
}
