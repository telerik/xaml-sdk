using System.Windows;
using System.Windows.Controls;

namespace StylingTheAxisLine
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			InitializeComponent();
			this.DataContext = new int[] { 1, 5, 6, 9, 5, 7 };
			this.radChart.DefaultView.ChartArea.AxisX.AxisStyles.AxisLineStyle = this.Resources["AxisLineStyle"] as Style;
			this.radChart.DefaultView.ChartArea.AxisY.AxisStyles.AxisLineStyle = this.Resources["AxisLineStyle"] as Style;
		}
	}
}
