using System.Collections.Generic;
using System.Linq;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GanttView;
using Telerik.Windows.Controls.ScheduleView;

namespace DragDropFromExternalSource
{
	public class CustomScheduleViewConverter : IDataObjectProvider
	{
		public object GetData(string type, bool autoConvert, IEnumerable<IOccurrence> draggedAppointments)
		{
			if (type == typeof(Project).FullName)
			{
				return draggedAppointments.Select(this.CreateProjectForOccurence).ToList();
			}
			else if (type == typeof(IDateRange).FullName)
			{
				return draggedAppointments.Select(this.CreateGanttTaskForOccurence);
			}

			return null;
		}

		public string[] GetFormats()
		{
			return new[] { typeof(Project).FullName, typeof(ScheduleViewDragDropPayload).FullName };
		}

		private IDateRange CreateGanttTaskForOccurence(IOccurrence arg)
		{
			var app = arg as IAppointment;
			if (app != null)
			{
				return new GanttTask { Title = app.Subject, Start = app.Start, End = app.End };
			}

			return null;
		}

		private Project CreateProjectForOccurence(IOccurrence arg)
		{
			var app = arg as IAppointment;
			if (app != null)
			{
				return new Project { Name = app.Subject, Start = app.Start, End = app.End };
			}

			return null;
		}
	}
}
