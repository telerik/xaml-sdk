using System.Windows;
using Telerik.Windows.Controls;
using System.Windows.Controls;

namespace Customization_MVVM
{
	public class MenuItemStyleSelector : StyleSelector
	{
		public Style TopLevel { get; set; }
		public Style TopLevelSection { get; set; }
		public Style Title { get; set; }
		public Style Link { get; set; }
		public Style SpecialLink { get; set; }
		public Style Paragraph { get; set; }
		public Style Gallery { get; set; }
		public Style Image { get; set; }

		public Style ParagraphImage { get; set; }

		public override Style SelectStyle(object item, DependencyObject container)
		{
			MenuItemViewModel menuItem = item as MenuItemViewModel;
			if (menuItem != null)
			{
				switch (menuItem.Type)
				{
					case MenuItemViewModel.MenuItemTypes.TopLevel: return this.TopLevel;
					case MenuItemViewModel.MenuItemTypes.TopLevelSection: return this.TopLevelSection;
					case MenuItemViewModel.MenuItemTypes.Title: return this.Title;
					case MenuItemViewModel.MenuItemTypes.Link: return this.Link;
					case MenuItemViewModel.MenuItemTypes.SpecialLink: return this.SpecialLink;
					case MenuItemViewModel.MenuItemTypes.Paragraph: return this.Paragraph;
					case MenuItemViewModel.MenuItemTypes.Gallery: return this.Gallery;
					case MenuItemViewModel.MenuItemTypes.Image: return this.Image;
					default: return this.Link;
				}
			}
			ParagraphImageMenuItemViewMode imageMenuItem = item as ParagraphImageMenuItemViewMode;
			if (imageMenuItem != null)
			{
				return this.ParagraphImage;
			}
			return null;
		}
	}
}
