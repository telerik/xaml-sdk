using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls.Gauge;

namespace DataBinding
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			ObservableCollection<double> values = new ObservableCollection<double>()
			{ 
				10,
				15,
				25,
				17,
				40,
				50,
				60,
				70,
				25,
				15,
				5,
				10,
				12,
				18,
				29,
				37,
				92,
			};
			this.radialBar.ValueSource = values;

			List<PlaybackData> values2 = new List<PlaybackData>
			{
				new PlaybackData() { Value = 10, Duration = TimeSpan.FromMilliseconds(500.0)},
				new PlaybackData() { Value = 15, Duration = TimeSpan.FromMilliseconds(1000.0)},
				new PlaybackData() { Value = 25, Duration = TimeSpan.FromMilliseconds(250.0)},
				new PlaybackData() { Value = 17, Duration = TimeSpan.FromMilliseconds(250.0)},
				new PlaybackData() { Value = 40, Duration = TimeSpan.FromMilliseconds(250.0)},
				new PlaybackData() { Value = 50, Duration = TimeSpan.FromMilliseconds(250.0)},
				new PlaybackData() { Value = 60, Duration = TimeSpan.FromMilliseconds(500.0)},
				new PlaybackData() { Value = 70, Duration = TimeSpan.FromMilliseconds(125.0)},
				new PlaybackData() { Value = 25, Duration = TimeSpan.FromMilliseconds(125.0)},
				new PlaybackData() { Value = 15, Duration = TimeSpan.FromMilliseconds(500.0)},
				new PlaybackData() { Value = 5, Duration = TimeSpan.FromMilliseconds(1000.0)},
				new PlaybackData() { Value = 10, Duration = TimeSpan.FromMilliseconds(500.0)},
				new PlaybackData() { Value = 12, Duration = TimeSpan.FromMilliseconds(500.0)},
				new PlaybackData() { Value = 18, Duration = TimeSpan.FromMilliseconds(250.0)},
				new PlaybackData() { Value = 29, Duration = TimeSpan.FromMilliseconds(250.0)},
				new PlaybackData() { Value = 37, Duration = TimeSpan.FromMilliseconds(500.0)},
				new PlaybackData() { Value = 92, Duration = TimeSpan.FromMilliseconds(500.0)},
			};
			this.radialBar2.ValueSource = values2;
		}

		private void StartPlayback()
		{
			this.radialBar.StartPlayback();
			this.radialBar2.StartPlayback();
		}

		private void StopPlayback()
		{
			this.radialBar.StopPlayback();
			this.radialBar2.StopPlayback();
		}

		private void MoveNext()
		{
			this.radialBar.MoveNext();
			this.radialBar2.MoveNext();
		}

		private void MovePrevious()
		{
			this.radialBar.MovePrevious();
			this.radialBar2.MovePrevious();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			Button button = sender as Button;
			switch (button.Tag as string)
			{
				case "StartPlayback": this.StartPlayback(); break;
				case "StopPlayback": this.StopPlayback(); break;
				case "MoveNext": this.MoveNext(); break;
				case "MovePrevious": this.MovePrevious(); break;
			}
		}
	}
}
