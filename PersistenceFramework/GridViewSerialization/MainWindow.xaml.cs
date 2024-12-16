﻿using System;
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
using GridViewSerialization;
using Telerik.Windows.Controls;
using Telerik.Windows.Persistence;
using Telerik.Windows.Persistence.Services;

namespace GridViewSerialization_WPF
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		System.IO.Stream stream;
        private PersistenceManager manager = new PersistenceManager()
            .AllowDataAssembly()
            .AllowCoreControls()
            .AllowGridViewControls()
            .AllowTypes(
                typeof(ColumnProxy),
                typeof(SortDescriptorProxy),
                typeof(GroupDescriptorProxy),
                typeof(FilterDescriptorProxy),
                typeof(FilterSetting),
                typeof(List<ColumnProxy>),
                typeof(List<SortDescriptorProxy>),
                typeof(List<GroupDescriptorProxy>),
                typeof(List<FilterDescriptorProxy>),
                typeof(List<FilterSetting>),
                typeof(List<object>)
            );

		public MainWindow()
		{
			InitializeComponent();

			ServiceProvider.RegisterPersistenceProvider<ICustomPropertyProvider>(typeof(RadGridView), new GridViewCustomPropertyProvider());
			this.DataContext = ExamplesDB.GetCustomers();
			this.EnsureLoadState();
		}

		private void OnSave(object sender, System.Windows.RoutedEventArgs e)
		{
			this.stream = manager.Save(this.gridView);
			this.EnsureLoadState();
		}

		private void OnLoad(object sender, System.Windows.RoutedEventArgs e)
		{
			this.stream.Position = 0L;
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
