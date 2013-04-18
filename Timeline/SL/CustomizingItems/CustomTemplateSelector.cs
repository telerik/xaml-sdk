using System.Windows;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Timeline;

namespace CustomizingItems
{
	public class CustomItemTemplateSelector : DataTemplateSelector
	{
		public DataTemplate InstantItemTemplate { get; set; }

		public DataTemplate ItemWithDurationTemplate { get; set; }

		public override DataTemplate SelectTemplate(object item, DependencyObject container)
		{
			TimelineDataItem data = item as TimelineDataItem;
			Product productItem;

			if (data == null)
			{
				productItem = item as Product;
			}
			else
			{
				productItem = data.DataItem as Product;
			}

			if (productItem == null)
			{
				return base.SelectTemplate(item, container);
			}

			if (productItem.Duration.Days != 0)
			{
				return this.ItemWithDurationTemplate;
			}
			else
			{
				return this.InstantItemTemplate;
			}
		}
	}
}