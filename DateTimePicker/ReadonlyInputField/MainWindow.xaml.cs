using System.Linq;
using System.Windows;
using Telerik.Windows.Controls;

namespace ReadonlyInputField
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml.
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void RadDateTimePickerLoaded(object sender, RoutedEventArgs e)
		{
			var datePicker = sender as RadDateTimePicker;
			var datePickerTextBox = datePicker.ChildrenOfType<RadWatermarkTextBox>().FirstOrDefault() as RadWatermarkTextBox;
			datePickerTextBox.IsReadOnly = true;
		}

		private void RadDatePickerLoaded(object sender, RoutedEventArgs e)
		{
			var datePicker = sender as RadDatePicker;
			var datePickerTextBox = datePicker.ChildrenOfType<RadWatermarkTextBox>().FirstOrDefault() as RadWatermarkTextBox;
			datePickerTextBox.IsReadOnly = true;
		}
	}
}
