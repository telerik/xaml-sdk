using System.Net;
using System.Windows;

namespace OData4Consumption
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            base.OnStartup(e);
        }
    }
}
