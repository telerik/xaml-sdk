using System.Windows;
using System.Windows.Controls;

namespace PrintingAndExporting
{
	/// <summary>
	/// Interaction logic for Example.xaml
	/// </summary>
	public partial class Example : UserControl
	{
		public Example()
		{
			InitializeComponent();
		}

		private void RadButton_Click_1(object sender, RoutedEventArgs e)
		{
			PrintingService.Print(this.GanttView);
		}
	}
}
