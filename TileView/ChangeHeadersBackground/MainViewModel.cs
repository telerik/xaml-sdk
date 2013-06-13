using System;
using System.Collections.ObjectModel;
using System.Windows.Media;
using Telerik.Windows.Controls;

namespace ChangeHeadersBackground
{
	public class MainViewModel
	{
		public MainViewModel()
		{
			this.Items = new ObservableCollection<ItemViewModel>();
			this.GenerateItems();
		}

		public ObservableCollection<ItemViewModel> Items { get; set; }

		private void GenerateItems()
		{
			var blue = new SolidColorBrush(Colors.Blue);
			var green = new SolidColorBrush(Colors.Green);
			var purple = new SolidColorBrush(Colors.Purple);

			this.Items.Add(new ItemViewModel(blue, green, purple)
			{
				Header = "Item 1",
				CurrentState = TileViewItemState.Maximized,
				Content = "Content of Item 1"
			});
			this.Items.Add(new ItemViewModel(blue, green, purple)
			{
				Header = "Item 2",
				Content = "Content of Item 2"
			});
			this.Items.Add(new ItemViewModel(blue, green, purple)
			{
				Header = "Item 3",
				Content = "Content of Item 3"
			});
			this.Items.Add(new ItemViewModel(blue, green, purple)
			{
				Header = "Item 4",
				Content = "Content of Item 4"
			});
			this.Items.Add(new ItemViewModel(blue, green, purple)
			{
				Header = "Item 5",
				Content = "Content of Item 5"
			});
		}
	}
}