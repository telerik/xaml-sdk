using System.Collections.Generic;
using System.Linq;
using Telerik.Windows.Controls.GanttView;
using Telerik.Windows.Controls.ScheduleView;
using Telerik.Windows.Controls.Scheduling;
using Telerik.Windows.DragDrop.Behaviors;

namespace DragDropFromExternalSource
{
	public class CustomScheduleViewDragDropBehavior : Telerik.Windows.Controls.ScheduleViewDragDropBehavior
	{
		public override IEnumerable<IOccurrence> ConvertDraggedData(object data)
		{
			var payload = DataObjectHelper.GetData(data, typeof(SchedulingDragOperationPayload), true) as SchedulingDragOperationPayload;
			if (payload != null)
			{
				return payload.DraggedItems.OfType<IGanttTask>().Select(p => new Appointment { Subject = p.Title, Start = p.Start, End = p.End }).ToList<IOccurrence>();
			}
			else
			{
				var project = ((List<object>)DataObjectHelper.GetData(data, typeof(Project), true)).FirstOrDefault() as Project;
				if (project != null)
				{
					return new List<IOccurrence> { new Appointment { Subject = project.Name, Start = project.Start, End = project.End } };
				}
			}

			return base.ConvertDraggedData(data);
		}

		public override bool CanStartDrag(Telerik.Windows.Controls.DragDropState state)
		{
			return state.DraggedAppointments.Count() < 2;
		}
	}
}
