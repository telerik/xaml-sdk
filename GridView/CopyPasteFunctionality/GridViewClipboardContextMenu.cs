using System;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.ComponentModel;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Controls;

namespace CopyPasteFunctionality
{
	public class GridViewClipboardContextMenu
	{
		private readonly RadGridView grid = null;

		public GridViewClipboardContextMenu(RadGridView grid)
		{
			this.grid = grid;
		}

		public static readonly DependencyProperty IsEnabledProperty
			= DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(GridViewClipboardContextMenu),
				new System.Windows.PropertyMetadata(new PropertyChangedCallback(OnIsEnabledPropertyChanged)));

		public static void SetIsEnabled(DependencyObject dependencyObject, bool enabled)
		{
			dependencyObject.SetValue(IsEnabledProperty, enabled);
		}

		public static bool GetIsEnabled(DependencyObject dependencyObject)
		{
			return (bool)dependencyObject.GetValue(IsEnabledProperty);
		}

		private static void OnIsEnabledPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
		{
			var grid = dependencyObject as RadGridView;
			if (grid != null)
			{
				if ((bool)e.NewValue)
				{
					var menu = new GridViewClipboardContextMenu(grid);
					menu.Attach();
				}
			}
		}

		private void Attach()
		{
			if (grid != null)
			{
				RadContextMenu contextMenu = new RadContextMenu();

				StyleManager.SetTheme(contextMenu, StyleManager.GetTheme(grid));

				contextMenu.Opened += OnMenuOpened;
				contextMenu.ItemClick += OnMenuItemClick;

				RadContextMenu.SetContextMenu(grid, contextMenu);
			}
		}

		private void OnMenuOpened(object sender, RoutedEventArgs e)
		{
			var menu = (RadContextMenu)sender;

			menu.Items.Clear();

			var copyItem = new RadMenuItem()
			{
				Header = "GridView Copy Mode",
				Icon = new Image()
				{
					Stretch = System.Windows.Media.Stretch.None,
					Source = new BitmapImage(new Uri("/Images/copy.png", UriKind.RelativeOrAbsolute))
				}
			};
			copyItem.Items.Add(new RadMenuItem() { Header = "Default" });
			copyItem.Items.Add(new RadMenuItem() { Header = "Copy with headers" });
			copyItem.Items.Add(new RadMenuItem() { Header = "Copy with footers" });
			copyItem.Items.Add(new RadMenuItem() { Header = "Copy All" });
			copyItem.Items.Add(new RadMenuItem() { Header = "Disable copying" });
			menu.Items.Add(copyItem);

			var pasteItem = new RadMenuItem()
			{
				Header = "GridView Paste Mode",
				Icon = new Image()
				{
					Stretch = System.Windows.Media.Stretch.None,
					Source = new BitmapImage(new Uri("/Images/paste.png", UriKind.RelativeOrAbsolute))
				}
			};
			pasteItem.Items.Add(new RadMenuItem() { Header = "Default" });
			pasteItem.Items.Add(new RadMenuItem() { Header = "Skip first and last line" });
			pasteItem.Items.Add(new RadMenuItem() { Header = "Skip hidden columns" });
			pasteItem.Items.Add(new RadMenuItem() { Header = "Override all selected cells on pasting" });
			pasteItem.Items.Add(new RadMenuItem() { Header = "Override all selected items on pasting" });
			pasteItem.Items.Add(new RadMenuItem() { Header = "Insert new items on pasting" });
			pasteItem.Items.Add(new RadMenuItem() { Header = "Disable pasting" });
			menu.Items.Add(pasteItem);
		}
		void OnMenuItemClick(object sender, RoutedEventArgs e)
		{
			var menu = (RadContextMenu)sender;

			var cell = menu.GetClickedElement<GridViewCell>();
			var clickedItem = ((RadRoutedEventArgs)e).OriginalSource as RadMenuItem;			

			string header = Convert.ToString(clickedItem.Header);

			using (grid.DeferRefresh())
			{
				switch (header)
				{ 
					case "Copy All":
						this.grid.ClipboardCopyMode = GridViewClipboardCopyMode.All;
						break;
					case "Copy with headers":
						this.grid.ClipboardCopyMode = GridViewClipboardCopyMode.Header | GridViewClipboardCopyMode.Cells;
						break;
					case "Copy with footers":
						this.grid.ClipboardCopyMode = GridViewClipboardCopyMode.Footer | GridViewClipboardCopyMode.Cells;
						break;
					case "Default":
						this.grid.ClipboardCopyMode = GridViewClipboardCopyMode.Default;
						break;
					case "Disable copying":
						this.grid.ClipboardCopyMode = GridViewClipboardCopyMode.None;
						break;
					case "Default Pasting":
						this.grid.ClipboardPasteMode = GridViewClipboardPasteMode.Default;
						break;
					case "Skip hidden columns":
						this.grid.ClipboardPasteMode = GridViewClipboardPasteMode.SkipHiddenColumns;
						break;
					case "Skip first and last line":
						this.grid.ClipboardPasteMode = GridViewClipboardPasteMode.SkipFirstLine | GridViewClipboardPasteMode.SkipLastLine;
						break;
					case "Override all selected cells on pasting":
						this.grid.ClipboardPasteMode = GridViewClipboardPasteMode.AllSelectedCells;
						break;
					case "Override all selected items on pasting":
						//this.grid.ClipboardPasteMode = GridViewClipboardPasteMode.AllSelectedRows;
						break;
					case "Insert new items on pasting":
					 //   this.grid.ClipboardPasteMode = GridViewClipboardPasteMode.InsertNewRows;
						break;
					case "Disable Pasting":
						this.grid.ClipboardPasteMode = GridViewClipboardPasteMode.None;
						break;
				}
			}			
		}
	}
}
	
