using System.Windows.Threading;
using Telerik.Windows.Diagrams.Core;

namespace Autoscrolling
{
    public class DiagramScrollingInfo
    {
        public DispatcherTimer ScrollingTimer { get; set; }
        public ConnectionTool ConnectionTool { get; set; }
        public ConnectionManipulationTool ConnectionManipulationTool { get; set; }
        public ScrollingDirection AutoScrollingDirection { get; set; }
        public DraggingService DraggingService { get; set; }
    }
}
