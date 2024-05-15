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

namespace CustomAzureProvider
{
    /// <summary>
    /// Interaction logic for AzureLogin.xaml
    /// </summary>
    public partial class AzureLogin : UserControl
    {
        public AzureLogin()
        {
            InitializeComponent();
        }

        public string AccountName { get; private set; }
        public string AccountKey { get; private set; }

        public event EventHandler LoggedIn;

        private void TextBoxAccountName_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;
            this.AccountName = tb.Text;
        }

        private void TextBoxAccountKey_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;
            this.AccountKey = tb.Text;
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
