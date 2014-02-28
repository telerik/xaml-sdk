using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GanttView;
using Telerik.Windows.Controls.Scheduling;

namespace TimelineIntervalMarkers
{
    public class ViewModel : ViewModelBase
    {
        private ObservableCollection<GanttTask> tasks;
        private ITimeLineVisualizationBehavior customTimeLineBehavior;
        private DateRange visibleTime;

        public ViewModel()
        {
            var date = DateTime.Now;

            this.tasks = GetTasks(date);

            this.visibleTime = new DateRange(date.AddDays(-1), date.AddDays(11));

            this.customTimeLineBehavior = new CustomTimeLineBehavior(this.VisibleTime);
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

        public ITimeLineVisualizationBehavior CustomTimeLineBehavior
        {
            get
            {
                return customTimeLineBehavior;
            }

            set
            {
                customTimeLineBehavior = value;
                OnPropertyChanged(() => this.CustomTimeLineBehavior);
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

            ganttRendering.Dependencies.Add(new Dependency() { FromTask = ganttAPI });
            ganttDemos.Dependencies.Add(new Dependency() { FromTask = ganttRendering });

            var iterationTask = new GanttTask()
            {
                Start = date,
                End = date.AddDays(7),
                Title = "Iteration 1",
                Children = { ganttAPI, ganttRendering, ganttDemos, milestone },
            };

            ObservableCollection<GanttTask> tasks = new ObservableCollection<GanttTask>() { iterationTask };

            return tasks;
        }
    }
}
