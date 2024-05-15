using System.Collections.Generic;
using Telerik.Pivot.Core;
using Telerik.Pivot.Core.Aggregates;

namespace LocalDataSourceCalculatedFields
{
    public class CommissionCalculatedField : CalculatedField
    {
        private RequiredField extendPriceField;

        public CommissionCalculatedField()
        {
            this.Name = "Commission";
            this.extendPriceField = RequiredField.ForProperty("ExtendedPrice");
        }

        protected override IEnumerable<RequiredField> RequiredFields()
        {
            yield return this.extendPriceField;
        }

        protected override AggregateValue CalculateValue(IAggregateValues aggregateValues)
        {
            var aggregateValue = aggregateValues.GetAggregateValue(this.extendPriceField);
            if (aggregateValue.IsError())
            {
                return aggregateValue;
            }

            double extendedPrice = aggregateValue.ConvertOrDefault<double>();
            if (extendedPrice > 15000)
            {
                return new DoubleAggregateValue(extendedPrice * 0.1);
            }

            return null;
        }
    }
}
