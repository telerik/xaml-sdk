using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.Windows.Controls.GanttView;

namespace TaskDeadline
{
	public class GanttDeadlineTask : GanttTask
	{
		private DateTime? ganttDeadLine;

		public DateTime? GanttDeadLine
		{
			get
			{
				return this.ganttDeadLine;
			}
			set
			{
				if (this.ganttDeadLine != value)
				{
					this.ganttDeadLine = value;
					this.OnPropertyChanged(() => this.GanttDeadLine);
				}
			}
		}
	}
}
