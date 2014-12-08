using Telerik.Windows.Controls.Scheduling;
using Telerik.Windows.Core;

namespace StartEndEventContainers
{
    public class StartLabelEventInfo : SlotInfo
    {
        public StartLabelEventInfo(Range<long> timeRange, int index)
            : base(timeRange, index, index)
        {
        }
        
        public object OriginalEvent { get; set; }
        public override bool Equals(object obj)
        {
            return this.Equals(obj as StartLabelEventInfo);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
