using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Pivot.Core;

namespace Serialization.DataProviderSerializers
{
    public class LocalDataSourceSerializer : DataProviderSerializer
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
