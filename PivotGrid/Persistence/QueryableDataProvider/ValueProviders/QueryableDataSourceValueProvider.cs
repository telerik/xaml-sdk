using System;
using System.Collections.Generic;
using Telerik.Pivot.Queryable;

namespace QueryableDataProviderPersistence.ValueProviders
{
    public class QueryableDataSourceValueProvider: DataProviderValueProvider
    {
        public override IEnumerable<Type> KnownTypes
        {
            get 
            {
                return QueryablePivotSerializationHelper.KnownTypes;
            }
        }
    }
}
