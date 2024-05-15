using System;
using System.Collections.Specialized;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;

namespace ScrollIntoViewAsyncMvvm
{
	public class MyScrollIntoViewAsyncBehavior
	{
		private readonly RadGridView grid = null;

		public MyScrollIntoViewAsyncBehavior(RadGridView grid)
		{
			this.grid = grid;
			this.grid.Items.CollectionChanged += Items_CollectionChanged;
		}

		void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (e.Action == NotifyCollectionChangedAction.Add)
			{
				grid.ScrollIntoViewAsync(e.NewItems[0], null);
			}
		}

		public static readonly DependencyProperty IsEnabledProperty
			= DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(MyScrollIntoViewAsyncBehavior),
				new PropertyMetadata(new PropertyChangedCallback(OnIsEnabledPropertyChanged)));

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
			RadGridView grid = dependencyObject as RadGridView;
			if (grid != null)
			{
				if ((bool)e.NewValue)
				{
					// Create new MyScrollIntoViewAsyncToLastAddedRowBehavior.
					MyScrollIntoViewAsyncBehavior behavior = new MyScrollIntoViewAsyncBehavior(grid);
				}
			}
		}
	}
}
