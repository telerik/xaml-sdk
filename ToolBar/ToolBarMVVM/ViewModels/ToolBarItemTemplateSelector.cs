using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;

namespace ToolBarMVVM
{
	public class ToolBarItemTemplateSelector : DataTemplateSelector
	{
		public override System.Windows.DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
		{
			if (item is TextBlockViewModel)
			{
				return this.TextBlockTemplate;
			}
			else if (item is SeparatorViewModel)
			{
				return this.SeparatorTemplate;
			}
			else if (item is ButtonViewModel)
			{
				return this.ButtonTemplate;
			}
			else if (item is ColorPickerViewModel)
			{
				return this.ColorPickerTemplate;
			}
			return base.SelectTemplate(item, container);
		}

		public DataTemplate ButtonTemplate { get; set; }
		public DataTemplate TextBlockTemplate { get; set; }
		public DataTemplate SeparatorTemplate { get; set; }
		public DataTemplate ColorPickerTemplate { get; set; }
	}
}
