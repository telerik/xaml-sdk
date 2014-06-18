using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GanttView;
using Telerik.Windows.Controls.Scheduling;

namespace TaskDeadlineProperty
{
    public class ViewModel : ViewModelBase
    {
        private DateRange visibleTime;      

        public ViewModel()
        {
            var date = DateTime.Now;
            var ganttAPI = new GanttTask()
            {
                Start = date,
                End = date.AddDays(2),
                Title = "Design public API",
                Deadline = date.AddDays(1)
            };

            var ganttRendering = new GanttTask()
            {
                Start = date.AddDays(2).AddHours(8),
                End = date.AddDays(4),
                Title = "Gantt Rendering",               
                Deadline = date.AddDays(5)
            };
            var ganttDemos = new GanttTask()
            {
                Start = date.AddDays(4.5),
                End = date.AddDays(7),
                Title = "Gantt Demos",
                Deadline = date.AddDays(7)
            };
            var milestone = new GanttTask()
            {
                Start = date.AddDays(7),
                End = date.AddDays(7).AddHours(1),
                Title = "Review",           
                Deadline = date.AddDays(8),
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
            this.Tasks = new ObservableCollection<GanttTask>() { iterationTask };
         


            var custom_ganttAPI = new CustomGanttTask()
            {
                Start = date,
                End = date.AddDays(2),
                Title = "Design public API",
                Deadline = date.AddDays(1)
            };

            var custom_ganttRendering = new CustomGanttTask()
            {
                Start = date.AddDays(2).AddHours(8),
                End = date.AddDays(4),
                Title = "Gantt Rendering",
                Deadline = date.AddDays(5)
            };
            var custom_ganttDemos = new CustomGanttTask()
            {
                Start = date.AddDays(4.5),
                End = date.AddDays(7),
                Title = "Gantt Demos",
                Deadline = date.AddDays(7)
            };
            var custom_milestone = new CustomGanttTask()
            {
                Start = date.AddDays(7),
                End = date.AddDays(7).AddHours(1),
                Title = "Review",
                Deadline = date.AddDays(8),
                IsMilestone = true
            };
            custom_ganttRendering.Dependencies.Add(new Dependency() { FromTask = custom_ganttAPI });
            custom_ganttDemos.Dependencies.Add(new Dependency() { FromTask = custom_ganttRendering });
            var custom_iterationTask = new CustomGanttTask()
            {
                Start = date,
                End = date.AddDays(7),
                Title = "Iteration 1",
                Children = { custom_ganttAPI, custom_ganttRendering, custom_ganttDemos, custom_milestone }
            };
            this.CustomTasks = new ObservableCollection<CustomGanttTask>() { custom_iterationTask };

            this.visibleTime = new DateRange(date.AddDays(-1), date.AddDays(9));
        }


        public ObservableCollection<GanttTask> Tasks {  get;  set;  }
        public ObservableCollection<CustomGanttTask> CustomTasks { get; set; }

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
