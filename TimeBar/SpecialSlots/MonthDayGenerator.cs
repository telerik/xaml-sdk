using System;
using System.Collections.Generic;
using System.ComponentModel;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.TimeBar;

namespace TimelineTimeBarSpecialSlots
{
    class MonthDayGenerator : ITimeRangeGenerator, INotifyPropertyChanged
    {
        private int dayOfTheMonth = 13;

        /// <summary>
        /// Gets or sets the the day of the month and notifies for changes.
        /// </summary>
        public int DayOfTheMonth
        {
            get 
            { 
                return dayOfTheMonth; 
            }
            set 
            {
                if (dayOfTheMonth != value)
                {
                    dayOfTheMonth = value;
                    this.OnPropertyChanged("DayOfTheMonth");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public IEnumerable<IPeriodSpan> GetRanges(SelectionRange<DateTime> visibleRange)
        {
            DateTime day = new DateTime(visibleRange.Start.Year, visibleRange.Start.Month, dayOfTheMonth);

            if (visibleRange.End.Day >= dayOfTheMonth && !(visibleRange.Start.Day <= dayOfTheMonth))
            {
                day = day.AddMonths(1);
            }

            for (DateTime current = day; current < visibleRange.End; current = current.AddMonths(1))
            {
                yield return new PeriodSpan(current, TimeSpan.FromDays(1));
            }
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
