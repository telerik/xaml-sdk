using System;
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

namespace Customization_MVVM
{
    public class MenuItemContainerTemplateSelector : DataTemplateSelector
    {
        public DataTemplate MenuItem { get; set; }
        public DataTemplate Separator { get; set; }
        public DataTemplate GroupItem { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            MenuItemViewModel mivm = item as MenuItemViewModel;
            if (mivm != null)
            {
                switch (mivm.Type)
                {
                        // TODO: Separator...
                    case MenuItemViewModel.MenuItemTypes.Link:
                    case MenuItemViewModel.MenuItemTypes.SpecialLink:
                    case MenuItemViewModel.MenuItemTypes.TopLevel:
                    case MenuItemViewModel.MenuItemTypes.Title:
                    case MenuItemViewModel.MenuItemTypes.Paragraph:
                    case MenuItemViewModel.MenuItemTypes.Image:
                        return this.MenuItem;
                    case MenuItemViewModel.MenuItemTypes.Gallery:
                    case MenuItemViewModel.MenuItemTypes.TopLevelSection:
                        return this.GroupItem;
                }
            }

            return base.SelectTemplate(item, container);
        }
    }
}
