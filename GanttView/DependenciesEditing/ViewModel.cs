using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GanttView;
using Telerik.Windows.Controls.Scheduling;

namespace DependenciesEditing
{
	public class ViewModel : ViewModelBase
	{
		private ObservableCollection<GanttTask> _Tasks;

		private VisibleRange _VisibleRange;

		public ViewModel()
		{
			this.Tasks = new ObservableCollection<GanttTask>();

			this.Tasks.Add(new GanttTask { Start = DateTime.Today.AddHours(1), End = DateTime.Today.AddHours(4), Title = "Task 1" });
			this.Tasks.Add(new GanttTask { Start = DateTime.Today.AddHours(2), End = DateTime.Today.AddHours(3), Title = "Task 2" });
			this.Tasks.Add(new GanttTask { Start = DateTime.Today.AddHours(3), End = DateTime.Today.AddHours(4), Title = "Task 3" });
			this.Tasks.Add(new GanttTask { Start = DateTime.Today.AddHours(4), End = DateTime.Today.AddHours(5), Title = "Task 4" });
			this.Tasks.Add(new GanttTask { Start = DateTime.Today.AddHours(5), End = DateTime.Today.AddHours(6), Title = "Task 5" });
			this.Tasks.Add(new GanttTask { Start = DateTime.Today.AddHours(6), End = DateTime.Today.AddHours(9), Title = "Task 6" });
			this.Tasks.Add(new GanttTask { Start = DateTime.Today.AddHours(6), End = DateTime.Today.AddHours(11), Title = "Task 7" });
			this.Tasks.Add(new GanttTask { Start = DateTime.Today.AddHours(7), End = DateTime.Today.AddHours(13), Title = "Task 8" });

			this.Tasks[0].AddDependency(this.Tasks[1], DependencyType.FinishStart);
			this.Tasks[1].AddDependency(this.Tasks[0], DependencyType.FinishStart);
			this.Tasks[2].AddDependency(this.Tasks[0], DependencyType.FinishStart);
			this.Tasks[3].AddDependency(this.Tasks[2], DependencyType.FinishStart);
			this.Tasks[3].AddDependency(this.Tasks[0], DependencyType.FinishStart);
			this.Tasks[5].AddDependency(this.Tasks[4], DependencyType.FinishStart);

            this.VisibleRange = new VisibleRange(DateTime.Today.AddHours(-1), DateTime.Today.AddHours(14));
		}

		public VisibleRange VisibleRange
		{
			get { return this._VisibleRange; }
			set
			{
				if (this._VisibleRange != value)
				{
					this._VisibleRange = value;
					this.OnPropertyChanged(() => this.VisibleRange);
				}
			}
		}

		public ObservableCollection<GanttTask> Tasks
		{
			get { return this._Tasks; }
			set
			{
				if (this._Tasks != value)
				{
					this._Tasks = value;
					this.OnPropertyChanged(() => this.Tasks);
				}
			}
		}

        private GanttTask _selectedTask;

        public GanttTask SelectedTask
        {
            get 
            { 
                return _selectedTask; 
            }
            set 
            {
                if (this._selectedTask != value)
                {
                    this._selectedTask = value;
                    this.OnPropertyChanged(() => this.SelectedTask);
                }
            }
        }
	}
}
