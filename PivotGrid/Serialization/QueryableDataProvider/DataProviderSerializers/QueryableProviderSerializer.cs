using System;
using System.Collections.Generic;
using Telerik.Pivot.Queryable;

namespace QueryableDataProviderSerialization.DataProviderSerializers
{
    public class QueryableProviderSerializer: DataProviderSerializer
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
