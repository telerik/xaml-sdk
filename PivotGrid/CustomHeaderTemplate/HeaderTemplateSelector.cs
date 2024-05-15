using System.Windows;
using System.Windows.Controls;
using Telerik.Pivot.Core;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Pivot;

namespace CustomHeaderTemplate
{
    public class HeaderTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ProductTemplate { get; set; }

        public override System.Windows.DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
        {
            FrameworkElement element = container as FrameworkElement;
            GroupData data = element.DataContext as GroupData;

            PropertyGroupDescriptionBase pgd = data.GroupDescription as PropertyGroupDescriptionBase;

            if (pgd != null && pgd.PropertyName == "Product")
            {
                return this.ProductTemplate;
            }

            return base.SelectTemplate(item, container);
        }

    }
}
