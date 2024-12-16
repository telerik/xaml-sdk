using System.Windows;
using ZipIntegration;

namespace ZipIntegration_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			this.EnsureLoadState();
		}

		private void OnSave(object sender, System.Windows.RoutedEventArgs e)
		{
			ZipViewModel context = this.DataContext as ZipViewModel;
			if (context != null)
			{
				context.Save(this.target);
				this.EnsureLoadState();
			}
		}

		private void OnLoad(object sender, System.Windows.RoutedEventArgs e)
		{
			ZipViewModel context = this.DataContext as ZipViewModel;
			if (context != null)
				context.Load(this.target);
			this.EnsureLoadState();
		}

		private bool CanLoad()
		{
			ZipViewModel context = this.DataContext as ZipViewModel;
			if (context != null)
				return context.CanLoad(this.target);
			else
				return false;
		}

		private void EnsureLoadState()
		{
			this.buttonLoad.IsEnabled = this.CanLoad();
		}
	}
}
