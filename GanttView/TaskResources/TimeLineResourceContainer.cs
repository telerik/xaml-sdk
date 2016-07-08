using System.Windows.Controls;
using Telerik.Windows.Rendering;

namespace TaskResources
{
    public class TimeLineResourceContainer: Control, IDataContainer
    {
        private TimeLineResourceEventInfo eventInfo;

        public TimeLineResourceContainer()
        {
            this.DefaultStyleKey = typeof(TimeLineResourceContainer);
        }

        public object DataItem 
        {
            get
            {
                return this.eventInfo;
            }
            set
            {
                var titleInfo = value as TimeLineResourceEventInfo;
                this.eventInfo = titleInfo;
                this.DataContext = this.eventInfo;
            }
        }
    }
}
