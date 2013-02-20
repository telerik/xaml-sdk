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
using Telerik.Windows.Persistence.Services;
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

			ServiceProvider.RegisterPersistenceProvider<ITypeConverterProvider>(typeof(System.Windows.Media.ImageSource), new ImageTypeProvider());

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
