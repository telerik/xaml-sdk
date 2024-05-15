using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Telerik.Pivot.Core;

namespace Serialization.DataProviderSerializers
{
    [DataContract]
    public class DataProviderSettings
    {
        [DataMember]
        public object[] Aggregates { get; set; }
        
        [DataMember]
        public object[] Filters { get; set; }
        
        [DataMember]
        public object[] Rows { get; set; }

        [DataMember]
        public object[] Columns { get; set; }
        
        [DataMember]
        public int AggregatesLevel { get; set; }

        [DataMember]
        public PivotAxis AggregatesPosition { get; set; }
    }
}
