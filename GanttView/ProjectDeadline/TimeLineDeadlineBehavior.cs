using System;
using System.Collections;
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
using Telerik.Windows.Controls.Scheduling;
using Telerik.Windows.Core;

namespace ProjectDeadline
{
	public class TimeLineDeadlineBehavior : DefaultGanttTimeLineVisualizationBehavior
	{
		private DateTime projectDeadline;

		public DateTime ProjectDeadline
		{
			get { return this.projectDeadline; }
			set
			{
				if (this.projectDeadline != value)
				{
					this.projectDeadline = value;
					this.OnPropertyChanged(string.Empty);
				}
			}
		}

		protected override IEnumerable GetBackgroundData(TimeLineVisualizationState state)
		{
			foreach (var background in base.GetBackgroundData(state))
			{
				yield return background;
			}

			var visibleRange = state.VisibleTimeRange;
			var deadline = state.Rounder.Round(new DateRange(this.projectDeadline, this.projectDeadline));
			var deadlineRange = new Range<long>(deadline.Start.Ticks, deadline.End.Ticks);

			if (visibleRange.IntersectsWith(deadlineRange))
			{
				yield return new TimeLineDeadlineEventInfo(deadlineRange);
			}
		}
	}
}
