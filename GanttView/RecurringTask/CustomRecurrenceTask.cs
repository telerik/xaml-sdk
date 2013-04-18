using System;
using Telerik.Windows.Controls.GanttView;

namespace RecurringTask
{
	public class CustomRecurrenceTask : GanttTask
	{
		private RecurrenceRule recurrenceRule;

		public CustomRecurrenceTask(DateTime start, DateTime end, string title)
			: base(start, end, title)
		{
		}

		public RecurrenceRule RecurrenceRule
		{
			get { return recurrenceRule; }
			set
			{
				if (this.recurrenceRule != value)
				{
					recurrenceRule = value;
				}
			}
		}
	}
}
