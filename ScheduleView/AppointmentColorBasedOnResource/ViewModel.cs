
using System;
using System.Collections.ObjectModel;
using System.Windows.Media;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ScheduleView;

namespace AppointmentColorBasedOnResource
{
	public class ViewModel
	{
		public ObservableCollection<Appointment> Appointments { get; set; }

		public ViewModel()
		{
			var app = new Appointment()
			{
				Start = DateTime.Now,
				End = DateTime.Now.AddHours(1),
				Subject = "Test Appointment"
			};

            app.Resources.Add(new Resource("Room1", "Room"));
            app.Resources.Add(new Resource("Room2", "Room"));

			this.Appointments = new ObservableCollection<Appointment>() { app };

		}
	}
}
