using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using Telerik.Windows.Controls.ScheduleView;

namespace DragDropWithScheduleView
{
	public partial class Example : UserControl
	{
		public Example()
		{
			InitializeComponent();
		}

		private void scheduleView_Loaded_1(object sender, System.Windows.RoutedEventArgs e)
		{
			var appointments = scheduleView.AppointmentsSource as ObservableCollection<Appointment>;
			var firstAppointment = appointments[2];
			scheduleView.ScrollIntoView(firstAppointment);
		}
	}
}
