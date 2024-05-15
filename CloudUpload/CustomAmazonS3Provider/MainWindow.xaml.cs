using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CustomAmazonS3Provider
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		AmazonS3Provider.AmazonS3UploadSettings settings = new AmazonS3Provider.AmazonS3UploadSettings();
		public MainWindow()
		{
			InitializeComponent();

			this.login.LoggedIn += this.login_LoggedIn;
		}

		void login_LoggedIn(object sender, EventArgs e)
		{
			this.root.IsEnabled = true;
			this.cloudUpload1.Provider = new AmazonS3Provider.AmazonS3UploadProvider(this.login.AccessKeyId, this.login.SecretAccessKey) { UploadFileSettings = this.settings };
		}

		private void ButtonListFiles_Click(object sender, RoutedEventArgs e)
		{
			if (this.cloudUpload1.Provider == null)
			{
				MessageBox.Show("Provider not set.");
				return;
			}

			this.ListBoxUploadedFiles.ItemsSource = null;
            var uris = AmazonS3Provider.Utilities.ListFiles(this.settings.Bucket, this.login.AccessKeyId, this.login.SecretAccessKey);
			this.ListBoxUploadedFiles.ItemsSource = uris;
		}

        private void TextBoxBucket_TextChanged(object sender, TextChangedEventArgs e)
		{
			this.settings.Bucket = ((TextBox)sender).Text;
		}
	}
}
