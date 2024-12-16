using System;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls.Calendar;

namespace WeekdaysButtonStyleSelector
{
    public class DayButtonStyleSelector : StyleSelector
	{
		public Style SpecialStyleWeekDays { get; set; }

		public Style SpecialStyleWeekEnds { get; set; }

		public override Style SelectStyle(object item, DependencyObject container)
		{
			// Add different calendar button styles
			var content = item as CalendarButtonContent;
			if (content != null)
			{
				bool isWeekday = content.Date.DayOfWeek == DayOfWeek.Monday || content.Date.DayOfWeek == DayOfWeek.Tuesday ||
					content.Date.DayOfWeek == DayOfWeek.Wednesday || content.Date.DayOfWeek == DayOfWeek.Thursday ||
					content.Date.DayOfWeek == DayOfWeek.Friday;
				if ((isWeekday && content.ButtonType == CalendarButtonType.Date) || content.Date == DateTime.Today)
				{
					return SpecialStyleWeekDays;
				}
				else
				{
					if (!isWeekday && content.ButtonType == CalendarButtonType.Date)
					{
						return SpecialStyleWeekEnds;
					}
				}
			}

			return base.SelectStyle(item, container);
		}
	}
}
