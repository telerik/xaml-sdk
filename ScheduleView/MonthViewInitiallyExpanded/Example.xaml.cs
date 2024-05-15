using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using Telerik.Windows.Controls.ScheduleView;

namespace MonthViewInitiallyExpanded
{
	/// <summary>
	/// Interaction logic for Example.xaml
	/// </summary>
	public partial class Example : UserControl
	{
		public Example()
		{
            var start = new DateTime(2012, 6, 1);
            this.DataContext = new Data
            {
                Appointmentes = new ObservableCollection<Appointment>(Enumerable.Range(0, 20).Select(i => new Appointment { Subject = i.ToString(), Start = start, End = start.AddDays(1) })),
                CurrentDate = start
            };
			InitializeComponent();
		}
	}
}
