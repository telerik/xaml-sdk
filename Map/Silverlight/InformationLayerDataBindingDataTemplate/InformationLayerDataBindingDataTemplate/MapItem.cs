using Telerik.Windows.Controls.Map;

namespace InformationLayerDataBindingDataTemplate
{
    public class MapItem
    {
        public MapItem(string caption, Location location, double baseZoomLevel, ZoomRange zoomRange)
        {
            this.Caption = caption;
            this.Location = location;
            this.BaseZoomLevel = baseZoomLevel;
            this.ZoomRange = zoomRange;
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
