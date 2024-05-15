using Telerik.Windows.Controls;
using System.Windows;

namespace SpecialSlotsToolTip
{
	public class SpecialSlotStyleSelector : ScheduleViewStyleSelector
	{
		public Style LunchSlotsStyle { get; set; }

		public override Style SelectStyle(object item, DependencyObject container, ViewDefinitionBase activeViewDefinition)
		{
			return this.LunchSlotsStyle;
		}
	}
}
