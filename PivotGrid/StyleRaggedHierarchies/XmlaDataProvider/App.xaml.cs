using System.Net;
using System.Windows;

namespace XmlaStyleRaggedHierarchies
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
