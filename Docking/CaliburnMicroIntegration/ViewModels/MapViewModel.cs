using Caliburn.Micro;
using CaliburnMicroIntegration.Models;
using CaliburnMicroIntegration.Utilities;

namespace CaliburnMicroIntegration.ViewModels
{
    public class MapViewModel : PaneViewModel, IHandle<GridSelectionChangedMessage>
    {
        private readonly IEventAggregator eventAggregator;

        public MapViewModel(IEventAggregator eventAggregator)
        {
            this.DisplayName = "Map";

            this.LocationInfos = LocationInfosService.GetLocationInfos();

            this.eventAggregator = eventAggregator;
            this.eventAggregator.SubscribeOnUIThread(this);
        }

        private LocationInfo selectedLocationInfo;

        public LocationInfo SelectedLocationInfo
        {
            get { return this.selectedLocationInfo; }
            set 
            {
                if (this.selectedLocationInfo != value)
                {
                    this.selectedLocationInfo = value;
                    this.NotifyOfPropertyChange();
                }
            }
        }

        public BindableCollection<LocationInfo> LocationInfos { get; set; }

        public Task HandleAsync(GridSelectionChangedMessage message, CancellationToken cancellationToken)
        {
            this.SelectedLocationInfo = message.SelectedLocationInfo;

            return Task.CompletedTask;
        }
    }
}
