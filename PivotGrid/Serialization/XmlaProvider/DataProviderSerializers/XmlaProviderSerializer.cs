using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Pivot.Xmla;

namespace Serialization.DataProviderSerializers
{
    public class XmlaProviderSerializer : DataProviderSerializer
    {
        public override IEnumerable<Type> KnownTypes
        {
            get
            {
                return XmlaPivotSerializationHelper.KnownTypes;
            }
        }
    }
}
