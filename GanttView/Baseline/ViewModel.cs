using System;
using System.Collections.ObjectModel;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GanttView;
using Telerik.Windows.Controls.Scheduling;

namespace Baseline
{
	public class ViewModel : ViewModelBase
	{
		private ObservableCollection<GanttBaselineTask> tasks;

		private ITimeLineVisualizationBehavior timeLineBaselineBehavior;

		private DateRange visibleTime;

		public ViewModel()
		{
			var date = DateTime.Now;

			this.tasks = GetTasks(date);

			this.visibleTime = new DateRange(date.AddDays(-1), date.AddDays(11));

			this.timeLineBaselineBehavior = new TimeLineBaselineBehavior();
		}

		public ObservableCollection<GanttBaselineTask> Tasks
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
			get
			{
				return this.visibleTime;
			}

			set
			{
				if (this.visibleTime != value)
				{
					this.visibleTime = value;
					this.OnPropertyChanged(() => this.VisibleTime);
				}
			}
		}

		public ITimeLineVisualizationBehavior TimeLineBaselineBehavior
		{
			get
			{
				return timeLineBaselineBehavior;
			}

			set
			{
				timeLineBaselineBehavior = value;
				OnPropertyChanged(() => this.TimeLineBaselineBehavior);
			}
		}

		private ObservableCollection<GanttBaselineTask> GetTasks(DateTime date)
		{
			var ganttAPI = new GanttBaselineTask()
			{
				Start = date,
				End = date.AddDays(2),
				Title = "Design public API",
				Description = "Description: Design public API",
				StartPlannedDate = date.AddDays(1),
				EndPlannedDate = date.AddDays(2.5)
			};
			var ganttRendering = new GanttBaselineTask()
			{
				Start = date.AddDays(2).AddHours(8),
				End = date.AddDays(4),
				Title = "Gantt Rendering",
				Description = "Description: Gantt Rendering",
				StartPlannedDate = date.AddDays(3),
				EndPlannedDate = date.AddDays(3).AddHours(12)
			};
			var ganttDemos = new GanttBaselineTask()
			{
				Start = date.AddDays(4.5),
				End = date.AddDays(7),
				Title = "Gantt Demos",
				Description = "Description: Gantt Demos",
				StartPlannedDate = date.AddDays(5),
				EndPlannedDate = date.AddDays(6)
			};
			var milestone = new GanttBaselineTask()
			{
				Start = date.AddDays(7),
				End = date.AddDays(7).AddHours(1),
				Title = "Review",
				Description = "Description: Review",
				IsMilestone = true,
				StartPlannedDate = date.AddDays(7),
				EndPlannedDate = date.AddDays(7).AddHours(15)
			};

			ganttRendering.Dependencies.Add(new Dependency() { FromTask = ganttAPI });
			ganttDemos.Dependencies.Add(new Dependency() { FromTask = ganttRendering });

			var iterationTask = new GanttBaselineTask()
			{
				Start = date,
				End = date.AddDays(7),
				Title = "Iteration 1",
				Children = { ganttAPI, ganttRendering, ganttDemos, milestone },
				StartPlannedDate = date
			};

			var iterationTaskEndPlannedDate = GetInterationPlannedDateEnd(iterationTask.Children, date);

			iterationTask.EndPlannedDate = iterationTaskEndPlannedDate;

			ObservableCollection<GanttBaselineTask> tasks = new ObservableCollection<GanttBaselineTask>() { iterationTask };

			return tasks;
		}

		private DateTime GetInterationPlannedDateEnd(System.Collections.Generic.IList<IGanttTask> list, DateTime date)
		{
			DateTime result = date;
			foreach (GanttBaselineTask task in list)
			{
				var plannedDaysDifference = (task.EndPlannedDate - task.StartPlannedDate);
				result = result.Add(plannedDaysDifference);
			}
			return result;
		}
	}
}
