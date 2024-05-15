using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.Windows.Controls.ScheduleView;

namespace MonthTickProvider
{
	public class CustomMonthTickProvider : ITickProvider
	{
		public string GetFormatString(IFormatProvider formatInfo, string formatString, DateTime currentStart)
		{
			return string.Format(formatInfo, "{0:MMMM}", currentStart);
		}

		public DateTime GetNextStart(TimeSpan pixelLength, DateTime currentStart)
		{
			var currentDate = currentStart.Date;

			var monthStart = CalendarHelper.GetStartOfMonth(currentStart.Year, currentStart.Month);
			if (monthStart == currentDate)
			{
				return monthStart.AddMonths(1);
			}
			return monthStart;
		}
	}
}
