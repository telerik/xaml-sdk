using System;
using System.Linq;
using Telerik.Windows.Controls.ScheduleView;
using Telerik.Windows.Controls.ScheduleView.ICalendar;

namespace ScheduleViewDB.Helpers
{
	public class RecurrenceHelper
	{
		public static bool IsOccurrenceInRange(string valueToParse, DateTime start, DateTime end)
		{
			RecurrencePattern pattern = new RecurrencePattern();
			if (RecurrencePatternHelper.TryParseRecurrencePattern(valueToParse, out pattern))
			{
				return pattern.GetOccurrences(start, start, end).Count() > 0;
			}

			return false;
		}
	}
}
