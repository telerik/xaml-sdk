using System.Windows;

namespace InteractivityEffects
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			this.RadChart.ItemsSource = DataObject.GetData();
		}
	}
}
