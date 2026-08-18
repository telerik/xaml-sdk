using CaliburnMicroIntegration.Models;

namespace CaliburnMicroIntegration.Utilities
{
    public sealed class GridSelectionChangedMessage
    {
        public GridSelectionChangedMessage(LocationInfo selectedLocationInfo)
        {
            SelectedLocationInfo = selectedLocationInfo;
        }

        public LocationInfo SelectedLocationInfo { get; }
    }
}
