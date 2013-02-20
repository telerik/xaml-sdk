using System.Linq;
using Telerik.Windows.Controls.GanttView;
using Telerik.Windows.Controls.ScheduleView;
using Telerik.Windows.Controls.Scheduling;
using Telerik.Windows.DragDrop.Behaviors;

namespace DragDropFromExternalSource
{
	public class CustomGanttConverter : DataConverter
	{
		public override object ConvertTo(object data, string format)
		{
			if (format == typeof(Project).FullName)
			{
				var project = DataObjectHelper.GetData(data, typeof(SchedulingDragOperationPayload), true) as SchedulingDragOperationPayload;
				if (project != null)
				{
					return project.DraggedItems.OfType<IGanttTask>().Select(p => new Project { Name = p.Title, Start = p.Start, End = p.End });
				}
			}

			return null;
		}

		public override string[] GetConvertToFormats()
		{
			return new[] { typeof(ScheduleViewDragDropPayload).FullName, typeof(Project).FullName };
		}
	}
}
