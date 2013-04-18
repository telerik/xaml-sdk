using System.Linq;
using System.Windows;
using Telerik.Windows.Controls.Map;

namespace ViewModesDisableCommand
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string bingMapsKey = this.BingMapsKey.Text;

            BingMapProvider provider = new BingMapProvider(MapMode.Aerial, true, bingMapsKey);

            this.DisableCommand(provider, typeof(BingMapBirdsEyeSource).FullName);
            this.DisableCommand(provider, "ChangeModeCommand");

            radMap.Provider = provider;
        }

        private void DisableCommand(MapProviderBase provider, string commandParameter)
        {
            CommandDescription roadCommand = (from cmd in provider.Commands
                                              where (string)cmd.CommandParameter == commandParameter
                                              select cmd).FirstOrDefault();
            if (roadCommand != null)
            {
                roadCommand.IsAllowed = false;
            }
        }
    }
}
