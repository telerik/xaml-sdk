using System.Windows.Input;
using Prism.Commands;
using Prism.Regions;

namespace DockingPrism7.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        public ShellViewModel(IRegionManager manager) : base(manager)
        {
        }

        public ICommand NavigateSentCommand
        {
            get
            {
                return new DelegateCommand(() => Navigate("SentView"));
            }
        }
        public ICommand NavigateIncomingCommand
        {
            get
            {
                return new DelegateCommand(() => Navigate("IncomingView"));
            }
        }
        public ICommand NavigateOutgoingCommand
        {
            get
            {
                return new DelegateCommand(() => Navigate("OutgoingView"));
            }
        }

        public ICommand NavigateBrowseCommand
        {
            get
            {
                return new DelegateCommand(() => Navigate("BrowseView"));
            }
        }

        public ICommand NavigateAdditionalCommand
        {
            get
            {
                return new DelegateCommand(() => Navigate("AdditionalView"));
            }
        }

        private void Navigate(string view)
        {
            RegionManager.RequestNavigate("BrowseRegion", view);
        }
    }
}