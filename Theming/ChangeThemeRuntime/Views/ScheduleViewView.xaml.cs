using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows.Controls.ScheduleView;

namespace ChangeThemeRuntime
{
    /// <summary>
    /// Interaction logic for ScheduleViewView.xaml
    /// </summary>
    public partial class ScheduleViewView : UserControl
    {
        public ScheduleViewView()
        {
            InitializeComponent();
            List<Appointment> appointments = new List<Appointment>();

            Appointment app1 = new Appointment();
            app1.Subject = "Appointment 1";
            app1.Start = DateTime.Today.AddHours(10);
            app1.End = DateTime.Today.AddHours(12);

            Appointment app2 = new Appointment();
            app2.Subject = "Appointment 2";
            app2.Start = DateTime.Today.AddHours(12);
            app2.End = DateTime.Today.AddHours(14);

            Appointment app3 = new Appointment();
            app3.Subject = "Appointment 3";
            app3.Start = DateTime.Today.AddHours(11);
            app3.End = DateTime.Today.AddHours(16);

            Appointment app4 = new Appointment();
            app4.Subject = "Appointment 4";
            app4.Start = DateTime.Today.AddHours(13);
            app4.End = DateTime.Today.AddHours(14);

            Appointment app5 = new Appointment();
            app5.Subject = "Appointment 5";
            app5.Start = DateTime.Today.AddHours(14);
            app5.End = DateTime.Today.AddHours(18);

            appointments.Add(app1);
            appointments.Add(app2);
            appointments.Add(app3);
            appointments.Add(app4);
            appointments.Add(app5);

            this.ScheduleView.AppointmentsSource = appointments;
        }
    }
}
