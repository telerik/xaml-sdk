using System;
using System.Windows.Media.Imaging;
using Telerik.Windows.Controls.Docking;
using Telerik.Windows.Controls.Navigation;

namespace FloatingPaneTaskbarIcons
{
    public class CustomDefaultGeneratedItemsFactory : DefaultGeneratedItemsFactory
    {
        public override ToolWindow CreateToolWindow()
        {
            var window = base.CreateToolWindow();
            RadWindowInteropHelper.SetShowInTaskbar(window, true);
            RadWindowInteropHelper.SetIcon(window, new BitmapImage(new Uri("pack://application:,,,/FloatingPaneTaskbarIcons;component/Images/icon-default.png", UriKind.Absolute)));

            return window;
        }
    }
}
