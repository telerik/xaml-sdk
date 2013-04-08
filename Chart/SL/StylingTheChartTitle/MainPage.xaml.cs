using System.Windows.Controls;

namespace StylingTheChartTitle
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			InitializeComponent();
			this.DataContext = new int[] { 1, 5, 6, 9, 5, 7 };
		}
	}
}
