using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Pivot.Core;
using Telerik.Pivot.Core.Aggregates;

namespace QueryableCalculatedItems
{
    public class AmericanOrdersSum: CalculatedItem
    {
        protected override Telerik.Pivot.Core.Aggregates.AggregateValue GetValue(IAggregateSummaryValues aggregateSummaryValues)
        {
            AggregateValue[] aggregateValues = 
            {
                aggregateSummaryValues.GetAggregateValue("Argentina"),
                aggregateSummaryValues.GetAggregateValue("Brazil"),
                aggregateSummaryValues.GetAggregateValue("Canada"),
                aggregateSummaryValues.GetAggregateValue("Mexico"),
                aggregateSummaryValues.GetAggregateValue("USA"),
                aggregateSummaryValues.GetAggregateValue("Venezuela")
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
