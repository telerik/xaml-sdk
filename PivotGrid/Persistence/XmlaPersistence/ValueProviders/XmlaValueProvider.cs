using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Pivot.Xmla;

namespace Persistence.ValueProviders
{
    public class XmlaValueProvider : DataProviderValueProvider
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
