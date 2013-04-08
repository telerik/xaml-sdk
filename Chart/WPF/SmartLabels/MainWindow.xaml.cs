using System.Linq;
using System.Windows;

namespace SmartLabels
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			this.DataContext = ChartData.GetSampleData().Select(cd => cd.YVal);
		}
	}
}
