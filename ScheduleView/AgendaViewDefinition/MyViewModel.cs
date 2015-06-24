using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ScheduleView;
using System.Linq;

namespace AgendaViewDefinition
{
    public class MyViewModel : ViewModelBase
    {
        private Appointment selectedAppointment;

        private ObservableCollection<Appointment> listBoxAppointments;

        public MyViewModel()
        {
            this.Appointments = this.GetAppointments(5);
            this.Appointments.CollectionChanged += Appointments_CollectionChanged;
            this.ListBoxAppointments = new ObservableCollection<Appointment>(this.Appointments);
            this.ChangeRangeCommand = new DelegateCommand(OnChangeRangeCommandExecute);
        }

        public ICommand ChangeRangeCommand { get; set; }
        public ObservableCollection<Appointment> Appointments { get; set; }

        public Appointment SelectedAppointment
        {
            get
            {
                return this.selectedAppointment;
            }

            set
            {
                if (this.selectedAppointment != value)
                {
                    this.selectedAppointment = value;
                    this.OnPropertyChanged(() => this.SelectedAppointment);
                }
            }
        }

        public ObservableCollection<Appointment> ListBoxAppointments 
        { 
            get
            {
                return this.listBoxAppointments;
            }
            set
            {
                if(this.listBoxAppointments != value)
                {
                    this.listBoxAppointments = value;
                    this.OnPropertyChanged("ListBoxAppointments");
                }
            }
        }

        private ObservableCollection<Appointment> GetAppointments(int appCount)
        {
            var now = DateTime.Today;
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

        private void OnChangeRangeCommandExecute(object obj)
        {
            this.ListBoxAppointments = new ObservableCollection<Appointment>(this.ListBoxAppointments.OrderBy(a => a.Start));
        }

        private void Appointments_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                this.ListBoxAppointments.AddRange(e.NewItems);
            }

            if (e.OldItems != null)
            {
                foreach (Appointment item in e.OldItems)
                {
                    this.ListBoxAppointments.Remove(item);
                }
            }
        }
    }
}
