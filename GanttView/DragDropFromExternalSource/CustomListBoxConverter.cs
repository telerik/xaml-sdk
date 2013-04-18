using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GanttView;
using Telerik.Windows.Controls.ScheduleView;
using Telerik.Windows.DragDrop.Behaviors;

namespace DragDropFromExternalSource
{
	public class CustomListBoxConverter : DataConverter
	{
		public override object ConvertTo(object data, string format)
		{
			if (format == typeof(IDateRange).FullName)
			{
				var draggedProjectItem = (DataObjectHelper.GetData(data, typeof(Project), true) as List<object>).First() as Project;
				var task = new GanttTask { Title = draggedProjectItem.Name, Start = draggedProjectItem.Start, End = draggedProjectItem.End };

				return task;
			}
			else if (DataObjectHelper.GetDataPresent(data, typeof(ScheduleViewDragDropPayload), false))
			{
				ScheduleViewDragDropPayload payload = (ScheduleViewDragDropPayload)DataObjectHelper.GetData(data, typeof(ScheduleViewDragDropPayload), false);
				if (payload != null)
				{
					return payload.DraggedAppointments;
				}
			}
			else if (format == typeof(ScheduleViewDragDropPayload).FullName)
			{
				var customers = DataObjectHelper.GetData(data, typeof(Project), true) as IEnumerable;
				if (customers != null)
				{
					return customers.OfType<Project>().Select(c => new Appointment { Subject = c.Name }).ToList();
				}
			}

			return null;
		}

		public override string[] GetConvertToFormats()
		{
			return new string[] { typeof(IDateRange).FullName, typeof(IOccurrence).FullName, typeof(ScheduleViewDragDropPayload).FullName };
		}
	}
}
