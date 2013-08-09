using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ScheduleView;

namespace SpecialSlotsZIndex
{
    public class SpecialSlotStyleSelector : ScheduleViewStyleSelector
    {
		private Style nonworkingHourStyle;
		private Style monthViewStyle;
		private Style readOnlyStyle;

		public Style NonWorkingHourStyle
		{
			get
			{
				return this.nonworkingHourStyle;
			}
			set
			{
				this.nonworkingHourStyle = value;
			}
		}

		public Style MonthViewStyle
		{
			get
			{
				return this.monthViewStyle;
			}
			set
			{
				this.monthViewStyle = value;
			}
		}

		public Style ReadOnlyStyle
		{
			get
			{
				return this.readOnlyStyle;
			}
			set
			{
				this.readOnlyStyle = value;
			}
		}

		public override Style SelectStyle(object item, DependencyObject container, ViewDefinitionBase activeViewDefinition)
		{
			Slot slot = item as Slot;
			if (slot.IsReadOnly)
			{
				return this.ReadOnlyStyle;
			}
			if (activeViewDefinition is MonthViewDefinition)
			{
				return this.MonthViewStyle;
			}
			else
			{
				if (item is NonWorkingSlot)
				{
					return this.NonWorkingHourStyle;
				}
			}
			return base.SelectStyle(item, container, activeViewDefinition);
		}

    }
}
