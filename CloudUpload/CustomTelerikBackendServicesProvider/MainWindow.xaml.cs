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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {        
        public MainWindow()
        {
            InitializeComponent();
            this.login.LoggedIn += this.login_LoggedIn;
        }

        void login_LoggedIn(object sender, EventArgs e)
        {
            this.root.IsEnabled = true;
            this.cloudUpload1.Provider = new CustomTelerikBackendServicesProvider.TelerikBackendServicesUploadProvider(this.login.ApiKey);
        }

        private void ButtonListFiles_Click(object sender, RoutedEventArgs e)
        {
            if (this.cloudUpload1.Provider == null)
            {
                MessageBox.Show("Provider not set.");
                return;
            }

            this.ListBoxUploadedFiles.ItemsSource = null;
            var uris = TelerikBackendServicesUtilities.ListFiles(this.login.ApiKey);
            this.ListBoxUploadedFiles.ItemsSource = uris;
        }
    }
}
