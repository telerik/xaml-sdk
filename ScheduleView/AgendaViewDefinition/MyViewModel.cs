using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ScheduleView;

namespace AgendaViewDefinition
{
    public class MyViewModel : ViewModelBase
    {
        private Appointment selectedItem;

        public MyViewModel()
        {
            this.Appointments = this.GetAppointments(5);

            this.SortedAppointments = new CollectionViewSource();
            this.SortedAppointments.Source = this.Appointments;
            this.SortedAppointments.SortDescriptions.Add(new SortDescription("Start", ListSortDirection.Ascending));
        }

        public ObservableCollection<Appointment> Appointments { get; set; }

        public CollectionViewSource SortedAppointments { get; set; }

        public Appointment SelectedItem
        {
            get
            {
                return this.selectedItem;
            }

            set
            {
                if (this.selectedItem != value)
                {
                    this.selectedItem = value;
                    this.OnPropertyChanged(() => this.SelectedItem);
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
    }
}
