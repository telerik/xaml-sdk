using System;
using System.Linq;
using System.Windows;
using Telerik.Windows.Controls;

namespace Windows_Explorer_Like_TreeView_WPF
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void RadTreeView_LoadOnDemand(object sender, Telerik.Windows.RadRoutedEventArgs e)
		{
			e.Handled = true;
			RadTreeViewItem expandedItem = e.OriginalSource as RadTreeViewItem;
			if (expandedItem == null)
				return;

			Drive drive = expandedItem.Item as Drive;
			if (drive != null)
			{
				ServiceFacade.Instance.LoadChildren(drive);
				if (drive.Children.Count == 0)
				{
					expandedItem.IsLoadOnDemandEnabled = false;
				}
				return;
			}

			Directory directory = expandedItem.Item as Directory;
			if (directory != null)
			{
				ServiceFacade.Instance.LoadChildren(directory);
				if (directory.Children.Count == 0)
				{
					expandedItem.IsLoadOnDemandEnabled = false;
				}
			}
		}

		private void RadTreeView_ItemPrepared(object sender, RadTreeViewItemPreparedEventArgs e)
		{
			if (e.PreparedItem.DataContext is File)
			{
				e.PreparedItem.IsLoadOnDemandEnabled = false;
			}
		}
	}
}
