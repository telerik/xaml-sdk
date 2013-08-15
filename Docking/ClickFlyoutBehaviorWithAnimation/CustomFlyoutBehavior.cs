using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Docking;

namespace ClickFlyoutBehaviorWithAnimation
{
    public class CustomFlyoutBehavior : IFlyoutBehavior
    {
        void IFlyoutBehavior.OnMouseEnter(IFlyoutHost host, RadPane targetPane)
        {
        }

        void IFlyoutBehavior.OnMouseLeave(IFlyoutHost host)
        {
        }

        void IFlyoutBehavior.OnOpeningTimerTimeout(IFlyoutHost host)
        {
        }

        void IFlyoutBehavior.OnClosingTimerTimeout(IFlyoutHost host)
        {
        }

        void IFlyoutBehavior.OnPaneActivated(IFlyoutHost host, RadPane targetPane)
        {
            host.SetSelectedPane(targetPane);
            if (host.CurrentState == FlyoutState.Closed)
            {
                host.StartOpenAnimation();
            }
        }

        void IFlyoutBehavior.OnPaneDeactivated(IFlyoutHost host, RadPane targetPane)
        {
            var selectedPane = host.SelectedPane;
            if (selectedPane != null && !selectedPane.IsActive && host.CurrentState == FlyoutState.Opened)
            {
                host.StartCloseAnimation();
            }
        }

        void IFlyoutBehavior.OnPaneMouseLeftButtonDown(IFlyoutHost host, RadPane targetPane)
        {
            if (host.CurrentState != FlyoutState.Opened)
            {
                host.StartOpenAnimation();
            }
            else
            {
                host.StartCloseAnimation();
            }
        }
    }
}
