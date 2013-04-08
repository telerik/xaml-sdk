using System.Windows.Controls;

namespace Tooltips
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
