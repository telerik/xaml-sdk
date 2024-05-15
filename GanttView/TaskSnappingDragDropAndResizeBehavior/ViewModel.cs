using System;
using System.Collections.ObjectModel;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GanttView;
using Telerik.Windows.Controls.Scheduling;

namespace TaskSnappingDragDropAndResizeBehavior
{
    public class ViewModel : ViewModelBase
    {
        private ObservableCollection<GanttTask> tasks;

        private DateRange visibleTime;

        public ViewModel()
        {
            var date = DateTime.Today;
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
                Start = date.AddDays(3),
                End = date.AddDays(5),
                Title = "Gantt Rendering",
                Description = "Description: Gantt Rendering",
            };
            var ganttDemos = new GanttTask()
            {
                Start = date.AddDays(4),
                End = date.AddDays(8),
                Title = "Gantt Demos",
                Description = "Description: Gantt Demos",
            };
            var milestone = new GanttTask()
            {
                Start = date.AddDays(8),
                End = date.AddDays(8),
                Title = "Review",
                Description = "Description: Review",
                IsMilestone = true,
            };
            ganttRendering.Dependencies.Add(new Dependency() { FromTask = ganttAPI });
            ganttDemos.Dependencies.Add(new Dependency() { FromTask = ganttRendering });
            var iterationTask = new GanttTask()
            {
                Start = date,
                End = date.AddDays(8),
                Title = "Iteration 1",
                Children = { ganttAPI, ganttRendering, ganttDemos, milestone }
            };
            ObservableCollection<GanttTask> tasks = new ObservableCollection<GanttTask>() { iterationTask };

            return tasks;
        }
    }
}
