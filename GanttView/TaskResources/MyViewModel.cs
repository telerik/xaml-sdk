using System;
using System.Collections.ObjectModel;
using System.Windows.Media;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GanttView;
using Telerik.Windows.Controls.Scheduling;

namespace TaskResources
{
    public class MyViewModel : ViewModelBase
    {
        private ObservableCollection<GanttResourceTask> _Tasks;
        private DateRange _VisibleTime;

        private ITimeLineVisualizationBehavior _TimeLineResourceBehavior;

        public MyViewModel()
        {
            this._TimeLineResourceBehavior = new ResourceTimeLineVisualizationBehavior();

            var resources = new ObservableCollection<GanttResource>()
            {
                new GanttResource("Thomas Hardy", Colors.Red),
                new GanttResource("Elizabeth Lincoln", Colors.Blue),
                new GanttResource("Christina Berglung", Colors.Orange)            
            };

            var date = DateTime.Now;
            var ganttAPI = new GanttResourceTask()
            {
                Start = date,
                End = date.AddDays(2),
                Title = "Design public API",
                Description = "Description: Design public API",
                GanttResource = resources[0]
            };
            var ganttRendering = new GanttResourceTask()
            {
                Start = date.AddDays(2).AddHours(8),
                End = date.AddDays(4),
                Title = "Gantt Rendering",
                Description = "Description: Gantt Rendering",
                GanttResource = resources[1]
            };
            var ganttDemos = new GanttResourceTask()
            {
                Start = date.AddDays(4.5),
                End = date.AddDays(7),
                Title = "Gantt Demos",
                Description = "Description: Gantt Demos",
                GanttResource = resources[2]
            };
            var milestone = new GanttResourceTask()
            {
                Start = date.AddDays(7),
                End = date.AddDays(7).AddHours(1),
                Title = "Review",
                Description = "Review",
                GanttResource = resources[2],
                IsMilestone = true
            };
            ganttRendering.Dependencies.Add(new Dependency() { FromTask = ganttAPI });
            ganttDemos.Dependencies.Add(new Dependency() { FromTask = ganttRendering });
            var iterationTask = new GanttResourceTask()
            {
                Start = date,
                End = date.AddDays(7),
                Title = "Iteration 1",
                Children = { ganttAPI, ganttRendering, ganttDemos, milestone }
            };
            this._Tasks = new ObservableCollection<GanttResourceTask>() { iterationTask };

            this._VisibleTime = new DateRange(date.AddDays(-1), date.AddDays(12));
        }

        public ObservableCollection<GanttResourceTask> Tasks
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

        public ITimeLineVisualizationBehavior TimeLineResourceBehavior
        {
            get { return this._TimeLineResourceBehavior; }
            set
            {
                if (this._TimeLineResourceBehavior != value)
                {
                    this._TimeLineResourceBehavior = value;
                    this.OnPropertyChanged(() => this.TimeLineResourceBehavior);
                }
            }
        }
    }
}
