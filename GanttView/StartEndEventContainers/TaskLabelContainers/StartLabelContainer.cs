using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Windows.Controls.GanttView;

namespace StartEndEventContainers
{
    public class StartLabelContainer : EventContainer
    {
        private StartLabelEventInfo eventInfo;

        public StartLabelContainer()
        {
            this.DefaultStyleKey = typeof(StartLabelContainer);
        }

        public override object DataItem
        {
            get
            {
                return this.eventInfo;
            }
            set
            {
                this.SetDataContext(value as StartLabelEventInfo);
            }
        }

        protected virtual void SetDataContext(StartLabelEventInfo newEventInfo)
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
