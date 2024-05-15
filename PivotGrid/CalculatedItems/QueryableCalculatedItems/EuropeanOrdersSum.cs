using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Pivot.Core;
using Telerik.Pivot.Core.Aggregates;

namespace QueryableCalculatedItems
{
    public class EuropeanOrdersSum : CalculatedItem
    {
        protected override Telerik.Pivot.Core.Aggregates.AggregateValue GetValue(IAggregateSummaryValues aggregateSummaryValues)
        {
            AggregateValue[] aggregateValues = 
            {
                aggregateSummaryValues.GetAggregateValue("Italy"),
                aggregateSummaryValues.GetAggregateValue("Germany"),
                aggregateSummaryValues.GetAggregateValue("Austria"),
                aggregateSummaryValues.GetAggregateValue("Belgum"),
                aggregateSummaryValues.GetAggregateValue("Denmark"),
                aggregateSummaryValues.GetAggregateValue("France"),
                aggregateSummaryValues.GetAggregateValue("Ireland"),
                aggregateSummaryValues.GetAggregateValue("Norway"),
                aggregateSummaryValues.GetAggregateValue("Poland"),
                aggregateSummaryValues.GetAggregateValue("Portugal"),
                aggregateSummaryValues.GetAggregateValue("Spain"),
                aggregateSummaryValues.GetAggregateValue("Sweden"),
                aggregateSummaryValues.GetAggregateValue("Switzerland"),
                aggregateSummaryValues.GetAggregateValue("UK")
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
