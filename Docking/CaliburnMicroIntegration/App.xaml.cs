using System.Windows;

namespace CaliburnMicroIntegration
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            new Bootstrapper();
        }
    }
}
