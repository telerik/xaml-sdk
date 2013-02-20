using System.Windows.Controls;
using GridSorting;

namespace GridSorting
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			InitializeComponent();

			this.DataContext = new TasksDataSource();
		}
	}
}
