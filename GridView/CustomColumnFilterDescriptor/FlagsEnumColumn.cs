using System;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace CustomColumnFilterDescriptor
{
	/// <summary>
	/// A special column handling flags enum properties.
	/// </summary>
	public class FlagsEnumColumn<T> : GridViewDataColumn
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="FlagsEnumColumn" /> class.
		/// </summary>
		public FlagsEnumColumn() : base()
		{
			if (!typeof(T).IsEnum)
			{
				throw new ArgumentException("Generic Type must be a System.Enum");
			}

			// We hide the lower part of the filtering control since we will be using distinct values only, i.e. the upper part.
			// This is not applicable for FilterRow mode which does not have distinct values.
			this.ShowFieldFilters = false;		
		}

		/// <summary>
		/// Creates the column filter descriptor.
		/// </summary>
		/// <returns></returns>
		public override IColumnFilterDescriptor CreateColumnFilterDescriptor()
		{
			// Here we return our special column filter descriptor.
			return new FlagsEnumColumnFilterDescriptor<T>(this);
		}
	}
}
