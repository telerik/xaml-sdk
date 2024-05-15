using System;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ScheduleView;

namespace CustomCurrentTimeIndicatorStyle
{
    public class ViewModel : ViewModelBase
    {
        private DispatcherTimer dateTimeTimer;
        private string currentTime;

        public ViewModel()
        {
            this.Appointments = this.GenerateAppointments();

            this.CurrentTime = DateTime.Now.ToString("h:mm tt");

            this.dateTimeTimer = new DispatcherTimer();
            this.dateTimeTimer.Interval = TimeSpan.FromSeconds(1);
            this.dateTimeTimer.Tick += new EventHandler(this.DateTimeTimerTick);

            this.dateTimeTimer.Start();
        }

        public ObservableCollection<Appointment> Appointments
        {
            get;
            private set;
        }

        public string CurrentTime
        {
            get
            {
                return this.currentTime;
            }

            set
            {
                if(this.currentTime != value)
                {
                    this.currentTime = value;
                    this.OnPropertyChanged(() => this.CurrentTime);
                }
            }
        }

        private void DateTimeTimerTick(object sender, EventArgs e)
        {
            this.CurrentTime = DateTime.Now.ToString("h:mm tt");
        }

        private ObservableCollection<Appointment> GenerateAppointments()
        {
            var customAppointments = new ObservableCollection<Appointment>();

            for (int i = 1; i <= 5; i++)
            {
                customAppointments.Add(new Appointment() { 
                    Subject = "Appointment " + i, 
                    Start = DateTime.Now.AddHours(i), 
                    End = DateTime.Now.AddHours(i + 2) 
                });
            }

            return customAppointments;
        }
    }
}
