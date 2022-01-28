using System;
using System.Collections.ObjectModel;
using System.Windows.Media;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GanttView;
using Telerik.Windows.Controls.Scheduling;

namespace DragDropAndResizeCustomContainers
{
    public class MyViewModel : ViewModelBase
    {
        private ObservableCollection<CustomGanttTask> _Tasks;
        private DateRange _VisibleTime;
        private ITimeLineVisualizationBehavior _TimeLineDeadlineBehavior;

        public MyViewModel()
        {
            this._TimeLineDeadlineBehavior = new TimeLineCustomBehavior();
            var date = DateTime.Now;
            var ganttAPI = new CustomGanttTask()
            {
                Start = date,
                End = date.AddDays(2),
                Title = "Design public API",
                Description = "Description: Design public API",
                Color = Brushes.Gray
            };
            var ganttRendering = new CustomGanttTask()
            {
                Start = date.AddDays(2).AddHours(8),
                End = date.AddDays(4),
                Title = "Gantt Rendering",
                Description = "Description: Gantt Rendering",
                Color = Brushes.Gray
            };
            var ganttDemos = new CustomGanttTask()
            {
                Start = date.AddDays(4.5),
                End = date.AddDays(7),
                Title = "Gantt Demos",
                Description = "Description: Gantt Demos",
                Color = Brushes.Gray
            };
            var milestone = new CustomGanttTask()
            {
                Start = date.AddDays(7),
                End = date.AddDays(7).AddHours(1),
                Title = "Review",
                Description = "Review",
                IsMilestone = true
            };
            ganttRendering.Dependencies.Add(new Dependency() { FromTask = ganttAPI });
            ganttDemos.Dependencies.Add(new Dependency() { FromTask = ganttRendering });
            var iterationTask = new CustomGanttTask(date, date.AddDays(7), "Iteration 1")
            {
                Children = { ganttAPI, ganttRendering, ganttDemos, milestone }
            };
            this._Tasks = new ObservableCollection<CustomGanttTask>() { iterationTask };
            this._VisibleTime = new DateRange(date.AddDays(-1), date.AddDays(9));
           
        }
        public ITimeLineVisualizationBehavior TimeLineDeadlineBehavior
        {
            get { return this._TimeLineDeadlineBehavior; }
            set
            {
                if (this._TimeLineDeadlineBehavior != value)
                {
                    this._TimeLineDeadlineBehavior = value;
                    this.OnPropertyChanged(() => this.TimeLineDeadlineBehavior);
                }
            }
        }
        public ObservableCollection<CustomGanttTask> Tasks
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
