using System.Collections.Generic;
using System.Linq;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Persistence.Services;
using Telerik.Windows.Data;
using System.Diagnostics;

namespace GridViewSerialization
{
    public class GridViewCustomPropertyProvider : ICustomPropertyProvider
    {
        public CustomPropertyInfo[] GetCustomProperties()
        {
            // Create three custom properties to persist the Columns, Sorting and Group descriptors using proxy objects
            return new CustomPropertyInfo[]
            {
                new CustomPropertyInfo("Columns", typeof(List<ColumnProxy>)),
                new CustomPropertyInfo("SortDescriptors", typeof(List<SortDescriptorProxy>)),
                new CustomPropertyInfo("GroupDescriptors", typeof(List<GroupDescriptorProxy>)),
                new CustomPropertyInfo("FilterDescriptors", typeof(List<FilterSetting>)),
            };
        }

        public void InitializeObject(object context)
        {
            if (context is RadGridView)
            {
                RadGridView gridView = context as RadGridView;
                gridView.SortDescriptors.Clear();
                gridView.GroupDescriptors.Clear();
				gridView.Columns
					.OfType<GridViewColumn>()
					.Where(c => c.ColumnFilterDescriptor.IsActive)
					.ToList().ForEach(c => c.ClearFilters());
            }
        }

        public object InitializeValue(CustomPropertyInfo customPropertyInfo, object context)
        {
            return null;
        }

        public object ProvideValue(CustomPropertyInfo customPropertyInfo, object context)
        {
            RadGridView gridView = context as RadGridView;

			switch (customPropertyInfo.Name)
			{
				case "Columns":
					{
						List<ColumnProxy> columnProxies = new List<ColumnProxy>();

						foreach (GridViewColumn column in gridView.Columns)
						{
							columnProxies.Add(new ColumnProxy()
							{
								UniqueName = column.UniqueName,
								Header = column.Header.ToString(),
								DisplayOrder = column.DisplayIndex,
								Width = column.Width,
							});
						}

						return columnProxies;
					}

				case "SortDescriptors":
					{
						List<SortDescriptorProxy> sortDescriptorProxies = new List<SortDescriptorProxy>();

						foreach (ColumnSortDescriptor descriptor in gridView.SortDescriptors)
						{
							sortDescriptorProxies.Add(new SortDescriptorProxy()
							{
								ColumnUniqueName = descriptor.Column.UniqueName,
								SortDirection = descriptor.SortDirection,
							});
						}

						return sortDescriptorProxies;
					}

				case "GroupDescriptors":
					{
						List<GroupDescriptorProxy> groupDescriptorProxies = new List<GroupDescriptorProxy>();

						foreach (ColumnGroupDescriptor descriotor in gridView.GroupDescriptors)
						{
							groupDescriptorProxies.Add(new GroupDescriptorProxy()
							{
								ColumnUniqueName = descriotor.Column.UniqueName,
								SortDirection = descriotor.SortDirection,
							});
						}

						return groupDescriptorProxies;
					}

				case "FilterDescriptors":
					{
						List<FilterSetting> filterSettings = new List<FilterSetting>();

						foreach (IColumnFilterDescriptor columnFilter in gridView.FilterDescriptors)
						{
							FilterSetting columnFilterSetting = new FilterSetting();

							columnFilterSetting.ColumnUniqueName = columnFilter.Column.UniqueName;

							columnFilterSetting.SelectedDistinctValues.AddRange(columnFilter.DistinctFilter.DistinctValues);

							if (columnFilter.FieldFilter.Filter1.IsActive)
							{
								columnFilterSetting.Filter1 = new FilterDescriptorProxy();
								columnFilterSetting.Filter1.Operator = columnFilter.FieldFilter.Filter1.Operator;
								columnFilterSetting.Filter1.Value = columnFilter.FieldFilter.Filter1.Value;
								columnFilterSetting.Filter1.IsCaseSensitive = columnFilter.FieldFilter.Filter1.IsCaseSensitive;
							}

							columnFilterSetting.FieldFilterLogicalOperator = columnFilter.FieldFilter.LogicalOperator;

							if (columnFilter.FieldFilter.Filter2.IsActive)
							{
								columnFilterSetting.Filter2 = new FilterDescriptorProxy();
								columnFilterSetting.Filter2.Operator = columnFilter.FieldFilter.Filter2.Operator;
								columnFilterSetting.Filter2.Value = columnFilter.FieldFilter.Filter2.Value;
								columnFilterSetting.Filter2.IsCaseSensitive = columnFilter.FieldFilter.Filter2.IsCaseSensitive;
							}

							filterSettings.Add(columnFilterSetting);
						}

						return filterSettings;
					}
			}

			return null;
        }

        public void RestoreValue(CustomPropertyInfo customPropertyInfo, object context, object value)
        {
            RadGridView gridView = context as RadGridView;

			switch (customPropertyInfo.Name)
			{
				case "Columns":
					{
						List<ColumnProxy> columnProxies = value as List<ColumnProxy>;

						foreach (ColumnProxy proxy in columnProxies)
						{
							GridViewColumn column = gridView.Columns[proxy.UniqueName];
							column.DisplayIndex = proxy.DisplayOrder;
							column.Header = proxy.Header;
							column.Width = proxy.Width;
						}
					}
					break;

				case "SortDescriptors":
					{
						gridView.SortDescriptors.SuspendNotifications();

						gridView.SortDescriptors.Clear();

						List<SortDescriptorProxy> sortDescriptoProxies = value as List<SortDescriptorProxy>;

						foreach (SortDescriptorProxy proxy in sortDescriptoProxies)
						{
							GridViewColumn column = gridView.Columns[proxy.ColumnUniqueName];
							gridView.SortDescriptors.Add(new ColumnSortDescriptor() { Column = column, SortDirection = proxy.SortDirection });
						}

						gridView.SortDescriptors.ResumeNotifications();
					}
					break;

				case "GroupDescriptors":
					{
						gridView.GroupDescriptors.SuspendNotifications();

						gridView.GroupDescriptors.Clear();

						List<GroupDescriptorProxy> groupDescriptorProxies = value as List<GroupDescriptorProxy>;

						foreach (GroupDescriptorProxy proxy in groupDescriptorProxies)
						{
							GridViewColumn column = gridView.Columns[proxy.ColumnUniqueName];
							gridView.GroupDescriptors.Add(new ColumnGroupDescriptor() { Column = column, SortDirection = proxy.SortDirection });
						}

						gridView.GroupDescriptors.ResumeNotifications();
					}
					break;

				case "FilterDescriptors":
					{
						gridView.FilterDescriptors.SuspendNotifications();

						foreach (var c in gridView.Columns)
						{
							if (c.ColumnFilterDescriptor.IsActive)
							{
								c.ClearFilters();
							}
						}

						List<FilterSetting> filterSettings = value as List<FilterSetting>;

						foreach (FilterSetting setting in filterSettings)
						{
							Telerik.Windows.Controls.GridViewColumn column = gridView.Columns[setting.ColumnUniqueName];

							Telerik.Windows.Controls.GridView.IColumnFilterDescriptor columnFilter = column.ColumnFilterDescriptor;

							foreach (object distinctValue in setting.SelectedDistinctValues)
							{
								columnFilter.DistinctFilter.AddDistinctValue(distinctValue);
							}

							if (setting.Filter1 != null)
							{
								columnFilter.FieldFilter.Filter1.Operator = setting.Filter1.Operator;
								columnFilter.FieldFilter.Filter1.Value = setting.Filter1.Value;
								columnFilter.FieldFilter.Filter1.IsCaseSensitive = setting.Filter1.IsCaseSensitive;
							}

							columnFilter.FieldFilter.LogicalOperator = setting.FieldFilterLogicalOperator;

							if (setting.Filter2 != null)
							{
								columnFilter.FieldFilter.Filter2.Operator = setting.Filter2.Operator;
								columnFilter.FieldFilter.Filter2.Value = setting.Filter2.Value;
								columnFilter.FieldFilter.Filter2.IsCaseSensitive = setting.Filter2.IsCaseSensitive;
							}
						}

						gridView.FilterDescriptors.ResumeNotifications();
					}
					break;
			}
        }
    }
}
