using System;
using System.Windows;
using Telerik.Windows.Controls;

namespace CustomTimeRulerLinesTimelineView
{
	public class CustomTimeRulerItemStyleSelector : OrientedTimeRulerItemStyleSelector
	{
		public Style CustomTimeRulerLineStyle { get; set; }
		public Style CustomMajorHorizontalTimeRulerItemStyle { get; set; }

		public override Style SelectStyle(object item, DependencyObject container, ViewDefinitionBase activeViewDefinition)
		{		
			TickData tickData = item as TickData;
			TimeRulerItem timeRulerItem = container as TimeRulerItem;
			if (tickData != null && timeRulerItem != null)
			{
				if (tickData.Type == TickType.Major)
				{
					if (tickData.Offset == TimeSpan.FromTicks(0))
					{
						return this.CustomMajorHorizontalTimeRulerItemStyle;
					}
				}
			}

			TimeRulerLine timeRulerLine = container as TimeRulerLine;
			if (timeRulerLine != null)
			{				
				if (tickData.Offset == TimeSpan.FromTicks(0))
				{
					return this.CustomTimeRulerLineStyle;
				}
			}
			return base.SelectStyle(item, container, activeViewDefinition);
		}
	}
}
