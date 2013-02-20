using Telerik.Windows.Controls.Map;

namespace Search
{
    public class MapItem
    {
        public MapItem()
        {
        }

        public string Caption
        {
            get;
            set;
        }

        public Location Location
        {
            get;
            set;
        }

        public double BaseZoomLevel
        {
            get;
            set;
        }

        public ZoomRange ZoomRange
        {
            get;
            set;
        }
    }
}
