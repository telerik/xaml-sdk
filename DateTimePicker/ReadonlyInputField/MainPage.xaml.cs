using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;

namespace ReadonlyInputField
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			InitializeComponent();
		}

		private void RadDateTimePickerLoaded(object sender, RoutedEventArgs e)
		{
			var datePicker = sender as RadDateTimePicker;
			this.Dispatcher.BeginInvoke(() =>
			{
				var datePickerTextBox = datePicker.ChildrenOfType<RadWatermarkTextBox>().FirstOrDefault() as RadWatermarkTextBox;
				datePickerTextBox.IsReadOnly = true;
			});
		}

		private void RadDatePickerLoaded(object sender, RoutedEventArgs e)
		{
			var datePicker = sender as RadDatePicker;
			this.Dispatcher.BeginInvoke(() =>
			{
				var datePickerTextBox = datePicker.ChildrenOfType<RadWatermarkTextBox>().FirstOrDefault() as RadWatermarkTextBox;
				datePickerTextBox.IsReadOnly = true;
			});
		}
	}
}
