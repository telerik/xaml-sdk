using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls.Data.PropertyGrid;

namespace GroupStyleSelector
{
    class MyGroupStyleSelector : StyleSelector
    {
        public override Style SelectStyle(object item, DependencyObject container)
        {
            GroupDefinition groupDef = item as GroupDefinition;

            if (groupDef.DisplayName == "Name")
            {
                return this.NameGroupStyle;
            }
            else if (groupDef.DisplayName == "Work Information")
            {
                return this.WorkGroupStyle;
            }
            else
            {
                return this.PersonalGroupStyle;
            }

        }
        public Style NameGroupStyle { get; set; }
        public Style WorkGroupStyle { get; set; }
        public Style PersonalGroupStyle { get; set; }
        
    }
}
