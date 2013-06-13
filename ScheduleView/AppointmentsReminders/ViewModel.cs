using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Threading;
using Telerik.Windows.Controls;

namespace AppointmentsReminders
{
    public class ViewModel : ViewModelBase
    {
        private static ObservableCollection<CustomAppointment> appointments;
        private DispatcherTimer dispatcherTimer;
        ReminderWindow reminderWindow;
        ReminderWindowViewModel reminderViewModel;

        public ObservableCollection<CustomAppointment> Appointments
        {
            get
            {
                if (appointments == null)
                {
                    return appointments = GetAppointments();
                }
                appointments.CollectionChanged += _appointments_CollectionChanged;
                return appointments;
            }
        }

        public ViewModel(RadScheduleView sv)
        {
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
            dispatcherTimer.Start();
            reminderViewModel = new ReminderWindowViewModel(this, sv);
            reminderWindow = new ReminderWindow() { DataContext = reminderViewModel };
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            this.SetReminder();
        }

        private ObservableCollection<CustomAppointment> GetAppointments()
        {
            var _appointments = new ObservableCollection<CustomAppointment>();
            _appointments.Add(new CustomAppointment { Start = DateTime.Now, End = DateTime.Now.AddHours(2), Subject = "Test" });

            return _appointments;
        }

        void _appointments_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (this.Appointments != null)
            {
                this.SetReminder();
            }
        }

        private IEnumerable<TimeSpan> GetReminders()
        {
            var _reminderItems = new List<TimeSpan>();
            _reminderItems.Add(new TimeSpan());
            _reminderItems.Add(new TimeSpan(0, 5, 0));
            _reminderItems.Add(new TimeSpan(0, 10, 0));
            _reminderItems.Add(new TimeSpan(0, 30, 0));
            _reminderItems.Add(new TimeSpan(1, 0, 0));
            _reminderItems.Add(new TimeSpan(2, 0, 0));

            return _reminderItems as IEnumerable<TimeSpan>;
        }

        private IEnumerable<TimeSpan> reminderSource;

        public IEnumerable<TimeSpan> ReminderSource
        {
            get
            {
                return reminderSource = this.GetReminders();
            }
            set
            {
                if (reminderSource != value)
                {
                    reminderSource = value;
                    OnPropertyChanged(() => this.ReminderSource);
                }
            }
        }

        private TimeSpan? selectedReminder;
        public TimeSpan? SelectedReminder
        {
            get { return selectedReminder; }
            set
            {
                if (selectedReminder != value)
                {
                    selectedReminder = value;
                    this.SetReminder();
                    OnPropertyChanged(() => this.SelectedReminder);
                }
            }
        }

        private CustomAppointment selectedAppointment;
        public CustomAppointment SelectedAppointment
        {
            get { return selectedAppointment; }
            set
            {
                if (value != selectedAppointment)
                {
                    selectedAppointment = value;
                    OnPropertyChanged(() => this.SelectedAppointment);
                    this.SetReminder();
                }
            }
        }

        private void SetReminder()
        {
            var startList = this.Appointments
                .Where(a => a.SelectedReminder != null).ToList();
            foreach (var app in startList)
            {
				if (app.Start <= DateTime.Now.Add(app.SelectedReminder.Value))
                {
                    this.SelectedAppointment = app;
                    if (this.SelectedAppointment != null && this.SelectedAppointment.SelectedReminder != null)
                    {
                        reminderWindow.Show();
                    }
                }
            }
        }
    }
}
