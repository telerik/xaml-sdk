using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GanttView;
using Telerik.Windows.Controls.Scheduling;

namespace ProjectDeadline
{
	public class MyViewModel : ViewModelBase
	{
		private ObservableCollection<GanttTask> tasks;
		private DateRange visibleTime;
		private DateTime projectDeadline = DateTime.Today;
		private ITimeLineVisualizationBehavior timeLineDeadlineBehavior;

		public MyViewModel()
		{
			var date = DateTime.Now;
			var ganttAPI = new GanttTask(date, date.AddDays(2), "Design public API") { Description = "Description: Design public API" };

			var ganttRendering = new GanttTask(date.AddDays(2).AddHours(8), date.AddDays(4), "Gantt Rendering") { Description = "Description: Gantt Rendering" };

			var ganttDemos = new GanttTask(date.AddDays(4.5), date.AddDays(7), "Gantt Demos") { Description = "Description: Gantt Demos" };

			var milestone = new GanttTask(date.AddDays(7), date.AddDays(7).AddHours(1), "Review") { Description = "Review", IsMilestone = true };


			ganttRendering.Dependencies.Add(new Dependency { FromTask = ganttAPI });
			ganttDemos.Dependencies.Add(new Dependency { FromTask = ganttRendering });

			var iterationTask = new GanttTask(date, date.AddDays(7), "Iteration 1")
			{
				Children = { ganttAPI, ganttRendering, ganttDemos, milestone }
			};

			this.tasks = new ObservableCollection<GanttTask>() { iterationTask };
			this.visibleTime = new DateRange(date.AddDays(-1), date.AddDays(9));
			this.timeLineDeadlineBehavior = new TimeLineDeadlineBehavior();

			this.ProjectDeadline = date.AddDays(8);
		}

		public ObservableCollection<GanttTask> Tasks
		{
			get
			{
				return tasks;
			}
			set
			{
				tasks = value;
				OnPropertyChanged(() => Tasks);
			}
		}

		public DateRange VisibleTime
		{
			get { return this.visibleTime; }
			set
			{
				if (this.visibleTime != value)
				{
					this.visibleTime = value;
					this.OnPropertyChanged(() => this.VisibleTime);
				}
			}
		}

		public DateTime ProjectDeadline
		{
			get { return this.projectDeadline; }
			set
			{
				if (this.projectDeadline != value)
				{
					this.projectDeadline = value;
					var behavior = this.timeLineDeadlineBehavior as TimeLineDeadlineBehavior;
					if (behavior != null)
					{
						behavior.ProjectDeadline = value;
					}
					this.OnPropertyChanged(() => this.ProjectDeadline);
				}
			}
		}

		public ITimeLineVisualizationBehavior TimeLineDeadlineBehavior
		{
			get
			{
				return timeLineDeadlineBehavior;
			}
			set
			{
				timeLineDeadlineBehavior = value;
				OnPropertyChanged(() => this.TimeLineDeadlineBehavior);
			}
		}
	}
}
