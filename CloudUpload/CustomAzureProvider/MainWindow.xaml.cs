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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AzureProvider.AzureUploadSettings settings = new AzureProvider.AzureUploadSettings();

        public MainWindow()
        {
            InitializeComponent();

            this.login.LoggedIn += this.login_LoggedIn;
        }

        void login_LoggedIn(object sender, EventArgs e)
        {
            this.root.IsEnabled = true;
            this.cloudUpload1.Provider = new AzureProvider.AzureUploadProvider(this.login.AccountName, this.login.AccountKey) { UploadFileSettings = this.settings };
        }

        private void ButtonListFiles_Click(object sender, RoutedEventArgs e)
        {
            if (this.cloudUpload1.Provider == null)
            {
                MessageBox.Show("Provider not set.");
                return;
            }

            this.ListBoxUploadedFiles.ItemsSource = null;
            var uris = AzureProvider.Utilities.ListUris(this.login.AccountName, this.login.AccountKey, this.settings.Container);
            this.ListBoxUploadedFiles.ItemsSource = uris;
        }

        private void TextBoxContainer_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.settings.Container = ((TextBox)sender).Text;
        }
    }
}