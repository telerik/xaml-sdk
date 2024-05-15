using System;
using System.Collections.ObjectModel;
using Telerik.Windows.Controls.ScheduleView;

namespace SlotsAndAppointmentsNavigation
{
	public class ViewModel
	{
		public ObservableCollection<Appointment> Appointments { get; set; }

		public ViewModel()
		{
			this.Appointments = new ObservableCollection<Appointment>();

			var today = DateTime.Today;

			for (int i = 1; i <= 10; i++)
			{
				var app = new Appointment()
				{
					Subject = "Test Appointment " + i.ToString(),
					Start = today.AddDays(i-1).AddHours(i * 2),
					End = today.AddDays(i-1).AddHours(i * 2 + 1)
				};
				this.Appointments.Add(app);
			}
		}
	}
}
