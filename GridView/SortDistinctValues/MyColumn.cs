using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Windows.Controls;

namespace SortDistinctValues
{
    public class MyColumn : GridViewDataColumn
    {
        protected override System.Linq.IQueryable SortDistinctValues(System.Linq.IQueryable source)
        {
            return base.SortDistinctValuesByFilteringDisplay(source);
        }
    }
}
