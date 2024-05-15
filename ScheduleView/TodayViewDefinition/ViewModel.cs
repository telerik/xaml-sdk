using System;
using System.Collections.ObjectModel;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ScheduleView;

namespace TodayViewDefinition
{
    public class ViewModel : ViewModelBase
    {
        private ObservableCollection<Appointment> appointments;

        private DateTime today;

        public ViewModel()
        {
            this.appointments = GetAppointments(5);
            this.Date = DateTime.Today.AddDays(-1);
            this.today = DateTime.Today;
        }

        public DateTime Date { get; set; }

        public DateTime Today
        {
            get
            {
                return this.today;
            }

            set
            {
                if (this.today != value)
                {
                    this.today = value;
                    this.OnPropertyChanged(() => this.Today);
                }
            }
        }

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

        private ObservableCollection<Appointment> GetAppointments(int appCount)
        {
            var now = DateTime.Now;
            var result = new ObservableCollection<Appointment>();
            for (int i = 0; i < appCount; i++)
            {
                result.Add(new Appointment()
                {
                    Start = now.AddHours(i),
                    End = now.AddHours(i + 1),
                    Subject = string.Format("Appointment {0}", i)
                });
            }

            return result;
        }
    }
}
