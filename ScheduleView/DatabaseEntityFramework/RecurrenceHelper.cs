using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Controls.ScheduleView;
using Telerik.Windows.Controls.ScheduleView.ICalendar;

namespace DatabaseEntityFramework
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
