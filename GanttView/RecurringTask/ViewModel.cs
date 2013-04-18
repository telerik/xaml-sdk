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
		private ObservableCollection<GanttTask> tasks;

		private VisibleRange visibleRange;

		private ITimeLineVisualizationBehavior timeLineRecurrenceBehavior;

		public ViewModel()
		{
			this.visibleRange = new VisibleRange() { Start = DateTime.Today, End = DateTime.Today.AddDays(1) };
			this.tasks = this.GetTasks();
			this.timeLineRecurrenceBehavior = new TimeLineRecurrenceBehavior();
		}

		public ObservableCollection<GanttTask> Tasks
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

		private ObservableCollection<GanttTask> GetTasks()
		{
			var collection = new ObservableCollection<GanttTask>();
			var today = DateTime.Today.AddHours(8);
			var interval = new TimeSpan(8, 0, 0);
			var recurringTask = new CustomRecurrenceTask(today.AddHours(-5), today.AddHours(-1), "Task with 3 recurrences")
			{
				RecurrenceRule = new RecurrenceRule(today.AddHours(-5), interval, 3)
			};
			collection.Add(recurringTask);
			var normalTask = new GanttTask(today.AddHours(8), today.AddHours(13), "Task Without Recurrence");
			normalTask.Children.Add(new GanttTask(today.AddHours(9), today.AddHours(12), "Child Task"));
			collection.Add(normalTask);

			return collection;
		}
	}
}
