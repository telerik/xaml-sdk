using System;
using System.Collections.ObjectModel;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GanttView;
using Telerik.Windows.Controls.Scheduling;

namespace ZeroTaskLevelInitiallyCollapsed
{
    public class ViewModel : ViewModelBase
    {
        private ObservableCollection<GanttTask> tasks;

        private DateRange visibleTime;

        public ViewModel()
        {
            var date = DateTime.Now;
            this.tasks = GetTasks(date);
            this.visibleTime = new DateRange(date.AddDays(-1), date.AddDays(11));
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

        private ObservableCollection<GanttTask> GetTasks(DateTime date)
        {
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
                Description = "Description: Review",
                IsMilestone = true,
            };

            var ganttAPI2 = new GanttTask()
            {
                Start = date,
                End = date.AddDays(2),
                Title = "Design public API 2",
                Description = "Description: Design public API 2",
            };
            var ganttRendering2 = new GanttTask()
            {
                Start = date.AddDays(2).AddHours(8),
                End = date.AddDays(4),
                Title = "Gantt Rendering 2",
                Description = "Description: Gantt Rendering 2",
            };
            var ganttDemos2 = new GanttTask()
            {
                Start = date.AddDays(4.5),
                End = date.AddDays(7),
                Title = "Gantt Demos 2",
                Description = "Description: Gantt Demos 2",
            };
            var milestone2 = new GanttTask()
            {
                Start = date.AddDays(7),
                End = date.AddDays(7).AddHours(1),
                Title = "Review 2",
                Description = "Description: Review 2",
                IsMilestone = true,
            };

            ganttRendering.Dependencies.Add(new Dependency() { FromTask = ganttAPI });
            ganttDemos.Dependencies.Add(new Dependency() { FromTask = ganttRendering });

            ganttRendering2.Dependencies.Add(new Dependency() { FromTask = ganttAPI2 });
            ganttDemos2.Dependencies.Add(new Dependency() { FromTask = ganttRendering2 });

            var iterationTask = new GanttTask()
            {
                Start = date,
                End = date.AddDays(7),
                Title = "Iteration 1",
                Children = { ganttAPI, ganttRendering, ganttDemos, milestone },
            };

            var iterationTask2 = new GanttTask()
            {
                Start = date,
                End = date.AddDays(7),
                Title = "Iteration 2",
                Children = { ganttAPI2, ganttRendering2, ganttDemos2, milestone2 },
            };

            var simpleTask = new GanttTask()
            {
                Start = date,
                End = date.AddDays(2),
                Title = "Simple Task",
                Description = "Simple Task",
            };

            var bigIterationTask = new GanttTask()
            {
                Start = date,
                End = date.AddDays(7),
                Title = "Big Iteration",
                Children = { iterationTask, simpleTask, iterationTask2 },
            };

            ObservableCollection<GanttTask> result = new ObservableCollection<GanttTask>() { bigIterationTask };

            return result;
        }
    }
}
