using System;
using Telerik.Windows.Controls.GanttView;

namespace Baseline
{
	public class GanttBaselineTask : GanttTask
	{
		private DateTime startPlannedDate;

		private DateTime endPlannedDate;

		public DateTime StartPlannedDate
		{
			get
			{
				return this.startPlannedDate;
			}

			set
			{
				if (this.startPlannedDate != value)
				{
					this.startPlannedDate = value;
					this.OnPropertyChanged(() => this.StartPlannedDate);
				}
			}
		}

		public DateTime EndPlannedDate
		{
			get
			{
				return this.endPlannedDate;
			}

			set
			{
				if (this.endPlannedDate != value)
				{
					this.endPlannedDate = value;
					this.OnPropertyChanged(() => this.EndPlannedDate);
				}
			}
		}
	}
}
