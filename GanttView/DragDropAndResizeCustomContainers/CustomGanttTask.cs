
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Linq;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GanttView;
using Telerik.Windows.Controls.Scheduling;
using Telerik.Windows.Core;

namespace DragDropAndResizeCustomContainers
{
	public class CustomGanttTask : GanttTask
	{
		
		public CustomGanttTask() : base()
		{

		}
		public CustomGanttTask(DateTime start, DateTime end, string title)
			 : base(start, end, title)
		{

		}

		private Brush _color;

		public Brush Color
		{
			get { return _color; }
			set { _color = value; }
		}
	}
}
