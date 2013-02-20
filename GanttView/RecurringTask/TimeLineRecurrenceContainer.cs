using Telerik.Windows.Controls.GanttView;
using Telerik.Windows.Controls.Scheduling.Internal;
using Telerik.Windows.Rendering;

namespace RecurringTask
{
	public class TimeLineRecurrenceContainer : SelectorItemBase, IDataContainer
	{
		private readonly EventProxy eventProxy;
		private TimeLineRecurrenceEventInfo eventInfo;

		public TimeLineRecurrenceContainer()
		{
			this.DefaultStyleKey = typeof(TimeLineRecurrenceContainer);

			this.DataContext = this.eventProxy = new EventProxy();
		}

		public override object DataItem
		{
			get
			{
				return this.eventInfo;
			}
			set
			{
				this.SetDataContext(this.eventProxy, value as TimeLineRecurrenceEventInfo);
			}
		}

		private void SetDataContext(EventProxy proxy, TimeLineRecurrenceEventInfo eventInfo)
		{
			this.eventInfo = eventInfo;
			if (eventInfo != null)
			{
				this.eventProxy.SetDataItem(eventInfo.OriginalEvent);
			}
			else
			{
				this.eventProxy.SetDataItem(null);
			}
		}
	}
}
