using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GanttView;
using Telerik.Windows.Controls.Scheduling;

namespace SameRowRecurringTask
{
	public class ViewModel : ViewModelBase
	{
        private ObservableCollection<IGanttTask> tasks;

		private VisibleRange visibleRange;

		private ITimeLineVisualizationBehavior timeLineRecurrenceBehavior;

		public ViewModel()
		{
			this.visibleRange = new VisibleRange() { Start = DateTime.Today, End = DateTime.Today.AddHours(21) };
			this.tasks = this.GetTasks();
			this.timeLineRecurrenceBehavior = new TimeLineRecurrenceBehavior();
		}

        public ObservableCollection<IGanttTask> Tasks
		{
			get
			{
				return this.tasks;
			}
		}

		public VisibleRange VisibleRange
		{
			get
			{
				return visibleRange;
			}

			set
			{
				if (visibleRange != value)
				{
					visibleRange = value;
					OnPropertyChanged(() => VisibleRange);
				}
			}
		}

		public ITimeLineVisualizationBehavior TimeLineRecurrenceBehavior
		{
			get
			{
				return timeLineRecurrenceBehavior;
			}

			set
			{
				if (timeLineRecurrenceBehavior == value)
				{
					return;
				}

				timeLineRecurrenceBehavior = value;
				OnPropertyChanged(() => this.TimeLineRecurrenceBehavior);
			}
		}

        private ObservableCollection<IGanttTask> GetTasks()
		{
            var collection = new ObservableCollection<IGanttTask>();
            var today = DateTime.Today;

            var recurrenceTask1 = new GanttTask(today.AddHours(1), today.AddHours(4), "Reccurence 1");
            var recurrenceTask2 = new GanttTask(today.AddHours(8), today.AddHours(12), "Reccurence 2");
            var recurrenceTask3 = new GanttTask(today.AddHours(16), today.AddHours(20), "Reccurence 3");
            var recurrenceSeriesTask = new RecurrenceTask(today, today.AddHours(20), "Recurrence Series")
            {
                Recurrences = { recurrenceTask1, recurrenceTask2, recurrenceTask3 }
            };
            collection.Add(recurrenceSeriesTask);
            var taskWithoutRecurrence = new GanttTask(today.AddHours(8), today.AddHours(13), "Task Without Recurrence");
            taskWithoutRecurrence.Children.Add(new GanttTask(today.AddHours(9), today.AddHours(12), "Child Task"));
            collection.Add(taskWithoutRecurrence);

            return collection;
		}
	}
}
