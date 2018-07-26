using System.Runtime.Serialization;
using Telerik.Pivot.Core;

namespace QueryableDataProviderSerialization.DataProviderSerializers
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
