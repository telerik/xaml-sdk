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

            BingRestMapProvider provider = new BingRestMapProvider(MapMode.Aerial, true, bingMapsKey);

            this.DisableCommand(provider, typeof(BingRestBirdsEyeSource).FullName);
            this.DisableCommand(provider, "ChangeModeCommand");

            radMap.Provider = provider;
        }

        private void DisableCommand(MapProviderBase provider, string commandParameter)
        {
            CommandDescription command = (from cmd in provider.Commands
                                              where (string)cmd.CommandParameter == commandParameter
                                              select cmd).FirstOrDefault();
            if (command != null)
            {
                command.IsAllowed = false;
            }
        }
    }
}
