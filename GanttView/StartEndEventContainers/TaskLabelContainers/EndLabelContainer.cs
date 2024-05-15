using Telerik.Windows.Controls.GanttView;

namespace StartEndEventContainers
{
    public class EndLabelContainer : EventContainer
    {
        private EndLabelEventInfo eventInfo;

        public EndLabelContainer()
        {
            this.DefaultStyleKey = typeof(EndLabelContainer);
        }

        public override object DataItem
        {
            get
            {
                return this.eventInfo;
            }
            set
            {
                this.SetDataContext(value as EndLabelEventInfo);
            }
        }

        protected virtual void SetDataContext(EndLabelEventInfo newEventInfo)
        {
            this.eventInfo = newEventInfo;
            var proxy = this.DataContext as EventProxy;
            if (newEventInfo != null)
            {
                proxy.SetDataItem(newEventInfo.OriginalEvent);
            }
            else
            {
                proxy.SetDataItem(null);
            }
        }
    }
}
