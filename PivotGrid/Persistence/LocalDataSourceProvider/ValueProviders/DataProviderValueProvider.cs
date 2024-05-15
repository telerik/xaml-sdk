using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Telerik.Pivot.Core;
using Telerik.Windows.Persistence.Services;

namespace Persistence.ValueProviders
{
    public abstract class DataProviderValueProvider : IValueProvider
    {
        public abstract IEnumerable<Type> KnownTypes { get; }

        public string ProvideValue(object context)
        {
            string serialized = string.Empty;

            IDataProvider dataProvider = context as IDataProvider;
            if (dataProvider != null)
            {
                MemoryStream stream = new MemoryStream();

                DataProviderSettings settings = new DataProviderSettings()
                {
                    Aggregates = dataProvider.Settings.AggregateDescriptions.OfType<object>().ToArray(),
                    Filters = dataProvider.Settings.FilterDescriptions.OfType<object>().ToArray(),
                    Rows = dataProvider.Settings.RowGroupDescriptions.OfType<object>().ToArray(),
                    Columns = dataProvider.Settings.ColumnGroupDescriptions.OfType<object>().ToArray(),
                    AggregatesLevel = dataProvider.Settings.AggregatesLevel,
                    AggregatesPosition = dataProvider.Settings.AggregatesPosition
                };

                DataContractSerializer serializer = new DataContractSerializer(typeof(DataProviderSettings), KnownTypes);
                serializer.WriteObject(stream, settings);

                stream.Position = 0;
                var streamReader = new StreamReader(stream);
                serialized += streamReader.ReadToEnd();
            }

            return serialized;
        }

        public void RestoreValue(object context, string savedValue)
        {
            IDataProvider dataProvider = context as IDataProvider;
            if (dataProvider != null)
            {
                var stream = new MemoryStream();
                var tw = new StreamWriter(stream);
                tw.Write(savedValue);
                tw.Flush();
                stream.Position = 0;

                DataContractSerializer serializer = new DataContractSerializer(typeof(DataProviderSettings), KnownTypes);
                var result = serializer.ReadObject(stream);

                dataProvider.Settings.AggregateDescriptions.Clear();
                foreach (var aggregateDescription in (result as DataProviderSettings).Aggregates)
                {
                    dataProvider.Settings.AggregateDescriptions.Add(aggregateDescription);
                }

                dataProvider.Settings.FilterDescriptions.Clear();
                foreach (var filterDescription in (result as DataProviderSettings).Filters)
                {
                    dataProvider.Settings.FilterDescriptions.Add(filterDescription);
                }

                dataProvider.Settings.RowGroupDescriptions.Clear();
                foreach (var rowDescription in (result as DataProviderSettings).Rows)
                {
                    dataProvider.Settings.RowGroupDescriptions.Add(rowDescription);
                }

                dataProvider.Settings.ColumnGroupDescriptions.Clear();
                foreach (var columnDescription in (result as DataProviderSettings).Columns)
                {
                    dataProvider.Settings.ColumnGroupDescriptions.Add(columnDescription);
                }

                dataProvider.Settings.AggregatesPosition = (result as DataProviderSettings).AggregatesPosition;
                dataProvider.Settings.AggregatesLevel = (result as DataProviderSettings).AggregatesLevel;
            }
        }
    }
}
