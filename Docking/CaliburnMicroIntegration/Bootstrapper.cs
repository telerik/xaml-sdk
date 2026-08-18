using Caliburn.Micro;
using CaliburnMicroIntegration.ViewModels;
using System.Windows;
using Telerik.Windows.Controls;

namespace CaliburnMicroIntegration
{
    public class Bootstrapper : BootstrapperBase
    {
        private SimpleContainer container;

        public Bootstrapper()
        {
            StyleManager.ApplicationTheme = new Windows11Theme();

            Initialize();
        }

        protected override void Configure()
        {
            this.container = new SimpleContainer();

            this.container.Singleton<IWindowManager, WindowManager>();
            this.container.Singleton<IEventAggregator, EventAggregator>();

            this.container.PerRequest<ShellViewModel>();
            this.container.PerRequest<GridViewModel>();
            this.container.PerRequest<MapViewModel>();
            this.container.PerRequest<PaneViewModel, GridViewModel>();
            this.container.PerRequest<PaneViewModel, MapViewModel>();
        }

        protected override async void OnStartup(object sender, StartupEventArgs e)
        {
            var settings = new Dictionary<string, object>
            {
                ["Width"] = 850,
                ["Height"] = 650,
                ["SizeToContent"] = SizeToContent.Manual,
                ["WindowStartupLocation"] = WindowStartupLocation.CenterScreen,
                ["WindowState"] = WindowState.Normal
            };

            await DisplayRootViewForAsync<ShellViewModel>(settings);
        }

        protected override object GetInstance(Type service, string key)
        {
            return this.container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return this.container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            this.container.BuildUp(instance);
        }
    }
}
