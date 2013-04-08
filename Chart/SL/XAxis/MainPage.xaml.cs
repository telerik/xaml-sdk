using System.Windows.Controls;

namespace XAxis
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
			InitializeComponent();
			this.DataContext = ChartData.GetSampleData();
		}
	}
}
