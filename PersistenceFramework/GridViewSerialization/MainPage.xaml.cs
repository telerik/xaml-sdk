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
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using Telerik.Windows.Persistence;
using Telerik.Windows.Persistence.Services;

namespace GridViewSerialization
{
	public partial class MainPage : UserControl
	{
		System.IO.Stream stream;
		public MainPage()
		{
			InitializeComponent();

			ServiceProvider.RegisterPersistenceProvider<ICustomPropertyProvider>(typeof(RadGridView), new GridViewCustomPropertyProvider());
			this.DataContext = ExamplesDB.GetCustomers();
			this.EnsureLoadState();
		}

		private void OnSave(object sender, System.Windows.RoutedEventArgs e)
		{
			PersistenceManager manager = new PersistenceManager();
			this.stream = manager.Save(this.gridView);
			this.EnsureLoadState();
		}

		private void OnLoad(object sender, System.Windows.RoutedEventArgs e)
		{
			this.stream.Position = 0L;
			PersistenceManager manager = new PersistenceManager();
			manager.Load(this.gridView, this.stream);
			this.EnsureLoadState();
		}
		private bool CanLoad()
		{
			return this.stream != null && this.stream.Length > 0;
		}

		private void EnsureLoadState()
		{
			this.buttonLoad.IsEnabled = this.CanLoad();
		}
	}
}
