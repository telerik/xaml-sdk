using System.Linq;
using System.Windows.Controls;

namespace SmartLabels
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			InitializeComponent();
			this.DataContext = ChartData.GetSampleData().Select(cd => cd.YVal);
		}
	}
}
