using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GanttView;
using Telerik.Windows.Controls.Scheduling;

namespace HolidayEvents
{
    public class ViewModel : ViewModelBase
    {
        private ITimeLineVisualizationBehavior timeLineDeadlineBehavior;
        private DateRange visibleRange;
        private List<DateRange> globalDeadlines;
        private ObservableCollection<CustomGanttTask> tasks;
        private double sliderValue;
        private TimeSpan pixelLenght;
        private long VisibleRangeTicks
        {
            get
            {
                return this.visibleRange.End.Subtract(this.visibleRange.Start).Ticks;
            }
        }

        public ViewModel()
        {
            var startDate = new DateTime(2013, 1, 1);
            var endDate = new DateTime(2013, 12, 31);

            var bulgaria = new CustomGanttTask()
            {
                Start = startDate,
                End = startDate.AddDays(1.5),
                Title = "Bulgaria",
                Description = "Bulgaria's national holidays",
                CustomDeadLines = new List<DateRange>() 
                { 
                    new DateRange(new DateTime(2013, 3, 3), new DateTime(2013, 3, 4)),
                    new DateRange(new DateTime(2013, 5, 1), new DateTime(2013, 5, 1)),
                    new DateRange(new DateTime(2013, 5, 6), new DateTime(2013, 5, 7)),
                    new DateRange(new DateTime(2013, 5, 24), new DateTime(2013, 5, 25)),
                    new DateRange(new DateTime(2013, 9, 6), new DateTime(2013, 9, 7)),
                    new DateRange(new DateTime(2013, 9, 22), new DateTime(2013, 9, 23)),
                    new DateRange(new DateTime(2013, 11, 1), new DateTime(2013, 11, 2)),
                    new DateRange(new DateTime(2013, 12, 24), new DateTime(2013, 12, 25)),
                    new DateRange(new DateTime(2013, 12, 26), new DateTime(2013, 12, 27)),
                    new DateRange(new DateTime(2013, 1, 2), new DateTime(2013, 1, 3))
                }
            };
            var england = new CustomGanttTask()
            {
                Start = startDate,
                End = startDate.AddDays(1.5),
                Title = "England",
                Description = "England's national holidays",
                CustomDeadLines = new List<DateRange>() 
                { 
                    new DateRange(new DateTime(2013, 3, 17), new DateTime(2013, 3, 18)),
                    new DateRange(new DateTime(2013, 5, 6), new DateTime(2013, 5, 7)),
                    new DateRange(new DateTime(2013, 5, 27), new DateTime(2013, 5, 28)),
                    new DateRange(new DateTime(2013, 7, 12), new DateTime(2013, 7, 13)),
                    new DateRange(new DateTime(2013, 8, 26), new DateTime(2013, 8, 27)),
                    new DateRange(new DateTime(2013, 12, 26), new DateTime(2013, 12, 28)),
                }
            };
            var usa = new CustomGanttTask()
            {
                Start = startDate,
                End = startDate.AddDays(1.5),
                Title = "USA",
                Description = "USA's national holidays",
                CustomDeadLines = new List<DateRange>() 
                { 
                    new DateRange(new DateTime(2013, 1, 21), new DateTime(2013, 1, 22)),
                    new DateRange(new DateTime(2013, 2, 18), new DateTime(2013, 2, 19)),
                    new DateRange(new DateTime(2013, 5, 27), new DateTime(2013, 5, 28)),
                    new DateRange(new DateTime(2013, 7, 4), new DateTime(2013, 7, 5)),
                    new DateRange(new DateTime(2013, 9, 2), new DateTime(2013, 9, 3)),
                    new DateRange(new DateTime(2013, 10, 14), new DateTime(2013, 10, 15)),
                    new DateRange(new DateTime(2013, 11, 11), new DateTime(2013, 11, 11)),
                    new DateRange(new DateTime(2013, 11, 29), new DateTime(2013, 11, 28)),
                }
            };


            var europe = new CustomGanttTask()
            {
                Start = startDate,
                End = endDate,
                Title = "Europe",
                Children = { bulgaria, england }
            };

            var unitedStates = new CustomGanttTask()
            {
                Start = startDate,
                End = startDate.AddDays(1.5),
                Title = "United States",
                Children = { usa }
            };
            this.tasks = new ObservableCollection<CustomGanttTask>() { europe, unitedStates };
            this.visibleRange = new DateRange(startDate, endDate);
            this.timeLineDeadlineBehavior = new EventsTimeLineVisualizationBehavior();

            this.GlobalDeadLines = new List<DateRange>() 
            { 
                new DateRange(new DateTime(2013, 1, 1), new DateTime(2013, 1, 2)),
                new DateRange(new DateTime(2013, 12, 25), new DateTime(2013, 12, 26)),
            };
            this.sliderValue = 1.0d;
            this.pixelLenght = TimeSpan.FromMinutes(36);
            this.VisibleArea = 1050;
        }

        public List<DateRange> GlobalDeadLines
        {
            get { return this.globalDeadlines; }
            set
            {
                if (this.globalDeadlines != value)
                {
                    this.globalDeadlines = value;
                    var behavior = this.timeLineDeadlineBehavior as EventsTimeLineVisualizationBehavior;
                    if (behavior != null)
                    {
                        behavior.GlobalDeadLines = value;
                    }
                    this.OnPropertyChanged(() => this.GlobalDeadLines);
                }
            }
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

        public DateRange VisibleRange
        {
            get { return this.visibleRange; }
            set
            {
                if (this.visibleRange != value)
                {
                    this.visibleRange = value;
                    this.OnPropertyChanged(() => this.VisibleRange);
                }
            }
        }

        public ITimeLineVisualizationBehavior TimeLineDeadlineBehavior
        {
            get
            {
                return timeLineDeadlineBehavior;
            }
            set
            {
                timeLineDeadlineBehavior = value;
                OnPropertyChanged(() => this.TimeLineDeadlineBehavior);
            }
        }

        public long VisibleArea { get; set; }

        

        public TimeSpan PixelLenght
        {
            get
            {
                sliderValue = sliderValue < 0.01d ? 0.01d : sliderValue;
                var currentTicks = (double)this.pixelLenght.Ticks / sliderValue;
                var maxTicks = this.VisibleArea == 0 ? (long)currentTicks : this.VisibleRangeTicks / this.VisibleArea;
                var ticksToUse = Math.Min((long)currentTicks, maxTicks);

                return TimeSpan.FromTicks(ticksToUse);
            }
            set
            {
                if (this.pixelLenght != value)
                {
                    this.pixelLenght = value;
                    OnPropertyChanged(() => this.PixelLenght);
                }
            }
        }

        public double SliderValue
        {
            get
            {
                return this.sliderValue;
            }

            set
            {
                if (this.sliderValue != value)
                {
                    this.sliderValue = value;
                    this.OnPropertyChanged(() => this.SliderValue);
                    OnPropertyChanged(() => this.PixelLenght);
                }
            }
        }
    }
}
