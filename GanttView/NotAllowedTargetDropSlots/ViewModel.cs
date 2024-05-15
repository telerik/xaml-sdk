using System;
using System.Collections.ObjectModel;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GanttView;
using Telerik.Windows.Controls.Scheduling;

namespace NotAllowedTargetDropSlots
{
    public class ViewModel : ViewModelBase
    {
        private ObservableCollection<GanttTask> tasks;
        private DateRange visibleRange;

        public ViewModel()
        {
            var date = Constants.ExampleStartDate;
            var ganttAPI = new GanttTask()
            {
                Start = date,
                End = date.AddDays(2),
                Title = "Design public API",
                Description = "Description: Design public API"
            };
            var ganttRendering = new GanttTask()
            {
                Start = date.AddDays(2).AddHours(8),
                End = date.AddDays(4),
                Title = "Gantt Rendering",
                Description = "Description: Gantt Rendering"
            };
            var ganttDemos = new GanttTask()
            {
                Start = date.AddDays(4.5),
                End = date.AddDays(7),
                Title = "Gantt Demos",
                Description = "Description: Gantt Demos"
            };
            var milestone = new GanttTask()
            {
                Start = date.AddDays(7),
                End = date.AddDays(7).AddHours(1),
                Title = "Review",
                Description = "Description: Review",
                IsMilestone = true
            };
            ganttRendering.Dependencies.Add(new Dependency() { FromTask = ganttAPI });
            ganttDemos.Dependencies.Add(new Dependency() { FromTask = ganttRendering });
            var iterationTask = new GanttTask()
            {
                Start = date,
                End = date.AddDays(7),
                Title = "Iteration 1",
                Children = { ganttAPI, ganttRendering, ganttDemos, milestone }
            };

            this.tasks = new ObservableCollection<GanttTask>() { iterationTask };
            this.visibleRange = new DateRange(date.AddDays(-1), date.AddDays(11));
        }

        /// <summary>
        /// Gets LockedRanges and notifies for changes
        /// </summary>
        public ObservableCollection<DateRange> LockedRanges
        {
            get
            {
                return Constants.LockedRanges;
            }

        }

        public ObservableCollection<GanttTask> GanttTasks
        {
            get
            {
                return this.tasks;
            }
            set
            {
                this.tasks = value;
                this.OnPropertyChanged(() => this.GanttTasks);
            }
        }

        public DateRange VisibleRange
        {
            get
            {
                return this.visibleRange;
            }
            set
            {
                this.visibleRange = value;
                this.OnPropertyChanged(() => this.VisibleRange);
            }
        }

    }
}
