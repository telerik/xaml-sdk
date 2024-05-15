using System.Windows;
using Telerik.Windows.Controls;

namespace ShellPrism
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ShellBootstrapper bootstrapper;

        public App()
        {
            bootstrapper = new ShellBootstrapper();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            this.bootstrapper.Run();
            base.OnStartup(e);
        }
    }
}