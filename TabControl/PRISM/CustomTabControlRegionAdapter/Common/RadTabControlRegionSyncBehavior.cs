using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.Regions.Behaviors;
using Telerik.Windows.Controls;

namespace CustomTabControlRegionAdapter.Infrastructure
{
	public class RadTabControlRegionSyncBehavior : RegionBehavior, IHostAwareRegionBehavior
	{
		///<summary> 
		/// The behavior key for this region sync behavior. 
		///</summary> 
		public const string BehaviorKey = "RadTabControlRegionSyncBehavior";

		private static readonly DependencyProperty IsGeneratedProperty =
			DependencyProperty.RegisterAttached("IsGenerated", typeof(bool), typeof(RadTabControlRegionSyncBehavior), null);

		private RadTabControl hostControl;
		
		/// <summary> 
		/// Gets or sets the <see cref="DependencyObject"/> that the <see cref="IRegion"/> is attached to. 
		/// </summary> 
		/// <value>A <see cref="DependencyObject"/> that the <see cref="IRegion"/> is attached to. 
		/// This is usually a <see cref="FrameworkElement"/> that is part of the tree.</value> 
		public DependencyObject HostControl
		{
			get
			{
				return this.hostControl;
			}

			set
			{
				RadTabControl newValue = value as RadTabControl;
				if (newValue == null)
				{
					throw new InvalidOperationException("HostControlMustBeARadTabControl");
				}

				if (IsAttached)
				{
					throw new InvalidOperationException("HostControlCannotBeSetAfterAttach");
				}

				this.hostControl = newValue;
			}
		}

		/// <summary> 
		/// Override this method to perform the logic after the behavior has been attached. 
		/// </summary> 
		protected override void OnAttach()
		{
			if (this.hostControl == null)
			{
				throw new InvalidOperationException("HostControlCannotBeNull");
			}

			this.SynchronizeItems();
			
			this.hostControl.SelectionChanged += this.OnSelectionChanged;
			this.Region.ActiveViews.CollectionChanged += this.OnActiveViewsChanged;
			this.Region.Views.CollectionChanged += this.OnViewsChanged;

			if (this.hostControl.IsDefaultItemSelected && this.hostControl.SelectedIndex == -1)
			{
				this.hostControl.SelectedIndex = 0;
			}
		}

		/// <summary> 
		/// Gets the item contained in the <see cref="RadTabItem"/>. 
		/// </summary> 
		/// <param name="tabItem">The container item.</param> 
		/// <returns>The item contained in the <paramref name="tabItem"/> if it was generated automatically by the behavior; otherwise <paramref name="tabItem"/>.</returns> 
		protected virtual object GetContainedItem(RadTabItem tabItem)
		{
			if (tabItem == null) throw new ArgumentNullException("tabItem");
			if ((bool)tabItem.GetValue(IsGeneratedProperty))
			{
				return tabItem.Content;
			}

			return tabItem;
		}

		/// <summary> 
		/// Override to change how RadTabItem's are prepared for items. 
		/// </summary> 
		/// <param name="item">The item to wrap in a RadTabItem</param> 
		/// <param name="parent">The parent <see cref="DependencyObject"/></param> 
		/// <returns>A tab item that wraps the supplied <paramref name="item"/></returns> 
		protected virtual RadTabItem PrepareContainerForItem(object item, DependencyObject parent)
		{
			RadTabItem container = item as RadTabItem;
			if (container == null)
			{
				object dataContext = GetDataContext(item);
				container = new RadTabItem();
				container.Content = item;
				container.Style = RadTabControlRegionAdapter.GetItemContainerStyle(parent);
				container.Header = dataContext;
				container.SetValue(IsGeneratedProperty, true);
			}

			return container;
		}

		/// <summary> 
		/// Undoes the effects of the <see cref="PrepareContainerForItem"/> method. 
		/// </summary> 
		/// <param name="tabItem">The container element for the item.</param> 
		protected virtual void ClearContainerForItem(RadTabItem tabItem)
		{
			if (tabItem == null) throw new ArgumentNullException("tabItem");
			if ((bool)tabItem.GetValue(IsGeneratedProperty))
			{
				tabItem.Content = null;
			}
		}

		/// <summary> 
		/// Creates or identifies the element that is used to display the given item. 
		/// </summary> 
		/// <param name="item">The item to get the container for.</param> 
		/// <param name="itemCollection">The parent's <see cref="ItemCollection"/>.</param> 
		/// <returns>The element that is used to display the given item.</returns> 
		protected virtual RadTabItem GetContainerForItem(object item, ItemCollection itemCollection)
		{
			if (itemCollection == null) throw new ArgumentNullException("itemCollection");
			RadTabItem container = item as RadTabItem;
			if (container != null && ((bool)container.GetValue(IsGeneratedProperty)) == false)
			{
				return container;
			}

			foreach (RadTabItem tabItem in itemCollection)
			{
				if ((bool)tabItem.GetValue(IsGeneratedProperty))
				{
					if (tabItem.Content == item)
					{
						return tabItem;
					}
				}
			}

			return null;
		}

		/// <summary> 
		/// Return the appropriate data context.
		/// Otherwise, we just us the item as the data context. 
		/// </summary> 
		private static object GetDataContext(object item)
		{
			FrameworkElement frameworkElement = item as FrameworkElement;
			return frameworkElement == null ? item : frameworkElement.DataContext;
		}

		private void SynchronizeItems()
		{
			List<object> existingItems = new List<object>();
			if (this.hostControl.Items.Count > 0)
			{
				// Control must be empty before "Binding" to a region 
				foreach (object childItem in this.hostControl.Items)
				{
					existingItems.Add(childItem);
				}
			}

			foreach (object view in this.Region.Views)
			{
				RadTabItem tabItem = this.PrepareContainerForItem(view, this.hostControl);
				this.hostControl.Items.Add(tabItem);
			}

			foreach (object existingItem in existingItems)
			{
				this.Region.Add(existingItem);
			}
		}

		//private void OnSelectionChanged(object sender, SelectionChangedEventArgs e) 
		private void OnSelectionChanged(object sender, RoutedEventArgs args)
		{
			var e = args as RadSelectionChangedEventArgs;
			// e.OriginalSource == null, that's why we use sender. 
			if (this.hostControl == sender)
			{
				foreach (RadTabItem tabItem in e.RemovedItems)
				{
					object item = this.GetContainedItem(tabItem);

					// check if the view is in both Views and ActiveViews collections (there may be out of sync) 
					if (this.Region.Views.Contains(item) && this.Region.ActiveViews.Contains(item))
					{
						this.Region.Deactivate(item);
					}
				}

				foreach (RadTabItem tabItem in e.AddedItems)
				{
					object item = this.GetContainedItem(tabItem);
					if (!this.Region.ActiveViews.Contains(item))
					{
						this.Region.Activate(item);
					}
				}
			}
		}

		private void OnActiveViewsChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (e.Action == NotifyCollectionChangedAction.Add)
			{
				this.hostControl.SelectedItem = this.GetContainerForItem(e.NewItems[0], this.hostControl.Items);
			}
			else if (e.Action == NotifyCollectionChangedAction.Remove
					 && this.hostControl.SelectedItem != null
					 && e.OldItems.Contains(this.GetContainedItem((RadTabItem)this.hostControl.SelectedItem)))
			{
				this.hostControl.SelectedItem = null;
			}
		}

		private void OnViewsChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (e.Action == NotifyCollectionChangedAction.Add)
			{
				foreach (object newItem in e.NewItems)
				{
					RadTabItem tabItem = this.PrepareContainerForItem(newItem, this.hostControl);
					this.hostControl.Items.Add(tabItem);
				}
			}
			else if (e.Action == NotifyCollectionChangedAction.Remove)
			{
				foreach (object oldItem in e.OldItems)
				{
					RadTabItem tabItem = this.GetContainerForItem(oldItem, this.hostControl.Items);
					this.hostControl.Items.Remove(tabItem);
					this.ClearContainerForItem(tabItem);
				}
			}
		}
	}
}
