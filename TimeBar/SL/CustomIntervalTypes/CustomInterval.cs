using System;
using Telerik.Windows.Controls.TimeBar;

namespace CustomIntervalTypes
{
    public class CustomInterval : IntervalBase
    {
        private static readonly Func<DateTime, string>[] formatters;

        static CustomInterval()
        {
            formatters = new Func<DateTime, string>[]
            {
                date => string.Format("My custom interval {0}, {1}", GetNumberOfInterval(date), date.ToString("yyyy")),
                date => string.Format("Custom interval {0}", GetNumberOfInterval(date)),
                date => string.Format("C{0} {1}", GetNumberOfInterval(date), date.ToString("yyyy")),
            };
        }

        public override DateTime ExtractIntervalStart(DateTime date)
        {
            int firstMonthOfInterval = GetFirstMonthOfInterval(date);
            return new DateTime(date.Year, firstMonthOfInterval, 1);
        }

        public override DateTime IncrementByInterval(DateTime date, int intervalSpan)
        {
            return date.AddMonths(intervalSpan * 6);
        }

        public override Func<DateTime, string>[] Formatters
        {
            get
            {
                return formatters;
            }
        }

        public override TimeSpan MinimumPeriodLength
        {
            get
            {
                return TimeSpan.FromDays(180);
            }
        }

        private static int GetNumberOfInterval(DateTime date)
        {
            int number = ((date.Month - 1) / 6) + 1;
            return number;
        }

        private int GetFirstMonthOfInterval(DateTime date)
        {
            int quarter = GetNumberOfInterval(date);
            int firstMonthOfInterval = ((quarter - 1) * 6) + 1;
            return firstMonthOfInterval;
        }
    }
}
