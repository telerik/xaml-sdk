using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Pivot.Core;
using Telerik.Pivot.Core.Aggregates;

namespace LocalDataSourceCalculatedItems
{
    public class WomenAverageSales : CalculatedItem
    {
        protected override AggregateValue GetValue(IAggregateSummaryValues aggregateSummaryValues)
        {
            AggregateValue[] aggregateValues = {
               aggregateSummaryValues.GetAggregateValue("Anne Dodsworth"),
               aggregateSummaryValues.GetAggregateValue("Janet Leverling"),
               aggregateSummaryValues.GetAggregateValue("Laura Callahan"),
               aggregateSummaryValues.GetAggregateValue("Margaret Peacock"),
               aggregateSummaryValues.GetAggregateValue("Nancy Davolio")
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
