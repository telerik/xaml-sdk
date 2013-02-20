using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows;
using Telerik.Windows.Controls;

namespace PaneGroupItemsSource
{
	public class PaneGroupExtensions
	{
		private RadPaneGroup Group { get; set; }

		private Dictionary<object, RadPane> Panes { get; set; }

		private PaneGroupExtensions()
		{
			this.Panes = new Dictionary<object, RadPane>();
		}

		private static readonly DependencyProperty PaneGroupExtensionProperty =
			DependencyProperty.RegisterAttached("PaneGroupExtension", typeof(PaneGroupExtensions), typeof(PaneGroupExtensions), null);

		public static readonly DependencyProperty ItemsSourceProperty =
			DependencyProperty.RegisterAttached("ItemsSource", typeof(IEnumerable), typeof(PaneGroupExtensions), new PropertyMetadata(null, OnItemsSourceChanged));

		private void RemoveItem(object paneModel)
		{
			if (this.Panes.ContainsKey(paneModel))
			{
				var pane = this.Panes[paneModel];

				pane.RemoveFromParent();

				this.Panes.Remove(paneModel);
			}
		}

		private void AddItem(object paneModel)
		{
			this.InsertItem(this.Panes.Count, paneModel);
		}

		private void InsertItem(int index, object paneModel)
		{
			if (this.Panes.ContainsKey(paneModel))
				return;

			var paneViewModel = paneModel as PaneModel;

			var pane = new RadPane
			{
				// TODO: Set the needed properties of the RadPane
				DataContext = paneViewModel,
				Header = paneViewModel.Header,
				Content = paneViewModel.Content,
			};

			this.Panes.Add(paneModel, pane);
			if (this.Group.Items.Count != 0)
			{
				this.Group.Items.Insert(index, pane);
			}
			else
			{
				this.Group.Items.Add(pane);
			}
		}

		private void ClearItems()
		{
			foreach (var pane in this.Panes.Values)
			{
				pane.RemoveFromParent();
			}

			this.Panes.Clear();
		}

		private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var group = d as RadPaneGroup;
			var oldValue = e.OldValue as IEnumerable;
			var newValue = e.NewValue as IEnumerable;
			var oldValueObservableCollection = e.OldValue as INotifyCollectionChanged;
			var newValueObservableCollection = e.NewValue as INotifyCollectionChanged;

			if (group != null)
			{
				var extension = GetPaneGroupExtension(group);
				if (extension == null)
				{
					extension = new PaneGroupExtensions { Group = group };
					SetPaneGroupExtension(group, extension);
				}

				if (oldValue != null)
				{
					foreach (var paneModel in oldValue)
					{
						extension.RemoveItem(paneModel);
					}

					if (oldValueObservableCollection != null)
					{
						oldValueObservableCollection.CollectionChanged -= extension.OnItemsSourceCollectionChanged;
					}
				}

				if (newValue != null)
				{
					foreach (var paneModel in newValue)
					{
						extension.AddItem(paneModel);
					}

					if (newValueObservableCollection != null)
					{
						newValueObservableCollection.CollectionChanged += extension.OnItemsSourceCollectionChanged;
					}
				}
			}
		}

		private void OnItemsSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (e.Action == NotifyCollectionChangedAction.Reset)
			{
				this.ClearItems();
				foreach (var paneModel in GetItemsSource(this.Group))
				{
					this.AddItem(paneModel);
				}
			}
			else
			{
				if (e.OldItems != null)
				{
					foreach (var paneModel in e.OldItems)
					{
						this.RemoveItem(paneModel);
					}
				}

				if (e.NewItems != null)
				{
					int index;
					if (e.NewStartingIndex != 0)
					{
						index = this.Group.Items.Count;
					}
					else
					{
						index = e.NewStartingIndex;
					}

					foreach (var paneModel in e.NewItems)
					{
						this.InsertItem(index++, paneModel);
					}
				}
			}
		}

		private static PaneGroupExtensions GetPaneGroupExtension(DependencyObject obj)
		{
			return (PaneGroupExtensions)obj.GetValue(PaneGroupExtensionProperty);
		}

		private static void SetPaneGroupExtension(DependencyObject obj, PaneGroupExtensions value)
		{
			obj.SetValue(PaneGroupExtensionProperty, value);
		}

		public static IEnumerable GetItemsSource(DependencyObject obj)
		{
			return (IEnumerable)obj.GetValue(ItemsSourceProperty);
		}

		public static void SetItemsSource(DependencyObject obj, IEnumerable<RadPane> value)
		{
			obj.SetValue(ItemsSourceProperty, value);
		}
	}
}
