using System.Windows;

namespace InsertNewRowOnEnterAndScroll
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			this.playersGrid.KeyboardCommandProvider = new CustomKeyboardCommandProvider(this.playersGrid);
		}
	}
}
