using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskSnappingDragDropAndResizeBehavior
{
    public static class SnappingHelper
    {
        public static DateTime RoundUpDateTime(DateTime dateTime)
        {
            DateTime result;
            if (dateTime.Hour >= 12)
            {
                result = SnappingHelper.RoundUpToNextDay(dateTime);
            }
            else
            {
                result = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
            }

            return result;
        }

        private static DateTime RoundUpToNextDay(DateTime dateTime)
        {
            DateTime result;
            if (dateTime.Day == DateTime.DaysInMonth(dateTime.Year, dateTime.Month))
            {
                if (dateTime.Month < 12)
                {
                    result = new DateTime(dateTime.Year, dateTime.Month + 1, 1);
                }
                else
                {
                    result = new DateTime(dateTime.Year + 1, 1, 1);
                }
            }
            else
            {
                result = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day + 1);
            }
            return result;
        }
    }
}
