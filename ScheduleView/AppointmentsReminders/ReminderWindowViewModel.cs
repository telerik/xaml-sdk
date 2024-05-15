using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Windows.Controls;

namespace AppointmentsReminders
{
    public class ReminderWindowViewModel : ViewModelBase
    {
        private DelegateCommand dismissCommand;
        private DelegateCommand snoozeCommand;
        private IEnumerable<TimeSpan> reminderSource;
        private ViewModel mainViewModel;
        private TimeSpan? selectedReminder;
        private CustomAppointment selectedAppointment;
        private DelegateCommand editAppointment;

        public ReminderWindowViewModel(ViewModel viewModel, RadScheduleView scheduleView)
        {
            this.MainViewModel = viewModel;
            this.MainViewModel.PropertyChanged += MainViewModel_PropertyChanged;
            this.DismissCommand = new DelegateCommand(this.DismissCommandExecuted, this.DismissCommandCanExecute);
            this.SnoozeCommand = new DelegateCommand(this.SnoozeCommandExecuted, this.SnoozeCommandCanExecute);
            this.ScheduleView = scheduleView;
            this.EditAppointmentCommand = new DelegateCommand(this.EditAppointmentCommandExecuted, this.EditAppointmentCommandCanExecute);
        }

        void MainViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            this.SelectedAppointment = this.MainViewModel.SelectedAppointment;
            this.SelectedReminder = this.MainViewModel.SelectedReminder;
        }

        private RadScheduleView scheduleView;

        public RadScheduleView ScheduleView
        {
            get { return scheduleView; }
            set { scheduleView = value; }
        }


        public ViewModel MainViewModel
        {
            get { return mainViewModel; }
            set 
            {
                if (mainViewModel != value)
                {
                    mainViewModel = value;
                    OnPropertyChanged(() => this.MainViewModel);
                }
            }
        }

        public IEnumerable<TimeSpan> ReminderSource
        {
            get
            {
                return reminderSource = this.MainViewModel.ReminderSource;
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

        public TimeSpan? SelectedReminder
        {
            get { return selectedReminder =  this.MainViewModel.SelectedReminder; }
            set
            {
                if (selectedReminder != value)
                {
                    selectedReminder = value;
                    OnPropertyChanged(() => this.SelectedReminder);
                }
            }
        }

        public CustomAppointment SelectedAppointment
        {
            get { return selectedAppointment = this.MainViewModel.SelectedAppointment; }
            set
            {
                if (value != selectedAppointment)
                {
                    selectedAppointment = value;
                    OnPropertyChanged(() => this.SelectedAppointment);
                }
            }
        }

        public DelegateCommand DismissCommand
        {
            get
            {
                return this.dismissCommand;
            }
            set
            {
                this.dismissCommand = value;
            }
        }

        public DelegateCommand SnoozeCommand
        {
            get
            {
                return this.snoozeCommand;
            }
            set
            {
                this.snoozeCommand = value;
            }
        }

        public DelegateCommand EditAppointmentCommand
        {
            get
            {
                return this.editAppointment;
            }
            set
            {
                this.editAppointment = value;
            }
        }

        public void DismissCommandExecuted(object parameter)
        {
            var app = (CustomAppointment)parameter;
            app.SelectedReminder = null;
            WindowCommands.Close.Execute(null, null);
        }

        public bool DismissCommandCanExecute(object parameter)
        {
            return true;
        }

        public void SnoozeCommandExecuted(object parameter)
        {
            WindowCommands.Close.Execute(null, null);
        }

        public bool SnoozeCommandCanExecute(object parameter)
        {
            return true;
        }

        public void EditAppointmentCommandExecuted(object parameter)
        {
            WindowCommands.Close.Execute(null, null);
            RadScheduleViewCommands.EditAppointment.Execute(parameter as CustomAppointment, this.ScheduleView);
        }

        public bool EditAppointmentCommandCanExecute(object parameter)
        {
            return true;
        }
    }
}
