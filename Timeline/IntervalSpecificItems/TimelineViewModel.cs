using System;
using System.Collections.Generic;
using System.ComponentModel;
using Telerik.Windows.Controls.TimeBar;

namespace IntervalSpecificItems
{
    public class TimelineViewModel : INotifyPropertyChanged
    {
        private List<TimelineDataItem> timelineItemsSource;
        private IntervalBase currentInterval;

        public event PropertyChangedEventHandler PropertyChanged;

        public TimelineViewModel()
        {
            this.GenerateItems();
        }

        public IntervalBase CurrentInterval
        {
            get
            {
                return this.currentInterval;
            }
            set
            {
                this.currentInterval = value;
                this.UpdateTimelineItemsSource();
            }
        }

        public List<TimelineDataItem> TimelineItemsSource
        {
            get
            {
                return this.timelineItemsSource;
            }
            set
            {
                this.timelineItemsSource = value;
                this.OnPropertyChanged("TimelineItemsSource");
            }
        }

        public List<TimelineDataItem> DayItems { get; set; }

        public List<TimelineDataItem> MonthItems { get; set; }

        public List<TimelineDataItem> YearItems { get; set; }

        private void UpdateTimelineItemsSource()
        {
            Type intervalType = this.CurrentInterval.GetType();

            if (intervalType == typeof(DayInterval))
            {
                this.TimelineItemsSource = this.DayItems;
            }
            else if (intervalType == typeof(MonthInterval))
            {
                this.TimelineItemsSource = this.MonthItems;
            }
            else
            {
                this.TimelineItemsSource = this.YearItems;
            }
        }

        private void GenerateItems()
        {
            DateTime startDate = new DateTime(2000, 1, 1);
            DateTime endDate = new DateTime(2010, 1, 1);

            this.DayItems = new List<TimelineDataItem>();
            this.MonthItems = new List<TimelineDataItem>();
            this.YearItems = new List<TimelineDataItem>();

            for (DateTime date = startDate; date < endDate; date = date.AddDays(1))
            {
                this.DayItems.Add(new TimelineDataItem() { Date = date, Duration = TimeSpan.FromDays(1) });
            }

            for (DateTime date = startDate; date < endDate; date = date.AddMonths(1))
            {
                this.MonthItems.Add(new TimelineDataItem() { Date = date, Duration = date.AddMonths(1) - date });
            }

            for (DateTime date = startDate; date < endDate; date = date.AddYears(1))
            {
                this.YearItems.Add(new TimelineDataItem() { Date = date, Duration = date.AddYears(1) - date });
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}