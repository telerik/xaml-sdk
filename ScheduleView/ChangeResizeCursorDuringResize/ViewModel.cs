using System;
using System.Collections.ObjectModel;
using Telerik.Windows.Controls.ScheduleView;

namespace ChangeResizeCursorAtRuntime
{
    public class ViewModel
    {
        private ObservableCollection<Appointment> appointments;

        public ViewModel()
        {
            this.appointments = new ObservableCollection<Appointment>();

            var app = new Appointment { Start = DateTime.Today.AddHours(8), End = DateTime.Today.AddHours(9), Subject = "Scrum meeting" };
            this.appointments.Add(app);
        }

        public ObservableCollection<Appointment> Appointments
        {
            get
            {
                return this.appointments;
            }
            set
            {
                this.appointments = value;
            }
        }
    }
}
