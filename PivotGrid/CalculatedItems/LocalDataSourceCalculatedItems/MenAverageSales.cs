using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Pivot.Core;
using Telerik.Pivot.Core.Aggregates;

namespace LocalDataSourceCalculatedItems
{
public class MenAverageSales : CalculatedItem
{
    protected override AggregateValue GetValue(IAggregateSummaryValues aggregateSummaryValues)
    {
        AggregateValue[] aggregateValues = {
            aggregateSummaryValues.GetAggregateValue("Andrew Fuller"),
            aggregateSummaryValues.GetAggregateValue("Michael Suyama"),
            aggregateSummaryValues.GetAggregateValue("Robert King"),
            aggregateSummaryValues.GetAggregateValue("Steven Buchanan")
        };

        if (aggregateValues.ContainsError())
        {
            return AggregateValue.ErrorAggregateValue;
        }

        double average = aggregateValues.Average(av => av.ConvertOrDefault<double>());
        return new DoubleAggregateValue(average);
    }
}
}
