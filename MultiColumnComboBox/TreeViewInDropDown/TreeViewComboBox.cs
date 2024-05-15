using System.Collections.Generic;
using Telerik.Windows.Controls;

namespace TreeViewInDropDown
{
    public class TreeViewComboBox : RadMultiColumnComboBox
    {
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.DropDownContentManager = new TreeViewDropDownContentManager(this);
        }

        public void InvokeSelectionChanged(IList<object> addedItems, IList<object> removedItems)
        {
            this.RaiseSelectionChanged(addedItems, removedItems);
        }

        public void ClearText()
        {
            this.ClearSearchText();
        }
    }
}
