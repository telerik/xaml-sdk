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
using Telerik.Windows.Controls.Scheduling;
using Telerik.Windows.Core;

namespace ProjectDeadline
{
	public class TimeLineDeadlineEventInfo : TimeSlotInfo
	{
		public TimeLineDeadlineEventInfo(Range<long> timeRange)
			: base(timeRange)
		{
		}

		public override bool Equals(object obj)
		{
			return this.Equals(obj as TimeLineDeadlineEventInfo);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
