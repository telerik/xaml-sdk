using System;
using System.Collections.ObjectModel;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GanttView;
using Telerik.Windows.Controls.Scheduling;

namespace StartEndEventContainers
{
	public class MyViewModel : ViewModelBase
	{
        private LabelVisualizationBehavior labelVisualizationBehavior;
		private DateRange visibleTime;
        private ObservableCollection<GanttTask> tasks;

		public MyViewModel()
		{
			var date = DateTime.Now;
			var ganttAPI = new GanttTask()
			{
				Start = date,
				End = date.AddDays(2),
				Title = "Design public API",
				Description = "Description: Design public API",
			};
            var ganttRendering = new GanttTask()
			{
				Start = date.AddDays(2).AddHours(8),
				End = date.AddDays(4),
				Title = "Gantt Rendering",
				Description = "Description: Gantt Rendering",
			};
            var ganttDemos = new GanttTask()
			{
				Start = date.AddDays(4.5),
				End = date.AddDays(7),
				Title = "Gantt Demos",
				Description = "Description: Gantt Demos",
			};
            var milestone = new GanttTask()
			{
				Start = date.AddDays(7),
				End = date.AddDays(7).AddHours(1),
				Title = "Review",
				Description = "Review",
				IsMilestone = true
			};
			ganttRendering.Dependencies.Add(new Dependency() { FromTask = ganttAPI });
			ganttDemos.Dependencies.Add(new Dependency() { FromTask = ganttRendering });
            var iterationTask = new GanttTask()
			{
                Start = date,
                End = date.AddDays(7),
                Title = "Iteration 1",
				Children = { ganttAPI, ganttRendering, ganttDemos, milestone }
			};
			this.Tasks = new ObservableCollection<GanttTask>() { iterationTask };
			this.VisibleTime = new DateRange(date.AddDays(-2), date.AddDays(10));
            this.LabelVisualizationBehavior = new LabelVisualizationBehavior();
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

        public LabelVisualizationBehavior LabelVisualizationBehavior
		{
			get
			{
				return labelVisualizationBehavior;
			}
			set
			{
				labelVisualizationBehavior = value;
				this.OnPropertyChanged(() => this.LabelVisualizationBehavior);
			}
		}
	}
}
