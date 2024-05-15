using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;

namespace PaneGroupItemsSource
{
	public partial class Example : UserControl
	{
		private ObservableCollection<PaneModel> panes;

		public Example()
		{
			InitializeComponent();

			this.DataContext = this;
			this.GenerateData();
		}

		private void GenerateData()
		{
			this.panes = new ObservableCollection<PaneModel>()
			{
				new PaneModel
				{
					Header = "Pane 1",
					Content = "Sample Content 1"
				},
				new PaneModel
				{
					Header = "Pane 2",
					Content = "Sample Content 2"
				},
			};

			this.PaneGroup.DataContext = this.panes;
		}

		private void ButtonReset_Click(object sender, RoutedEventArgs e)
		{
			this.GenerateData();
		}

		private void ButtonAdd_Click(object sender, RoutedEventArgs e)
		{
			this.panes.Add(new PaneModel
			{
				Header = "New Added Pane",
				Content = "New Sample Content"
			});
		}

		private void ButtonInsert_Click(object sender, RoutedEventArgs e)
		{
			this.panes.Insert(0, new PaneModel
			{
				Header = "New Inserted Pane",
				Content = "New Sample Content"
			});
		}

		private void ButtonDelFirst_Click(object sender, RoutedEventArgs e)
		{
			if (this.panes.Count != 0)
			{
				this.panes.RemoveAt(0);
			}
		}

		private void ButtonDelLast_Click(object sender, RoutedEventArgs e)
		{
			if (this.panes.Count != 0)
			{
				this.panes.Remove(this.panes.Last());
			}
		}
	}
}
