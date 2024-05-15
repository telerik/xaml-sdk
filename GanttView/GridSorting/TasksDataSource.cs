using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Data;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GanttView;
using Telerik.Windows.Controls.Scheduling;

namespace GridSorting
{
	public class TasksDataSource : ViewModelBase
	{
		public TasksDataSource()
		{
			var start = new DateTime(2012, 9, 10);
			this._VisibleRange = new VisibleRange(start, start.AddMonths(1));
			var tasksSource = new ObservableCollection<IGanttTask>
			{
				new GanttTask { Start = start.AddDays(10), End = start.AddDays(13), Title = "Task 1" },
				new GanttTask { Start = start.AddDays(5), End = start.AddDays(10), Title = "Task 2" },
				new GanttTask { Start = start.AddDays(12), End = start.AddDays(25), Title = "Task 3", 
					Children = {
						new GanttTask { Start = start.AddDays(12), End = start.AddDays(14), Title = "Task 3.1" },
						new GanttTask { Start = start.AddDays(20), End = start.AddDays(25), Title = "Task 3.2" },
						new GanttTask { Start = start.AddDays(13), End = start.AddDays(19), Title = "Task 3.3" },
					}},
				new GanttTask { Start = start.AddDays(20), End = start.AddMonths(1).AddDays(-3), Title = "Task 4" },
				new GanttTask { Start = start, End = start.AddMonths(1), Title = "A lot of subtasks" }
			};
			var rand = new Random();
			var task = (GanttTask)tasksSource[4];
			for (int i = 0; i < 1000; i++)
			{
				var taskStart = start.AddDays(rand.Next() % 28);
				var duration = TimeSpan.FromDays(1 + rand.Next() % (30 - (start - taskStart).Days));
				task.Children.Add(new GanttTask(taskStart, taskStart.Add(duration), string.Format("Task {0}", i)));
			}
			this._TasksSource = tasksSource;
		}

		private IDateRange _VisibleRange;

		/// <Summary>Gets or sets VisibleRange and notfinies for changes</Summary>
		public IDateRange VisibleRange
		{
			get { return this._VisibleRange; }
			set
			{
				if (this._VisibleRange != value)
				{
					this._VisibleRange = value;
					this.OnPropertyChanged(() => this.VisibleRange);
				}
			}
		}

		private IEnumerable _TasksSource;

		/// <Summary>Gets or sets TasksSource and notfinies for changes</Summary>
		public IEnumerable TasksSource
		{
			get { return this._TasksSource; }
			set
			{
				if (this._TasksSource != value)
				{
					this._TasksSource = value;
					this.OnPropertyChanged(() => this.TasksSource);
				}
			}
		}

		public void SourtBy(Binding propertyBinding, bool isAscending)
		{
			var propertyName = propertyBinding.Path.Path.Split('.').First();

			var tasks = this._TasksSource as IList<IGanttTask>;
			this.TasksSource = null;

			if (propertyName == "Title")
			{
				Sort(tasks, t => t.Title, isAscending);
			}
			else if (propertyName == "Start")
			{
				Sort(tasks, t => t.Start, isAscending);
			}
			else if (propertyName == "End")
			{
				Sort(tasks, t => t.End, isAscending);
			}

			this.TasksSource = tasks;
		}

		private void Sort<T>(IList<IGanttTask> tasks, Func<IGanttTask, T> sortKeySelector, bool isAscending)
		{
			var sortedTasks = (isAscending ? tasks.OrderBy(sortKeySelector) : tasks.OrderByDescending(sortKeySelector)).ToArray();
			tasks.Clear();
			foreach (var task in sortedTasks)
			{
				tasks.Add(task);
				Sort(task.Children as IList<IGanttTask>, sortKeySelector, isAscending);
			}
		}
	}
}