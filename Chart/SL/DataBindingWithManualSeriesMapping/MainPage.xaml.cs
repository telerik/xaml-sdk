using System.Windows.Controls;

namespace DataBindingWithManualSeriesMapping
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			InitializeComponent();
			this.DataContext = TradeData.GetWeeklyData();
		}
	}
}
