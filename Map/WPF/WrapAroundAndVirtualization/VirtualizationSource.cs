using System.Collections.Generic;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Map;

namespace UI_Virtualization_And_Wraparound
{
	public class VirtualizationSource : IMapItemsVirtualizationSource
	{
        private bool isFirstRequest = true;
        private RadMap map;
        private ExampleViewModel dataContext;

		public VirtualizationSource(ExampleViewModel dataContext)
        {
			this.dataContext = dataContext;
        }

		public void MapItemsRequest(object sender, MapItemsRequestEventArgs eventArgs)
		{
            if (this.isFirstRequest)
            {
                this.map = eventArgs.Layer.MapControl;
                this.isFirstRequest = false;
            }

			double minZoom = eventArgs.MinZoom;

            // Coercing (normalizing) the map portion of the request in normal [-180; 180] longitude range.
            Location upperLeft = map.GetCoercedLocation(eventArgs.UpperLeft);
            Location lowerRight = map.GetCoercedLocation(eventArgs.LowerRight);

            if (this.dataContext == null)
				return;

			if (minZoom == 3)
			{
				// request areas.
				List<StoreLocation> list = this.dataContext.GetStores(
					upperLeft.Latitude,
                    upperLeft.Longitude,
                    lowerRight.Latitude,
					lowerRight.Longitude,
					StoreType.Area);

				dataContext.SetStores(list, eventArgs);
			}

			if (minZoom == 9)
			{
				// request areas
				List<StoreLocation> list = this.dataContext.GetStores(
                    upperLeft.Latitude,
                    upperLeft.Longitude,
                    lowerRight.Latitude,
                    lowerRight.Longitude, 
					StoreType.Store);

				this.dataContext.SetStores(list, eventArgs);
			}
		}
	}
}
