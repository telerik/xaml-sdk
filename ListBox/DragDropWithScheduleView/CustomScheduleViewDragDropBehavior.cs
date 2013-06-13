using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ScheduleView;
using Telerik.Windows.DragDrop.Behaviors;

namespace DragDropWithScheduleView
{
	public class CustomScheduleViewDragDropBehavior : ScheduleViewDragDropBehavior
	{
		public override IEnumerable<IOccurrence> ConvertDraggedData(object data)
		{
			if (DataObjectHelper.GetDataPresent(data, typeof(Customer), false))
			{
				var customers = DataObjectHelper.GetData(data, typeof(Customer), true) as IEnumerable;
				if (customers != null)
				{
					var newApp = customers.OfType<Customer>().Select(c => new Appointment { Subject = c.Name });
					return newApp;
				}
			}

			return base.ConvertDraggedData(data);
		}
	}
}
