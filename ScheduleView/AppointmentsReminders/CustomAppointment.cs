using System;
using System.ComponentModel;
using Telerik.Windows.Controls.ScheduleView;

namespace AppointmentsReminders
{
    public class CustomAppointment : Appointment, IEditableObject
    {
        public override IAppointment Copy()
        {
            IAppointment appointment = new CustomAppointment();
            appointment.CopyFrom(this);
            return appointment;
        }

        public override void CopyFrom(IAppointment other)
        {
            CustomAppointment appointment = other as CustomAppointment;
            if (appointment != null)
            {
                this.SelectedReminder = appointment.SelectedReminder;
            }
            base.CopyFrom(other);
        }

        private TimeSpan? selectedReminder;

        public TimeSpan? SelectedReminder
        {
            get { return this.Storage<CustomAppointment>().selectedReminder; ; }
            set
            {
                CustomAppointment storage = this.Storage<CustomAppointment>();
                if (storage.selectedReminder != value)
                {
                    storage.selectedReminder = value;
                    OnPropertyChanged(() => this.SelectedReminder);
                }
            }
        }
    }
}
