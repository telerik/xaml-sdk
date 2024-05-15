using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GanttView;
using Telerik.Windows.Controls.Scheduling;

namespace ExportUIElement
{
    public class MainViewModel
    {
        public List<PlotInfo> Data1 { get; set; }
        public List<PlotInfo> Data2 { get; set; }
        public List<PlotInfo> Data3 { get; set; }
        public ObservableCollection<IGanttTask> Tasks { get; set; }
        public IDateRange VisibleRange { get; set; }

        public MainViewModel()
        {
            this.Data1 = new List<PlotInfo>
            {
                new PlotInfo { Category = "Jan", Value = 67, },
                new PlotInfo { Category = "Feb", Value = 75, },
                new PlotInfo { Category = "Mar", Value = 70, },
                new PlotInfo { Category = "Apr", Value = 37, },
                new PlotInfo { Category = "May", Value = 22, },
                new PlotInfo { Category = "Jun", Value = 10, },
            };

            this.Data2 = new List<PlotInfo>
            {
                new PlotInfo { Category = "May", Value = 22, },
                new PlotInfo { Category = "Jun", Value = 10, },
                new PlotInfo { Category = "Jul", Value = 47, },
                new PlotInfo { Category = "Aug", Value = 75, },
                new PlotInfo { Category = "Sep", Value = 70, },
                new PlotInfo { Category = "Oct", Value = 57, },
            };

            this.Data3 = new List<PlotInfo>
            {
                new PlotInfo { Category = "Feb", Value = 90, },
                new PlotInfo { Category = "Mar", Value = 88, },
                new PlotInfo { Category = "Apr", Value = 72, },
                new PlotInfo { Category = "May", Value = 77, },
                new PlotInfo { Category = "Jun", Value = 80, },
                new PlotInfo { Category = "Jul", Value = 67, },
                new PlotInfo { Category = "Aug", Value = 55, },
                new PlotInfo { Category = "Sep", Value = 35, },
            };

            this.SetUpGanttData();
        }

        private void SetUpGanttData()
        {
            this.VisibleRange = new DateRange(DateTime.Today, DateTime.Today.AddDays(15));
            this.Tasks = new ObservableCollection<IGanttTask>();

            var start = DateTime.Today;
            var end = start.AddDays(3);

            var testingTaskChild1 = new GanttTask(start, end, "Unit test planning");
            var testingTaskChild2 = new GanttTask(start.AddDays(1), end.AddDays(2), "Integration test planning");

            start = start.AddDays(3);
            end = start.AddDays(4);
            var testingTaskChild3 = new GanttTask(start, end, "Unit testing");

            start = end.Date.AddHours(1);
            end = start.AddDays(2);
            var testingTaskChild4 = new GanttTask(start, end, "Integration Testing");

            testingTaskChild2.Dependencies.Add(new Dependency { FromTask = testingTaskChild1 });
            testingTaskChild3.Dependencies.Add(new Dependency { FromTask = testingTaskChild2 });
            testingTaskChild4.Dependencies.Add(new Dependency { FromTask = testingTaskChild3 });

            start = DateTime.Today;
            end = start.AddDays(4);

            var testingTask = new GanttTask(start, end, "Testing")
            {
                Children = { testingTaskChild1, testingTaskChild2, testingTaskChild3, testingTaskChild4 }
            };

            this.Tasks.Add(testingTask);
        }
    }
}
