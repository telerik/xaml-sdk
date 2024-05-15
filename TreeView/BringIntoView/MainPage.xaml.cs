using System;
using System.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;

namespace BringIntoView
{
	public partial class MainPage : UserControl
	{
		private SampleViewModel sampleVM;

		public MainPage()
		{
			InitializeComponent();

			sampleVM = new SampleViewModel();
			myTreeView.ItemsSource = sampleVM;

			this.textBox.Text = "90.3.3.3";

			BusinessItem item = this.sampleVM.GetItemByName(this.textBox.Text);
			if (item != null)
			{
				item.IsSelected = true;
				string path = item.GetPath();
				myTreeView.BringPathIntoView(path);
			}
		}

		private void BringItem(object sender, RoutedEventArgs e)
		{
			BusinessItem item = this.sampleVM.GetItemByName(this.textBox.Text);
			if (item != null)
			{
				item.IsSelected = true;
				string path = item.GetPath();
				myTreeView.BringPathIntoView(path);
			}
		}

		private void AddItem(object sender, RoutedEventArgs e)
		{
			BusinessItem parent = this.sampleVM.GetItemByName(this.parentBox.Text);

			if (parent != null)
			{
				BusinessItem newItem = new BusinessItem(parent)
				{
					Name = parent.Name + "." + parent.Items.Count,
					IsSelected = true
				};

				parent.Items.Add(newItem);

				string path = newItem.GetPath();
				this.myTreeView.BringPathIntoView(path);
			}
		}
	}
}
