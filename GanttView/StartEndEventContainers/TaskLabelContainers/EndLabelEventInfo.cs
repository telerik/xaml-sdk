using Telerik.Windows.Controls.Scheduling;
using Telerik.Windows.Core;

namespace StartEndEventContainers
{
    public class EndLabelEventInfo : SlotInfo
    {
        public EndLabelEventInfo(Range<long> timeRange, int index)
            : base(timeRange, index, index)
        {
        }
        
        public object OriginalEvent { get; set; }
        public override bool Equals(object obj)
        {
            return this.Equals(obj as EndLabelEventInfo);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
