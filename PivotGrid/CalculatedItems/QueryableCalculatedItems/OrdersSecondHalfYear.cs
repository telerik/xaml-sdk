using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Pivot.Core;
using Telerik.Pivot.Core.Aggregates;
using Telerik.Pivot.Core.Groups;

namespace QueryableCalculatedItems
{
    public class OrdersSecondHalfYear : CalculatedItem
    {
        protected override Telerik.Pivot.Core.Aggregates.AggregateValue GetValue(IAggregateSummaryValues aggregateSummaryValues)
        {
            AggregateValue[] aggregateValues = 
            {
                aggregateSummaryValues.GetAggregateValue(new QuarterGroup(3)),
                aggregateSummaryValues.GetAggregateValue(new QuarterGroup(4))
            };

            if (aggregateValues.ContainsError())
            {
                return AggregateValue.ErrorAggregateValue;
            }

            double sum = aggregateValues.Sum(s => s.ConvertOrDefault<double>());

            return new DoubleAggregateValue(sum);
        }
    }
}
