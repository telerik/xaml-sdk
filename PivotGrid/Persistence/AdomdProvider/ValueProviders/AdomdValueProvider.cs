using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Pivot.Adomd;

namespace Persistence.ValueProviders
{
    public class AdomdValueProvider : DataProviderValueProvider
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
