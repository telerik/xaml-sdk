using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CustomTelerikBackendServicesProvider
{
    /// <summary>
    /// Interaction logic for TelerikBackendServicesLogin.xaml
    /// </summary>
    public partial class TelerikBackendServicesLogin : UserControl
    {
        public TelerikBackendServicesLogin()
        {
            InitializeComponent();
        }

        public string ApiKey { get; private set; }

        public event EventHandler LoggedIn;

        private void TextBoxApiKey_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;
            this.ApiKey = tb.Text;
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            if (this.LoggedIn != null)
            {
                this.LoggedIn(this, EventArgs.Empty);
            }
        }
    }
}
