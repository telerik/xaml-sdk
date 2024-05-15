using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows;
using Telerik.Windows.Controls;

namespace ContextMenu
{
	public partial class MainWindow : Window
	{
		RadTreeViewItem clickedElement;

		public MainWindow()
		{
			this.InitializeComponent();
			this.radTreeView.AddHandler(RadMenuItem.ClickEvent, new RoutedEventHandler(OnContextMenuClick));
		}

		private void RadContextMenu_Opened(object sender, RoutedEventArgs e)
		{
			// Find the tree item that is associated with the clicked context menu item
			this.clickedElement = (sender as RadContextMenu).GetClickedElement<RadTreeViewItem>();
		}

		private void OnContextMenuClick(object sender, RoutedEventArgs args)
		{
			// Get the clicked context menu item
			RadMenuItem menuItem = ((RadRoutedEventArgs)args).OriginalSource as RadMenuItem;

			League league = this.clickedElement.Item as League;
			ItemsControl parentItemsControl = (ItemsControl)this.clickedElement.ParentItem ?? this.clickedElement.ParentTreeView;
			string header = menuItem.Header as string;
			switch (header)
			{
				case "New Child":
					league.Divisions.Add(new Division("New Division"));
					break;
				case "New Sibling":
					(parentItemsControl.ItemsSource as ObservableCollection<League>).Add(new League("New League"));
					break;
				case "Delete":
					(parentItemsControl.ItemsSource as ObservableCollection<League>).Remove(league);
					break;
			}
		}
	}
}