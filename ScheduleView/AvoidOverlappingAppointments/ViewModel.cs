using System;
using System.Collections.ObjectModel;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ScheduleView;

namespace AvoidOverlappingAppointments
{
	public class ViewModel
	{
		public ObservableCollection<Appointment> Appointments { get; set; }

		public ViewModel()
		{
            var date = CalendarHelper.GetFirstDayOfWeek(DateTime.Today, DayOfWeek.Monday);
            var scrumApp = new Appointment()
            {
                Subject = "Morning Scrum",
                Start = date.AddHours(9),
                End = date.AddHours(9).AddMinutes(30)
            };
            scrumApp.Resources.Add(new Resource("Room 1", "Room"));
            scrumApp.RecurrenceRule = new RecurrenceRule(
                         new RecurrencePattern()
                {
                    Frequency = RecurrenceFrequency.Daily,
                    MaxOccurrences=5
                }
            );

            var meetingApp = new Appointment()
            {
                Subject = "Meeting with John",
                Start = date.AddHours(11),
                End = date.AddHours(12)
            };
            meetingApp.Resources.Add(new Resource("Room 1", "Room"));

			Appointments = new ObservableCollection<Appointment>() { scrumApp, meetingApp };
		}
	}
}
