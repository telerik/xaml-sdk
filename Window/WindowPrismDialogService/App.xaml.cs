using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Unity;
using System.Windows;

namespace WindowPrismDialogService
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>

    public partial class App : PrismApplication
    {
        private Shell shell;
        protected override Window CreateShell()
        {
            this.shell = Container.Resolve<Shell>();
            return null;
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDialog<NotificationDialog, NotificationDialogViewModel>();
            containerRegistry.RegisterDialogWindow<NotificationDialogWindow>();
        }

        protected override void OnInitialized()
        {
            var regionManager = Container.Resolve<IRegionManager>();
            RegionManager.SetRegionManager(this.shell, regionManager);
            RegionManager.UpdateRegions();

            this.shell.Show();

        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            ViewModelLocationProvider.Register<Shell, ShellWindowViewModel>();
        }
    }
}
