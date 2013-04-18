using System;
using System.Collections.ObjectModel;
using System.Linq;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GanttView;
using Telerik.Windows.Controls.ScheduleView;
using Telerik.Windows.Controls.Scheduling;

namespace DragDropFromExternalSource
{
	public class ViewModel : ViewModelBase
	{
		private ObservableCollection<GanttTask> tasks;
		private ObservableCollection<Project> projects;
		private ObservableCollection<Appointment> appointments;
		private DateRange visibleRange;

		public ViewModel()
		{
			this.tasks = new SoftwarePlanning();
			this.projects = this.GetProjects();
			this.appointments = this.GetAppointments();

			var start = this.tasks.Min(t => t.Start).Date;
			var end = this.tasks.Max(t => t.End).Date;

			this.visibleRange = new DateRange(start.AddHours(-12), end.AddDays(3));
		}

		public DayOfWeek FirstDayOfWeek
		{
			get
			{
				return DateTime.Today.DayOfWeek;
			}
		}

		public ObservableCollection<GanttTask> GanttTasks
		{
			get
			{
				return this.tasks;
			}

			set
			{
				this.tasks = value;
				this.OnPropertyChanged(() => this.GanttTasks);
			}
		}

		public ObservableCollection<Project> Projects
		{
			get
			{
				return this.projects;
			}

			set
			{
				this.projects = value;
				this.OnPropertyChanged(() => this.Projects);
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
				this.appointments = value;
				this.OnPropertyChanged(() => this.Appointments);
			}
		}

		public DateRange VisibleRange
		{
			get
			{
				return this.visibleRange;
			}

			set
			{
				this.visibleRange = value;
				this.OnPropertyChanged(() => this.VisibleRange);
			}
		}

		private ObservableCollection<Project> GetProjects()
		{
			var projts = new ObservableCollection<Project>();
			projts.Add(new Project { Name = "Some project name", Start = DateTime.Today, End = DateTime.Today.AddDays(2) });
			projts.Add(new Project { Name = "Some other project name", Start = DateTime.Today.AddDays(2), End = DateTime.Today.AddDays(4) });
			return projts;
		}

		private ObservableCollection<Appointment> GetAppointments()
		{
			var retValue = new ObservableCollection<Appointment>();

			retValue.Add(new Appointment { Subject = "Alexander Tuckings", Body = "Software Developer - C#, VB.NET / JavaScript", Start = DateTime.Today.AddHours(8), End = DateTime.Today.AddHours(16) });
			retValue.Add(new Appointment { Subject = "Pett Grishith", Body = "ASP.NET Support Officer - Microsot SQL Server 2005 /2008", Start = DateTime.Today.AddDays(1).AddHours(8), End = DateTime.Today.AddDays(1).AddHours(16) });
			retValue.Add(new Appointment { Subject = "Earick Danstin", Body = "Software Developer - experience with C# and ASP.NET, experience with MS SQL Server", Start = DateTime.Today.AddDays(2).AddHours(8), End = DateTime.Today.AddDays(2).AddHours(16) });

			return retValue;
		}
	}
}
