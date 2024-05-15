using HierarchicalDataBinding.Models;
using System.Windows;
using System.Windows.Controls;

namespace HierarchicalDataBinding
{
    public class NavigationViewContentTemplateSelector : DataTemplateSelector
    {
        public DataTemplate EditorsTemplate { get; set; }
        public DataTemplate LayoutControlsTemplate { get; set; }
        public DataTemplate RadComboBoxTemplate { get; set; }
        public DataTemplate RadAutoCompleteBoxTemplate { get; set; }
        public DataTemplate RadTileListTemplate { get; set; }
        public DataTemplate RadTileViewTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var navigationItemModel = item as NavigationItemModel;

            switch (navigationItemModel.Title)
            {
                case "Editors": return this.EditorsTemplate;
                case "Layout controls": return this.LayoutControlsTemplate;
                case "RadComboBox": return this.RadComboBoxTemplate;
                case "RadAutoCompleteBox": return this.RadAutoCompleteBoxTemplate;
                case "RadTileList": return this.RadTileListTemplate;
                case "RadTileView": return this.RadTileViewTemplate;
                default:
                    break;
            }

            return base.SelectTemplate(item, container);
        }
    }
}
