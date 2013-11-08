using System;
using System.Collections.ObjectModel;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ScheduleView;

namespace RemoveDeleteButton
{
    public class ViewModel : ViewModelBase
    {
        private ObservableCollection<Appointment> appointments;

        /// <summary>
        /// Gets or sets Appointments and notifies for changes
        /// </summary>
        public ObservableCollection<Appointment> Appointments
        {
            get
            {
                return this.appointments;
            }

            set
            {
                if (this.appointments != value)
                {
                    this.appointments = value;
                    this.OnPropertyChanged(() => this.Appointments);
                }
            }
        }

        public ViewModel()
        {
            this.Appointments = new ObservableCollection<Appointment>
            {
                new Appointment { Start = DateTime.Now, End = DateTime.Now.AddHours(1), Subject = "Appointment 1" },
                new Appointment { Start = DateTime.Now.AddHours(2), End = DateTime.Now.AddHours(3), Subject = "Appointment 2" },
                new Appointment { Start = DateTime.Now.AddHours(4), End = DateTime.Now.AddHours(5), Subject = "Appointment 3" }
            };
        }
    }
}