using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GanttView;
using Telerik.Windows.Controls.Scheduling;

namespace PrintingAndExportingAdvanced
{
	public class PrintingAndExportingViewModel : ViewModelBase
	{
		private ObservableCollection<GanttTask> tasks;

		private DateRange visibleTime;

		private ObservableCollection<GanttArea> ganttAreas;

		private GanttArea selectedArea;

		private bool showHeaders;

		private Action<ImageExportSettings> export;

		public PrintingAndExportingViewModel(Action<ImageExportSettings> export)
		{
			this.export = export;
			var date = DateTime.Now;
			this.tasks = GetTasks(date);
			this.visibleTime = new DateRange(date.AddDays(-1), date.AddDays(11));
			this.ganttAreas = new ObservableCollection<GanttArea>()
			{
				GanttArea.AllAreas,
				GanttArea.GridViewArea,
				GanttArea.TimeLineArea
			};
			this.selectedArea = this.ganttAreas[0];
			this.showHeaders = true;
			this.ExportCommand = new DelegateCommand(ExportCommandExecute);
		}

		private void ExportCommandExecute(object obj)
		{
			var settings = new ImageExportSettings(
				new Size(815, 1023), // or 408, 600
				this.showHeaders,
				this.selectedArea);
			this.export(settings);
		}

		public bool ShowHeaders
		{
			get
			{
				return this.showHeaders;
			}

			set
			{
				if (this.showHeaders != value)
				{
					this.showHeaders = value;
					this.OnPropertyChanged(() => this.ShowHeaders);
				}
			}
		}

		public GanttArea SelectedArea
		{
			get
			{
				return this.selectedArea;
			}

			set
			{
				if (this.selectedArea != value)
				{
					this.selectedArea = value;
					this.OnPropertyChanged(() => this.SelectedArea);
				}
			}
		}

		public ObservableCollection<GanttTask> Tasks
		{
			get
			{
				return tasks;
			}

			set
			{
				tasks = value;
				OnPropertyChanged(() => Tasks);
			}
		}

		public DateRange VisibleTime
		{
			get
			{
				return this.visibleTime;
			}

			set
			{
				if (this.visibleTime != value)
				{
					this.visibleTime = value;
					this.OnPropertyChanged(() => this.VisibleTime);
				}
			}
		}

		public ObservableCollection<GanttArea> GanttAreas
		{
			get
			{
				return this.ganttAreas;
			}

			set
			{
				if (this.ganttAreas != value)
				{
					this.ganttAreas = value;
					this.OnPropertyChanged(() => this.GanttAreas);
				}
			}
		}

		private ObservableCollection<GanttTask> GetTasks(DateTime date)
		{
			var ganttAPI = new GanttTask()
			{
				Start = date,
				End = date.AddDays(2),
				Title = "Design public API",
				Description = "Description: Design public API",
			};
			var ganttRendering = new GanttTask()
			{
				Start = date.AddDays(2).AddHours(8),
				End = date.AddDays(4),
				Title = "Gantt Rendering",
				Description = "Description: Gantt Rendering",
			};
			var ganttDemos = new GanttTask()
			{
				Start = date.AddDays(4.5),
				End = date.AddDays(7),
				Title = "Gantt Demos",
				Description = "Description: Gantt Demos",
			};
			var milestone = new GanttTask()
			{
				Start = date.AddDays(7),
				End = date.AddDays(7).AddHours(1),
				Title = "Review",
				Description = "Description: Review",
				IsMilestone = true,
			};

			ganttRendering.Dependencies.Add(new Dependency() { FromTask = ganttAPI });
			ganttDemos.Dependencies.Add(new Dependency() { FromTask = ganttRendering });

			var iterationTask = new GanttTask()
			{
				Start = date,
				End = date.AddDays(7),
				Title = "Iteration 1",
				Children = { ganttAPI, ganttRendering, ganttDemos, milestone },
			};

			ObservableCollection<GanttTask> tasks = new ObservableCollection<GanttTask>() { iterationTask };

			return tasks;
		}

		public ICommand ExportCommand
		{
			get;
			private set;
		}
	}
}
