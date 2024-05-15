using System.Windows.Controls;
using Telerik.Windows.Rendering;

namespace TimelineIntervalMarkers
{
    public class IntervalContainer : Control, IDataContainer
    {
        public IntervalContainer()
        {
            this.DefaultStyleKey = typeof(IntervalContainer);
        }

        public object DataItem { get; set; }
    }
}
