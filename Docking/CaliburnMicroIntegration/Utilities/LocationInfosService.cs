using Caliburn.Micro;
using CaliburnMicroIntegration.Models;
using Telerik.Windows.Controls.Map;

namespace CaliburnMicroIntegration.Utilities
{
    public static class LocationInfosService
    {
        public static BindableCollection<LocationInfo> GetLocationInfos()
        {
            return new BindableCollection<LocationInfo>
            {
                new LocationInfo("Seattle", new Location(47.6062, -122.3321)),
                new LocationInfo("Los Angeles", new Location(34.0522, -118.2437)),
                new LocationInfo("New York", new Location(40.7128, -74.0060)),
                new LocationInfo("Chicago", new Location(41.8781, -87.6298)),
                new LocationInfo("Houston", new Location(29.7604, -95.3698))
            };
        } 
    }
}
