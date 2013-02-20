using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Data;
using System.Windows;

namespace FilteringCollectionProperties
{
	/// <summary>
	/// A special column handling flags enum properties.
	/// </summary>
	public class CollectionPropertyColumn : GridViewDataColumn
	{
		public CollectionPropertyColumn() : base()
		{
			this.ShowDistinctFilters = false;

			// I know that it is List<string>, but let the filtering UI
			// think that it is filtering simple strings, since we will
			// do the filtering ourselves anyway.
			this.FilterMemberType = typeof(string);
		}

		public override bool CanFilter()
		{
			return true; // since we will handle the filtering ourselves.
		}
		
		/// <summary>
		/// Creates the column filter descriptor.
		/// </summary>
		/// <returns></returns>
		public override IColumnFilterDescriptor CreateColumnFilterDescriptor()
		{
			// Here we return our special column filter descriptor.
			return new CollectionPropertyColumnFilterDescriptor(this);
		}

		public override FrameworkElement CreateCellElement(GridViewCell cell, object dataItem)
		{
			StringBuilder sb = new StringBuilder();
			foreach (string workingDay in ((Person)dataItem).WorkingDays)
			{
				sb.AppendFormat("{0}, ", workingDay);
			}

			if (sb.Length > 0)
			{
				sb.Remove(sb.Length - 2, 2);
			}

			TextBlock textBlock = (TextBlock)base.CreateCellElement(cell, dataItem);

			textBlock.Text = sb.ToString();

			return textBlock;	
		}


	}
}
