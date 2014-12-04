using System;
using Telerik.Windows.Controls.ScheduleView;
using System.Globalization;

namespace CustomTimeRulerLinesTimelineView
{
	public class WeeklyTickProvider : ITickProvider
	{
		public string GetFormatString(IFormatProvider formatInfo, string formatString, DateTime currentStart)
		{
			var weekNumber = GetWeekNumber(formatInfo as CultureInfo, currentStart);

			return string.Format(formatInfo, "Week {0}", weekNumber);
		}

		private static int GetWeekNumber(CultureInfo formatInfo, DateTime currentStart)
		{
			var cultureInfo = formatInfo as CultureInfo;
			if (cultureInfo != null)
			{
				return cultureInfo.Calendar.GetWeekOfYear(currentStart, cultureInfo.DateTimeFormat.CalendarWeekRule, cultureInfo.DateTimeFormat.FirstDayOfWeek);
			}
			return 0;
		}

		public DateTime GetNextStart(TimeSpan pixelLength, DateTime currentStart)
		{
			var currentDate = currentStart.Date;

			var weekStart = CalendarHelper.GetFirstDayOfWeek(currentStart, DayOfWeek.Monday);
			if (weekStart == currentDate)
			{
				return weekStart.AddDays(7);
			}
			return weekStart;
		}
	}
}
