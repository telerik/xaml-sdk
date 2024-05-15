using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Scheduling;
using Telerik.Windows.Core;

namespace DragDropAndResizeCustomContainers
{
    public class TimeLineCustomEventInfo : SlotInfo, IDateRange
	{
		private readonly CustomGanttTask task;

		public TimeLineCustomEventInfo(Range<long> timeRange, int index, CustomGanttTask task)
            : base(timeRange, index, index)
        {
			this.task = task;
		}

		public string Title
		{
			get
			{
				return this.task.Title;
			}
			set
			{
				this.task.Title = value;
			}
		}

		public DateTime Start
		{
			get
			{
				return this.task.Start;
			}
			set
			{
				this.task.Start = value;
			}
		}

		public DateTime End
		{
			get
			{
				return this.task.End;
			}
			set
			{
				this.task.End = value;
			}
		}

		public Brush Color
		{
			get
			{
				return this.task.Color;
			}
			set
			{
				this.task.Color = value;
			}
		}

		public CustomGanttTask Task
		{
			get { return this.task; }
		}

		public override bool Equals(object obj)
        {
            return this.Equals(obj as TimeLineCustomEventInfo);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
