using System;
using System.Collections.ObjectModel;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GanttView;
using Telerik.Windows.Controls.Scheduling;

namespace SummaryTaskConsistency
{
    public class ViewModel : ViewModelBase
    {
        private ObservableCollection<MyGanttTask> tasks;

        private DateRange visibleTime;

        public ViewModel()
        {
            var date = DateTime.Now;
            this.tasks = GetTasks(date);
            this.visibleTime = new DateRange(date.AddDays(-3), date.AddDays(11));
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

        private ObservableCollection<MyGanttTask> GetTasks(DateTime date)
        {
            var iterationTask = new MyGanttTask()
            {
                Start = date,
                End = date.AddDays(7),
                Title = "Iteration 1",
            };

            var ganttAPI = new MyGanttTask()
            {
                Start = date,
                End = date.AddDays(2),
                Title = "Design public API",
                Description = "Description: Design public API",
                Parent = iterationTask
            };
            var ganttRendering = new MyGanttTask()
            {
                Start = date.AddDays(2).AddHours(8),
                End = date.AddDays(4),
                Title = "Gantt Rendering",
                Description = "Description: Gantt Rendering",
                Parent = iterationTask
            };
            var ganttDemos = new MyGanttTask()
            {
                Start = date.AddDays(4.5),
                End = date.AddDays(7),
                Title = "Gantt Demos",
                Description = "Description: Gantt Demos",
                Parent = iterationTask
            };
            var milestone = new MyGanttTask()
            {
                Start = date.AddDays(7),
                End = date.AddDays(7).AddHours(1),
                Title = "Review",
                Description = "Description: Review",
                Parent = iterationTask,
                IsMilestone = true,
            };

            iterationTask.Children.Add(ganttAPI);
            iterationTask.Children.Add(ganttRendering);
            iterationTask.Children.Add(ganttDemos);
            iterationTask.Children.Add(milestone);
            ganttRendering.Dependencies.Add(new Dependency() { FromTask = ganttAPI });
            ganttDemos.Dependencies.Add(new Dependency() { FromTask = ganttRendering });


            ObservableCollection<MyGanttTask> tasks = new ObservableCollection<MyGanttTask>() { iterationTask };

            return tasks;
        }
    }
}
