using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Pivot.Core;

namespace Persistence.ValueProviders
{
    public class LocalDataSourceValueProvider : DataProviderValueProvider
    {
        public override IEnumerable<Type> KnownTypes
        {
            get
            {
                return PivotSerializationHelper.KnownTypes;
            }
        }
    }
}
