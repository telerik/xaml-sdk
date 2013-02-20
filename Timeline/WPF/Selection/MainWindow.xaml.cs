using System.Windows;
using Telerik.Windows.Controls;

namespace Selection
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void RadTimeline_SelectionChanged(object sender, SelectionChangeEventArgs e)
		{
			var selectedItem = (sender as RadTimeline).SelectedItem;

			if (selectedItem != null)
			{
				//Do something with the selected item.
			}
		}
	}
}
