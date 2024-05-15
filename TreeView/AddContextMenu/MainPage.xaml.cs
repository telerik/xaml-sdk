using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows;
using Telerik.Windows.Controls;

namespace ContextMenu
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			InitializeComponent();
			this.radTreeView.AddHandler(RadMenuItem.ClickEvent, new RoutedEventHandler(OnContextMenuClick));
		}

		RadTreeViewItem clickedElement;
		private void RadContextMenu_Opened(object sender, RoutedEventArgs e)
		{
			// Find the tree item that is associated with the clicked context menu item
			clickedElement = (sender as RadContextMenu).GetClickedElement<RadTreeViewItem>();
		}

		private void OnContextMenuClick(object sender, RoutedEventArgs args)
		{
			// Get the clicked context menu item
			RadMenuItem menuItem = ((RadRoutedEventArgs)args).OriginalSource as RadMenuItem;

			League league = clickedElement.Item as League;
			Telerik.Windows.Controls.ItemsControl parentItemsControl = (Telerik.Windows.Controls.ItemsControl)clickedElement.ParentItem ?? clickedElement.ParentTreeView;
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
