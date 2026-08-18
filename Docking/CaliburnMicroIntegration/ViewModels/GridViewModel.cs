using Caliburn.Micro;
using CaliburnMicroIntegration.Models;
using CaliburnMicroIntegration.Utilities;
using Telerik.Windows.Controls.Map;

namespace CaliburnMicroIntegration.ViewModels
{
    public class GridViewModel : PaneViewModel
    {
        private readonly IEventAggregator eventAggregator;
        private LocationInfo selectedLocationInfo;

        public GridViewModel(IEventAggregator eventAggregator)
        {
            this.DisplayName = "Locations";

            this.LocationInfos = LocationInfosService.GetLocationInfos();

            this.eventAggregator = eventAggregator;
        }

        public LocationInfo SelectedLocationInfo
        {
            get { return this.selectedLocationInfo; }
            set 
            {
                if (this.selectedLocationInfo != value)
                {
                    this.selectedLocationInfo = value;
                    this.NotifyOfPropertyChange();

                    this.eventAggregator.PublishOnUIThreadAsync(new Utilities.GridSelectionChangedMessage(value));
                }
            }
        }

        public BindableCollection<LocationInfo> LocationInfos { get; set; }
    }
}
