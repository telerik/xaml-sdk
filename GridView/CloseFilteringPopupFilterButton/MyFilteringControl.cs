using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Controls;

namespace CloseFilteringPopupFilterButton
{
    class MyFilteringControl : FilteringControl
    {
        public MyFilteringControl(GridViewColumn column) : base(column)
        {
        }

        protected override void OnApplyFilter()
        {
            base.OnApplyFilter();

            var popup = this.ParentOfType<System.Windows.Controls.Primitives.Popup>();
            if (popup != null)
            {
                popup.IsOpen = false;
            }
        }
    }
}