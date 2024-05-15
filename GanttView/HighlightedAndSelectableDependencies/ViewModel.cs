using System;
using System.Collections.ObjectModel;
using System.Windows.Media;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GanttView;
using Telerik.Windows.Controls.Scheduling;

namespace HighlightedAndSelectableDependencies
{
    public class ViewModel : ViewModelBase
    {
        private ObservableCollection<GanttTask> _Tasks;
        private DateRange _VisibleTime;

        public ViewModel()
        {
            var date = DateTime.Now;
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
            
            ganttRendering.Dependencies.Add(new CustomDependency() { Color = Brushes.Blue, FromTask = ganttAPI });
            ganttDemos.Dependencies.Add(new CustomDependency() { Color = Brushes.Green, FromTask = ganttRendering });

            var iterationTask = new GanttTask()
            {
                Start = date,
                End = date.AddDays(7),
                Title = "Iteration 1",
                Children = { ganttAPI, ganttRendering, ganttDemos, milestone },
            };

            ObservableCollection<GanttTask> tasks = new ObservableCollection<GanttTask>() { iterationTask };
            for (var i = 0; i < 20; i++)
            {
                var task = new GanttTask()
                {
                    Start = date,
                    End = date.AddDays(3),
                    Title = string.Format("Task {0}", i),
                    Description = "Description: Review",
                };

                var task2 = new GanttTask()
                {
                    Start = date.AddDays(4),
                    End = date.AddDays(7),
                    Title = string.Format("Dependent Task {0}", i),
                    Description = "Description: Review",
                };

                if (i % 2 == 0)
                {
                    task2.Dependencies.Add(new CustomDependency() { Color = Brushes.Red, FromTask = task });
                }
                else
                {
                    task2.Dependencies.Add(new CustomDependency() { Color = Brushes.Yellow, FromTask = task });
                }

                tasks.Add(task);
                tasks.Add(task2);
            }

            this._Tasks = tasks;
            this._VisibleTime = new DateRange(date.AddDays(-1), date.AddDays(9));
        }

        public ObservableCollection<GanttTask> Tasks
        {
            get { return this._Tasks; }
            set
            {
                if (this._Tasks != value)
                {
                    this._Tasks = value;
                    this.OnPropertyChanged(() => this.Tasks);
                }
            }
        }

        public DateRange VisibleTime
        {
            get { return this._VisibleTime; }
            set
            {
                if (this._VisibleTime != value)
                {
                    this._VisibleTime = value;
                    this.OnPropertyChanged(() => this.VisibleTime);
                }
            }
        }
    }
}
