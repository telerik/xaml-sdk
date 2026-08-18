using Caliburn.Micro;

namespace CaliburnMicroIntegration.ViewModels
{
    public class ShellViewModel : Conductor<Screen>.Collection.OneActive
    {
        public ShellViewModel(GridViewModel gridViewModel, MapViewModel mapViewModel)
        {
            this.Panes = new BindableCollection<PaneViewModel>
            {
                gridViewModel,
                mapViewModel
            };
        }

        public BindableCollection<PaneViewModel> Panes { get; set; }
    }
}
