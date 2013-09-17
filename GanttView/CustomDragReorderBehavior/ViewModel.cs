using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GanttView;
using Telerik.Windows.Controls.Scheduling;

namespace CustomDragReorderBehavior
{
    public class ViewModel : ViewModelBase
    {
        private ObservableCollection<CustomGanttTask> tasks;

        private DateRange visibleTime;

        public ViewModel()
        {
            var date = DateTime.Today;
            this.tasks = GetTasks(date);
            this.visibleTime = new DateRange(date.AddDays(-1), date.AddDays(11));
        }

        public ObservableCollection<CustomGanttTask> Tasks
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

        private ObservableCollection<CustomGanttTask> GetTasks(DateTime date)
        {
            var ganttAPI = new CustomGanttTask()
            {
                Start = date,
                End = date.AddDays(2),
                Title = "Design public API",
                Description = "Description: Design public API",
                IsDragReorderAllowed = true
            };
            var ganttRendering = new CustomGanttTask()
            {
                Start = date.AddDays(3),
                End = date.AddDays(5),
                Title = "Gantt Rendering",
                Description = "Description: Gantt Rendering",
                IsDragReorderAllowed = false
            };
            var ganttDemos = new CustomGanttTask()
            {
                Start = date.AddDays(4),
                End = date.AddDays(8),
                Title = "Gantt Demos",
                Description = "Description: Gantt Demos",
                IsDragReorderAllowed = true
            };
            var milestone = new CustomGanttTask()
            {
                Start = date.AddDays(7),
                End = date.AddDays(7).AddHours(1),
                Title = "Review",
                Description = "Description: Review",
                IsMilestone = true,
                IsDragReorderAllowed = false
            };
            ganttRendering.Dependencies.Add(new Dependency() { FromTask = ganttAPI });
            ganttDemos.Dependencies.Add(new Dependency() { FromTask = ganttRendering });
            var iterationTask = new CustomGanttTask()
            {
                Start = date,
                End = date.AddDays(9),
                Title = "Iteration 1",
                Children = { ganttAPI, ganttRendering, ganttDemos, milestone },
                IsDragReorderAllowed = false
            };
            ObservableCollection<CustomGanttTask> tasks = new ObservableCollection<CustomGanttTask>() { iterationTask };

            return tasks;
        }
    }
}
