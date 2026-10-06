using System.Windows;
using Telerik.Windows.Controls.MultiColumnComboBox;

namespace TreeViewInDropDown
{
    public class TreeViewItemsSourceProvider : ItemsSourceProvider
    {
        protected override Freezable CreateInstanceCore()
        {
            return new TreeViewItemsSourceProvider();
        }
    }
}
