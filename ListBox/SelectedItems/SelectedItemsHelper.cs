using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Telerik.Windows;
using Telerik.Windows.Controls;

namespace SelectedItems
{
	public class SelectedItemsHelper
	{
		public static readonly DependencyProperty SelectedItemsProperty =
			DependencyProperty.RegisterAttached("SelectedItems", typeof(IList), typeof(SelectedItemsHelper), new FrameworkPropertyMetadata((IList)null, new PropertyChangedCallback(OnSelectedItemsChanged)));

		public static IList GetSelectedItems(DependencyObject d)
		{
			return (IList)d.GetValue(SelectedItemsProperty);
		}

		public static void SetSelectedItems(DependencyObject d, IList value)
		{
			d.SetValue(SelectedItemsProperty, value);
		}

		private static void OnSelectedItemsChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			var listBox = sender as RadListBox;
			if (listBox != null)
			{
				IList selectedItems = GetSelectedItems(listBox);
				if (selectedItems != null)
				{
					listBox.SelectedItems.Clear();
					foreach (var item in selectedItems)
					{
						listBox.SelectedItems.Add(item);
					}
				}
			}
		}
	}
}
