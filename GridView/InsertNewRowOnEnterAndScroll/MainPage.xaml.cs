using System.Windows.Controls;

namespace InsertNewRowOnEnterAndScroll
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			InitializeComponent();
			this.playersGrid.KeyboardCommandProvider = new CustomKeyboardCommandProvider(this.playersGrid);
		}
	}
}
