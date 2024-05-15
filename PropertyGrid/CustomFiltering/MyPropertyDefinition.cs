using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Windows.Controls.Data.PropertyGrid;

namespace CustomFiltering
{
    public class MyPropertyDefinition : PropertyDefinition
    {
        public override bool IsFiltered
        {
            get
            {
                if (!string.IsNullOrEmpty(this.SearchString))
                {
                    var searchStringToLower = this.SearchString.ToLower();
                    return this.Value.ToString().ToLower().Contains(searchStringToLower) || this.DisplayName.ToLower().Contains(searchStringToLower);
                }
                else
                {
                    return true;
                }
            }
        }
    }
}
