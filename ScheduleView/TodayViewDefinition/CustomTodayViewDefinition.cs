using System;
using Telerik.Windows.Controls;

namespace TodayViewDefinition
{
    public class CustomTodayViewDefinition : DayViewDefinition
    {
        protected override DateTime GetVisibleRangeStart(DateTime currentDate, System.Globalization.CultureInfo culture, DayOfWeek? firstDayOfWeek)
        {
            return DateTime.Today;
        }

        protected override string FormatVisibleRangeText(IFormatProvider formatInfo, DateTime rangeStart, DateTime rangeEnd, DateTime currentDate)
        {
            return base.FormatVisibleRangeText(formatInfo, DateTime.Today, DateTime.Today, DateTime.Today);
        }
    }
}
