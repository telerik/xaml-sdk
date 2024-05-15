using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Data;

namespace SortGroupByAggregate
{
    public class SortingGroupDescriptor : GroupDescriptor
    {
        private AggregateFunction sortFunction;
        public AggregateFunction SortFunction
        {
            get
            {
                return sortFunction;
            }
            set
            {
                sortFunction = value;

                this.OnPropertyChanged("SortFunction");
            }
        }

        public override Expression CreateGroupSortExpression(Expression groupingExpression)
        {
            if (this.SortFunction != null)
            {
                return this.SortFunction.CreateAggregateExpression(groupingExpression);
            }
            else
            {
                return base.CreateGroupSortExpression(groupingExpression);
            }
        }
    }
}
