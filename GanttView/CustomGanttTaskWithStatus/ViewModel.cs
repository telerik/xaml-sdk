using System;
using System.Collections.ObjectModel;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GanttView;
using Telerik.Windows.Controls.Scheduling;

namespace CustomGanttTaskWithStatus
{
	public class ViewModel : ViewModelBase
	{
		private ObservableCollection<MyGanttTask> tasks;

		private DateRange visibleTime;

		public ViewModel()
		{
			var date = DateTime.Now;
			var ganttAPI = new MyGanttTask()
			{
				Start = date,
				End = date.AddDays(2),
				Title = "Design public API",
				Description = "Description: Design public API",
				Status = "Done"
			};
			var ganttRendering = new MyGanttTask()
			{
				Start = date.AddDays(2).AddHours(8),
				End = date.AddDays(4),
				Title = "Gantt Rendering",
				Description = "Description: Gantt Rendering",
				Status = "In progress"
			};
			var ganttDemos = new MyGanttTask()
			{
				Start = date.AddDays(4.5),
				End = date.AddDays(7),
				Title = "Gantt Demos",
				Description = "Description: Gantt Demos",
				Status = "In progress"
			};
			var milestone = new MyGanttTask()
			{
				Start = date.AddDays(7),
				End = date.AddDays(7).AddHours(1),
				Title = "Review",
				Description = "Description: Review",
				IsMilestone = true,
				Status = "Not Done"
			};

			ganttRendering.Dependencies.Add(new Dependency() { FromTask = ganttAPI });
			ganttDemos.Dependencies.Add(new Dependency() { FromTask = ganttRendering });

			var iterationTask = new MyGanttTask()
			{
				Start = date,
				End = date.AddDays(7),
				Title = "Iteration 1",
				Children = { ganttAPI, ganttRendering, ganttDemos, milestone },
				Status = "Not Done"
			};

			this.tasks = new ObservableCollection<MyGanttTask>() { iterationTask };
			this.visibleTime = new DateRange(date.AddDays(-1), date.AddDays(9));
		}

		public ObservableCollection<MyGanttTask> Tasks
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
	}
}
