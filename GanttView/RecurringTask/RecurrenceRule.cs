using System;

namespace RecurringTask
{
	public class RecurrenceRule
	{
		public RecurrenceRule(DateTime start, TimeSpan interval, int ocurrenceCount)
		{
			this.start = start;
			this.ocurrenceCount = ocurrenceCount;
			this.interval = interval;
		}

		private int ocurrenceCount;
		public int OcurrenceCount
		{
			get { return ocurrenceCount; }
			set { ocurrenceCount = value; }
		}

		private DateTime start;
		public DateTime Start
		{
			get { return start; }
			set { start = value; }
		}

		private TimeSpan interval;
		public TimeSpan Interval
		{
			get { return interval; }
			set { interval = value; }
		}
	}
}
