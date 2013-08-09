using System;
using System.Collections.Generic;
using System.ComponentModel;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.TimeBar;

namespace TimelineTimeBarSpecialSlots
{
    class WeekDaysGenerator : ITimeRangeGenerator
    {
        private int workingDaysCount = 5;
        private DayOfWeek firstDay = DayOfWeek.Monday;

        /// <summary>
        /// Gets or sets FirstWorkingDay and notifies for changes.
        /// </summary>
        public DayOfWeek FirstDay
        {
            get
            {
                return this.firstDay;
            }
            set
            {
                if (this.firstDay != value)
                {
                    this.firstDay = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets WorkingDaysCount and notifies for changes.
        /// </summary>
        public int DaysCount
        {
            get
            {
                return this.workingDaysCount;
            }
            set
            {
                if (this.workingDaysCount != value)
                {
                    this.workingDaysCount = value;
                }
            }
        }

        public IEnumerable<IPeriodSpan> GetRanges(SelectionRange<DateTime> visibleRange)
        {
            TimeSpan slotSpan = TimeSpan.FromDays(workingDaysCount);
            var differenceFirstVisible = firstDay - visibleRange.Start.DayOfWeek;
            DateTime day = new DateTime(visibleRange.Start.Year, visibleRange.Start.Month, visibleRange.Start.Day);

            for (DateTime current = day.AddDays(differenceFirstVisible); current < visibleRange.End; current += TimeSpan.FromDays(7))
            {
                yield return new PeriodSpan(current, slotSpan);
            }
        }
    }
}
