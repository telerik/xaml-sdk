using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace OpenClosedPanesWithContextMenu
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			InitializeComponent();
			this.RadContextMenu.ItemsSource = new ObservableCollection<RadPane>() { new RadPane() { Header = "Add Pane" } };
		}

		private void RadDocking_Close_1(object sender, Telerik.Windows.Controls.Docking.StateChangeEventArgs e)
		{
			ObservableCollection<RadPane> closedPanes = new ObservableCollection<RadPane>();
			if (this.RadContextMenu.ItemsSource != null)
			{
				closedPanes = this.RadContextMenu.ItemsSource as ObservableCollection<RadPane>;
			}

			foreach (RadPane Pane in e.Panes)
			{
				Pane.Header = string.Format("Show {0}", Pane.Header);
				closedPanes.Add(Pane);
			}

			this.RadContextMenu.ItemsSource = closedPanes;
		}

		private void RadContextMenuItemClick1(object sender, Telerik.Windows.RadRoutedEventArgs e)
		{
			var pane = (e.OriginalSource as RadMenuItem).Header as RadPane;
			if (Convert.ToString(pane.Header) == "Add Pane")
			{
				RadPane newPane = new RadPane() { Header = "New Pane" };
				this.PaneGroup1.AddItem(newPane, Telerik.Windows.Controls.Docking.DockPosition.Center);
			}
			else
			{
				string newHeader = pane.Header.ToString().Replace("Show ", "");
				pane.Header = newHeader;
				pane.IsHidden = false;
				var contextMenu = sender as RadContextMenu;
				ObservableCollection<RadPane> newItemsSource = contextMenu.ItemsSource as ObservableCollection<RadPane>;
				newItemsSource.Remove(pane);
				contextMenu.ItemsSource = newItemsSource;
			}
		}

		private void RadButtonPermanentlyCloseClick(object sender, RoutedEventArgs e)
		{
			this.TabbedPane3.RemoveFromParent();
			this.PermanentlyCloseButton.IsEnabled = false;
		}
	}
}
