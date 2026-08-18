using Telerik.Windows.Controls.Map;

namespace CaliburnMicroIntegration.Models
{
    public class LocationInfo
    {
        public LocationInfo(string locationName, Location location)
        {
            this.LocationName = locationName;
            this.Location = location;
        }

        public string LocationName { get; set; }
        public Location Location { get; set; }
    }
}
