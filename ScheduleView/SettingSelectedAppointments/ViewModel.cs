using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ScheduleView;

namespace SettingSelectedAppointments
{
    public class ViewModel : ViewModelBase
    {
        private ObservableCollection<Appointment> selectedAppointments;

        public ViewModel()
        {
            this.Appointments = GetAppointments();

            this.SelectedAppointments = new ObservableCollection<Appointment>()
            {
                this.Appointments[0],
                this.Appointments[2],
                this.Appointments[4]
            };

            this.ChangeSelectedAppointments = new DelegateCommand(OnChangeSelectedAppointmentsExecute);
        }

        public ObservableCollection<Appointment> Appointments { get; set; }
        public ICommand ChangeSelectedAppointments { get; set; }

        public ObservableCollection<Appointment> SelectedAppointments
        {
            get
            {
                return this.selectedAppointments;
            }

            set
            {
                if (this.selectedAppointments != value)
                {
                    this.selectedAppointments = value;
                    this.OnPropertyChanged(() => this.SelectedAppointments);
                }
            }
        }

        private ObservableCollection<Appointment> GetAppointments()
        {
            ObservableCollection<Appointment> appointmentList = new ObservableCollection<Appointment>();

            appointmentList.Add(new Appointment { Subject = "App 1", Start = DateTime.Now.AddHours(1), End = DateTime.Now.AddHours(2) });
            appointmentList.Add(new Appointment { Subject = "App 2", Start = DateTime.Now.AddHours(2), End = DateTime.Now.AddHours(3) });
            appointmentList.Add(new Appointment { Subject = "App 3", Start = DateTime.Now.AddDays(1), End = DateTime.Now.AddDays(1).AddHours(1) });
            appointmentList.Add(new Appointment { Subject = "App 4", Start = DateTime.Now.AddDays(2), End = DateTime.Now.AddDays(2).AddHours(1) });
            appointmentList.Add(new Appointment { Subject = "App 5", Start = DateTime.Now.AddDays(3), End = DateTime.Now.AddDays(3).AddHours(1) });
            appointmentList.Add(new Appointment { Subject = "App 6", Start = DateTime.Now.AddDays(4), End = DateTime.Now.AddDays(4).AddHours(1) });

            return appointmentList;
        }

        private void OnChangeSelectedAppointmentsExecute(object obj)
        {
            this.SelectedAppointments = new ObservableCollection<Appointment>()
            {
                this.Appointments[1],
                this.Appointments[3],
                this.Appointments[5]
            };
        }
    }
}
