using System.Collections.Generic;
using Telerik.Windows.Controls.GanttView;

namespace CustomGanttTaskWithStatus
{
	public class MyGanttTask : GanttTask
	{
		public List<string> StatusList
		{
			get 
			{
				return new List<string>() { "Not Done", "In progress", "Done" };
			}
		}

		public string Status { get; set; }
	}
}
