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
	/// Interaction logic for AmazonS3Login.xaml
	/// </summary>
	public partial class AmazonS3Login : UserControl
	{
		public AmazonS3Login()
		{
			InitializeComponent();
		}

        public string AccessKeyId { get; private set; }
        public string SecretAccessKey { get; private set; }

		public event EventHandler LoggedIn;

        private void TextBoxAccessKeyId_TextChanged(object sender, TextChangedEventArgs e)
		{
			var tb = (TextBox)sender;
            this.AccessKeyId = tb.Text;
		}

        private void TextBoxSecretAccessKey_TextChanged(object sender, TextChangedEventArgs e)
		{
			var tb = (TextBox)sender;
            this.SecretAccessKey = tb.Text;
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
