using System;
using System.Collections.ObjectModel;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GanttView;
using Telerik.Windows.Controls.Scheduling;

namespace ExpandCollapseViaHeader
{
    public class ViewModel : ViewModelBase
    {
        private ObservableCollection<GanttTask> tasks;

        private DateRange visibleRange;

        public ViewModel()
        {
            var date = DateTime.Now;

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
            var ganttDemosTest = new GanttTask()
            {
                Start = date.AddDays(4.5),
                End = date.AddDays(7),
                Title = "Gantt Demos Test",
                Description = "Description: Gantt Demos Test"
            };
            var ganttDemos = new GanttTask()
            {
                Start = date.AddDays(4.5),
                End = date.AddDays(7),
                Title = "Gantt Demos",
                Description = "Description: Gantt Demos",
                Children = { ganttDemosTest }
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

            var webSiteLaunch = new GanttTask()
            {
                Start = date.AddDays(8),
                End = date.AddDays(10),
                Title = "Website Launch",
                Description = "Description: Website Launch",
            };

            var blogPostPublish = new GanttTask()
            {
                Start = date.AddDays(10),
                End = date.AddDays(10.3),
                Title = "Blog Post Publish",
                Description = "Description: Blog Post Publish",
            };

            var releaseParty = new GanttTask()
            {
                Start = date.AddDays(11),
                End = date.AddDays(13),
                Title = "Release Party",
                Description = "Description: Release Party",
            };

            blogPostPublish.Dependencies.Add(new Dependency() { FromTask = webSiteLaunch });
            releaseParty.Dependencies.Add(new Dependency() { FromTask = blogPostPublish });

            var postProduction = new GanttTask()
            {
                Start = date.AddDays(8),
                End = date.AddDays(13),
                Title = "Post Production",
                Description = "Description: Post Production",
                Children = { webSiteLaunch, blogPostPublish, releaseParty }
            };

            this.tasks = new ObservableCollection<GanttTask>() { iterationTask, postProduction };
            this.visibleRange = new DateRange(date.AddDays(-1), date.AddDays(14));
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

        public DateRange VisibleRange
        {
            get
            {
                return this.visibleRange;
            }

            set
            {
                if (this.visibleRange != value)
                {
                    this.visibleRange = value;
                    this.OnPropertyChanged(() => this.VisibleRange);
                }
            }
        }
    }
}
