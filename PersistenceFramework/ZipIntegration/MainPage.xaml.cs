using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Telerik.Windows.Persistence.Services;
using ZipIntegration;

namespace Zip_Integration_SL
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			InitializeComponent();

			ServiceProvider.RegisterPersistenceProvider<IValueProvider>(typeof(BitmapImage), new ImageValueProvider());

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
