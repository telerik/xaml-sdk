using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Pivot.Core;
using Telerik.Pivot.Core.Aggregates;

namespace QueryableCalculatedFields
{
    public class FreightCalculatedField : CalculatedField
    {
        private RequiredField freightField;

        public FreightCalculatedField()
        {
            this.freightField = RequiredField.ForProperty("Freight");
        }

        protected override IEnumerable<RequiredField> RequiredFields()
        {
            yield return this.freightField;
        }

        protected override AggregateValue CalculateValue(IAggregateValues aggregateValues)
        {
            var aggregateValue = aggregateValues.GetAggregateValue(this.freightField);
            if (aggregateValue.IsError())
            {
                return aggregateValue;
            }

            double freight = aggregateValue.ConvertOrDefault<double>();
            if (freight != 0d)
            {
                return new DoubleAggregateValue(freight * 1.2);
            }

            return null;
        }
    }
}
