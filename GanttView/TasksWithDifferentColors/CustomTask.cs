using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Windows.Controls.GanttView;
using System.Windows.Media;

namespace TasksWithDifferentColors
{

	public class CustomTask : GanttTask
	{
		private Brush background;

		/// <Summary>Gets or sets Background and notifies for changes</Summary>
		public Brush Background
		{
			get { return this.background; }
			set
			{
				if (this.background != value)
				{
					this.background = value;
					this.OnPropertyChanged(() => this.Background);
				}
			}
		}
	}
}
