using System;
using System.Collections.ObjectModel;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GanttView;
using Telerik.Windows.Controls.Scheduling;

namespace DirtyTasks
{
    public class ViewModel : ViewModelBase
    {
        private ObservableCollection<MyGanttTask> tasks;

        private DateRange visibleTime;

        public ViewModel()
        {
            var iterationStart = DateTime.Now;
            var iterationEnd = iterationStart.AddDays(7);
            var ganttAPI = new MyGanttTask()
            {
                Start = iterationStart,
                End = iterationStart.AddDays(2),
                Title = "Design public API",
                Description = "Description: Design public API",
            };
            var ganttRendering = new MyGanttTask()
            {
                Start = iterationStart.AddDays(2).AddHours(8),
                End = iterationStart.AddDays(4),
                Title = "Gantt Rendering",
                Description = "Description: Gantt Rendering",
            };
            var ganttDemos = new MyGanttTask()
            {
                Start = iterationStart.AddDays(4.5),
                End = iterationStart.AddDays(7),
                Title = "Gantt Demos",
                Description = "Description: Gantt Demos",
            };
            var milestone = new MyGanttTask()
            {
                Start = iterationStart.AddDays(7),
                End = iterationStart.AddDays(7).AddHours(1),
                Title = "Review",
                Description = "Description: Review",
                IsMilestone = true,
            };

            ganttRendering.Dependencies.Add(new Dependency() { FromTask = ganttAPI });
            ganttDemos.Dependencies.Add(new Dependency() { FromTask = ganttRendering });

            var iterationTask = new MyGanttTask()
            {
                Start = iterationStart,
                End = iterationEnd,
                Title = "Iteration 1",
                Children = { ganttAPI, ganttRendering, ganttDemos, milestone },
            };

            ganttAPI.IsModified = false;
            ganttRendering.IsModified = false;
            ganttDemos.IsModified = false;
            milestone.IsModified = false;
            iterationTask.IsModified = false;

            this.tasks = new ObservableCollection<MyGanttTask>() { iterationTask };
            this.visibleTime = new DateRange(iterationStart.AddDays(-1), iterationStart.AddDays(11));
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
