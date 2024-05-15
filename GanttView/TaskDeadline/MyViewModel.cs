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

namespace TaskDeadline
{
	public class MyViewModel : ViewModelBase
	{
		private ITimeLineVisualizationBehavior timeLineDeadlineBehavior;
		private DateRange visibleTime;
		private ObservableCollection<GanttTask> tasks;

		public MyViewModel()
		{
			var date = DateTime.Now;
			var ganttAPI = new GanttDeadlineTask()
			{
				Start = date,
				End = date.AddDays(2),
				Title = "Design public API",
				Description = "Description: Design public API",
				GanttDeadLine = date.AddDays(1)
			};
			var ganttRendering = new GanttDeadlineTask()
			{
				Start = date.AddDays(2).AddHours(8),
				End = date.AddDays(4),
				Title = "Gantt Rendering",
				Description = "Description: Gantt Rendering",
				GanttDeadLine = date.AddDays(5)
			};
			var ganttDemos = new GanttDeadlineTask()
			{
				Start = date.AddDays(4.5),
				End = date.AddDays(7),
				Title = "Gantt Demos",
				Description = "Description: Gantt Demos",
				GanttDeadLine = date.AddDays(7)
			};
			var milestone = new GanttDeadlineTask()
			{
				Start = date.AddDays(7),
				End = date.AddDays(7).AddHours(1),
				Title = "Review",
				Description = "Review",
				GanttDeadLine = date.AddDays(8),
				IsMilestone = true
			};
			ganttRendering.Dependencies.Add(new Dependency() { FromTask = ganttAPI });
			ganttDemos.Dependencies.Add(new Dependency() { FromTask = ganttRendering });
			var iterationTask = new GanttTask(date, date.AddDays(7), "Iteration 1")
			{
				Children = { ganttAPI, ganttRendering, ganttDemos, milestone }
			};
			this.tasks = new ObservableCollection<GanttTask>() { iterationTask };
			this.visibleTime = new DateRange(date.AddDays(-1), date.AddDays(9));
			this.timeLineDeadlineBehavior = new TimeLineDeadlineBehavior();
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
