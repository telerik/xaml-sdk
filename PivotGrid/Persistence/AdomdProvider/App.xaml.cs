using System.Net;
using System.Windows;

namespace Persistence
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
        }
    }
}
