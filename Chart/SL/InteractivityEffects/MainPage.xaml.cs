using System.Windows.Controls;

namespace InteractivityEffects
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			InitializeComponent();
			this.RadChart.ItemsSource = DataObject.GetData();
		}
	}
}
