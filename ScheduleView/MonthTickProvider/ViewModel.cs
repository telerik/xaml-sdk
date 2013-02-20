using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using Telerik.Windows.Controls.ScheduleView;

namespace MonthTickProvider
{
	public class ViewModel
	{
		public ObservableCollection<Appointment> Appointments { get; set; }

		public ViewModel()
		{
			this.Appointments = new ObservableCollection<Appointment>();
		}
	}
}
