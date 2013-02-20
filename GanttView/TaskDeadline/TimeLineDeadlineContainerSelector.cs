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

namespace TaskDeadline
{
	public class TimeLineDeadlineContainerSelector : DefaultTimeLineContainerSelector
	{
		public override ContainerTypeIdentifier GetContainerType(object item)
		{
			if (item is TimeLineDeadlineEventInfo)
			{
				return ContainerTypeIdentifier.FromType<TimeLineDeadlineContainer>();
			}

			return base.GetContainerType(item);
		}
	}
}
