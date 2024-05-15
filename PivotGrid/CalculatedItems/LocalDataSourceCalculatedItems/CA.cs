using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Pivot.Core;
using Telerik.Pivot.Core.Aggregates;

namespace LocalDataSourceCalculatedItems
{
    public class CA : CalculatedItem
    {
        protected override AggregateValue GetValue(IAggregateSummaryValues aggregateSummaryValues)
        {
            AggregateValue aggregateValue = aggregateSummaryValues.GetAggregateValue("Canada");

            if (aggregateValue.IsError())
            {
                return AggregateValue.ErrorAggregateValue;
            }

            var value = aggregateValue.ConvertOrDefault<double>() > 500 ? 500 : 0;
            return new DoubleAggregateValue(value);
        }
    }
}
