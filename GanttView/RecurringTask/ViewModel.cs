using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GanttView;
using Telerik.Windows.Controls.Scheduling;

namespace RecurringTask
{
	public class ViewModel : ViewModelBase
	{
        private ObservableCollection<IGanttTask> tasks;

		private VisibleRange visibleRange;

		private ITimeLineVisualizationBehavior timeLineRecurrenceBehavior;

		public ViewModel()
		{
			this.visibleRange = new VisibleRange() { Start = DateTime.Today, End = DateTime.Today.AddDays(3) };
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
            var today = DateTime.Today.AddHours(8);

            var child1 = new RecurrenceTask(today, today.AddHours(4), "reccurence1");
            var child2 = new RecurrenceTask(today.AddHours(8), today.AddHours(12), "reccurence2");
            var child3 = new RecurrenceTask(today.AddHours(16), today.AddHours(20), "reccurence3");

            var task1 = new RecurrenceTask(today, today.AddHours(20), "recurrence")
            {
                Children = { child1, child2, child3 }
            };

            collection.Add(task1);

            var taskWithoutRecurrence = new GanttTask(today.AddHours(8), today.AddHours(13), "Task Without Recurrence");
            taskWithoutRecurrence.Children.Add(new GanttTask(today.AddHours(9), today.AddHours(12), "Child Task"));
            collection.Add(taskWithoutRecurrence);

            return collection;
		}
	}
}
