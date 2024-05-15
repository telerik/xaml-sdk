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
using Telerik.Windows.Controls.GanttView;
using Telerik.Windows.Rendering.Virtualization;

namespace ProjectDeadline
{
	public class TimeLineDeadlineContainerSelector : DefaultTimeLineContainerSelector
	{
		private static readonly ContainerTypeIdentifier timelineDeadlineEventInfoContainerType = ContainerTypeIdentifier.FromType<TimeLineDeadlineContainer>();

		public override ContainerTypeIdentifier GetContainerType(object item)
		{
			if (item is TimeLineDeadlineEventInfo)
			{
				return timelineDeadlineEventInfoContainerType;
			}

			return base.GetContainerType(item);
		}
	}
}
