using System;
using System.Collections.ObjectModel;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ScheduleView;

namespace AvoidOverlappingAppointments
{
	public class ViewModel : ViewModelBase
	{
        private Appointment _selectedAppointment;

        public Appointment SelectedAppointment
        {
            get { return this._selectedAppointment; }
            set
            {
                if (this._selectedAppointment != value)
                {
                    this._selectedAppointment = value;
                    this.OnPropertyChanged("SelectedAppointment");
                }
            }
        }

		public ObservableCollection<Appointment> Appointments { get; set; }

		public ViewModel()
		{
            var date = CalendarHelper.GetFirstDayOfWeek(DateTime.Today, DayOfWeek.Monday);

            var meetingApp = new Appointment()
            {
                Subject = "Meeting with John",
                Start = date.AddHours(7),
                End = date.AddHours(8)
            };
            meetingApp.Resources.Add(new Resource("Room 1", "Room"));

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
			Appointments = new ObservableCollection<Appointment>() { scrumApp, meetingApp };
            this.SelectedAppointment = meetingApp;
		}
	}
}
