using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Windows.Controls;
using System.Collections;
using System.Windows.Media;
using System.Collections.ObjectModel;
using Telerik.Windows.Controls.Scheduling;

namespace TasksWithDifferentColors
{

	public class ViewModel : ViewModelBase
	{
		private static readonly Brush redBrush = new SolidColorBrush(Colors.Red);
		private static readonly Brush greenBrush = new SolidColorBrush(Colors.Green);
		private static readonly Brush blueBrush = new SolidColorBrush(Colors.Blue);

		private IDateRange visibleRange;

		/// <Summary>Gets or sets VisibleRange and notifies for changes</Summary>
		public IDateRange VisibleRange
		{
			get { return this.visibleRange; }
			set
			{
				if (this.visibleRange != value)
				{
					this.visibleRange = value;
					this.OnPropertyChanged(() => this.VisibleRange);
				}
			}
		}

		private IEnumerable tasks;

		/// <Summary>Gets or sets Tasks and notifies for changes</Summary>
		public IEnumerable Tasks
		{
			get { return this.tasks; }
			set
			{
				if (this.tasks != value)
				{
					this.tasks = value;
					this.OnPropertyChanged(() => this.Tasks);
				}
			}
		}

		public ViewModel()
		{
			var start = DateTime.Today;
			this.Tasks = new ObservableCollection<CustomTask>
			{
				new CustomTask { Title = "Red Task", Start = start, End = start.AddDays(1), Background = redBrush },
				new CustomTask { Title = "Green Task", Start = start.AddDays(2), End = start.AddDays(3), Background = greenBrush },
				new CustomTask { Title = "Blue Task", Start = start.AddDays(1), End = start.AddDays(6), Background = blueBrush }
			};
			this.VisibleRange = new DateRange(start, start.AddDays(7));
		}
	}
}
