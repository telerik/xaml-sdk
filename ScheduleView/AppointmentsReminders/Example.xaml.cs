using System.Windows.Controls;
using Telerik.Windows.Controls;

namespace AppointmentsReminders
{
	public partial class Example : UserControl
	{
		ViewModel viewModel;

		public Example()
		{
            viewModel = new ViewModel(this.scheduleView);
            this.DataContext = viewModel;
			InitializeComponent();
		}

		private void RadScheduleView_ShowDialog(object sender, Telerik.Windows.Controls.ShowDialogEventArgs e)
		{
			var selectedAppointment = ((Telerik.Windows.Controls.RadScheduleView)(sender)).SelectedAppointment as CustomAppointment;
			viewModel.SelectedAppointment = selectedAppointment;
			var additionalData = viewModel;
			e.DialogViewModel.AdditionalData = additionalData;
		}

		private void RadScheduleView_DialogClosing(object sender, Telerik.Windows.Controls.CancelRoutedEventArgs e)
		{
			var args = e as CloseDialogEventArgs;
			var selectedAppointment = ((Telerik.Windows.Controls.RadScheduleView)(sender)).SelectedAppointment as CustomAppointment;
			if (args != null && args.DialogResult.HasValue && args.DialogResult.Value && selectedAppointment != null)
			{
				var selectedReminder = selectedAppointment.SelectedReminder;

				if (selectedReminder != this.viewModel.SelectedReminder)
				{
					this.viewModel.SelectedReminder = selectedReminder;
				}
			}
		}
	}
}
