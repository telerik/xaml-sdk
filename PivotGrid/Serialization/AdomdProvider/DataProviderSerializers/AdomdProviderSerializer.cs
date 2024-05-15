using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Pivot.Adomd;

namespace Serialization.DataProviderSerializers
{
    public class AdomdProviderSerializer : DataProviderSerializer
    {
        public override IEnumerable<Type> KnownTypes
        {
            get
            {
                return AdomdPivotSerializationHelper.KnownTypes;
            }
        }
    }
}
