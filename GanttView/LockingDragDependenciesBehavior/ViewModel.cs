using System;
using System.Collections.ObjectModel;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GanttView;
using Telerik.Windows.Controls.Scheduling;

namespace LockingDragDependenciesBehavior
{
    public class ViewModel : ViewModelBase
    {
        private ObservableCollection<LockingTask> tasks;

        private DateRange visibleTime;

        public ViewModel()
        {
            var date = DateTime.Now;
            var ganttAPI = new LockingTask()
            {
                Start = date,
                End = date.AddDays(2),
                Title = "Design public API",
                Description = "Description: Design public API",
                AreDependenciesLocked = true
            };
            var ganttRendering = new LockingTask()
            {
                Start = date.AddDays(2).AddHours(8),
                End = date.AddDays(4),
                Title = "Gantt Rendering",
                Description = "Description: Gantt Rendering",
                AreDependenciesLocked = false
            };
            var ganttDemos = new LockingTask()
            {
                Start = date.AddDays(4.5),
                End = date.AddDays(7),
                Title = "Gantt Demos",
                Description = "Description: Gantt Demos",
                AreDependenciesLocked = false
            };
            var milestone = new LockingTask()
            {
                Start = date.AddDays(7),
                End = date.AddDays(7).AddHours(1),
                Title = "Review",
                Description = "Description: Review",
                IsMilestone = true,
                AreDependenciesLocked = true
            };

            ganttRendering.Dependencies.Add(new Dependency() { FromTask = ganttAPI });
            ganttDemos.Dependencies.Add(new Dependency() { FromTask = ganttRendering });

            var iterationTask = new LockingTask()
            {
                Start = date,
                End = date.AddDays(7),
                Title = "Iteration 1",
                Children = { ganttAPI, ganttRendering, ganttDemos, milestone },
                AreDependenciesLocked = true
            };

            this.tasks = new ObservableCollection<LockingTask>() { iterationTask };
            this.visibleTime = new DateRange(date.AddDays(-1), date.AddDays(11));
        }

        public ObservableCollection<LockingTask> Tasks
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
    }
}
