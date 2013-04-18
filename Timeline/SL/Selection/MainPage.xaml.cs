using System.Windows.Controls;
using Telerik.Windows.Controls;

namespace Selection
{
	public partial class MainPage : UserControl
	{
		public MainPage()
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
