using System.Windows.Controls;
using Telerik.Windows.Rendering;

namespace TimelineIntervalMarkers
{
    public class CurrentHourIndicatorContainer : Control, IDataContainer
    {
        public CurrentHourIndicatorContainer()
        {
            this.DefaultStyleKey = typeof(CurrentHourIndicatorContainer);
        }

        public object DataItem { get; set; }
    }
}
