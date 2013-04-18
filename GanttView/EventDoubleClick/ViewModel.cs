using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GanttView;
using Telerik.Windows.Controls.Scheduling;

namespace EventDoubleClick
{
	public class ViewModel : ViewModelBase
	{
		private ObservableCollection<IGanttTask> tasks;
		private DateRange visibleRange;
		private DelegateCommand doubleClickCommand;

		public ViewModel()
		{
			this.tasks = new SoftwarePlanning();
			var start = this.tasks.Min(t => t.Start).Date;
			var end = this.tasks.Max(t => t.End).Date;

			this.visibleRange = new DateRange(start.AddHours(-12), end.AddDays(3));
			this.doubleClickCommand = new DelegateCommand(OnDoubleClickCommandExecuted);
		}

		public ObservableCollection<IGanttTask> GanttTasks
		{
			get
			{
				return this.tasks;
			}
			set
			{
				this.tasks = value;
				this.OnPropertyChanged(() => this.GanttTasks);
			}
		}

		public DateRange VisibleRange
		{
			get
			{
				return this.visibleRange;
			}
			set
			{
				this.visibleRange = value;
				this.OnPropertyChanged(() => this.VisibleRange);
			}
		}

		public DelegateCommand DoubleClickCommand 
		{
			get 
			{
				return this.doubleClickCommand;
			}
			set
			{
				if (this.doubleClickCommand != value)
				{
					this.doubleClickCommand = value;
					OnPropertyChanged(() => this.DoubleClickCommand);
				}
			}
		}

		private void OnDoubleClickCommandExecuted(object task)
		{
			MessageBox.Show(string.Format("DoubleClick on: \n {0}!", ((IGanttTask)task).Title));
		}
	}
}
