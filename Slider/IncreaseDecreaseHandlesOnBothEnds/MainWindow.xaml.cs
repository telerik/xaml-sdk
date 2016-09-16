using System.Windows;
using System.Windows.Controls.Primitives;

namespace WPFLayout
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void OnIncreaseSelectionButtonClick(object sender, RoutedEventArgs e)
		{
			var button = (RepeatButton)sender;
			var tag = button.Tag.ToString();
			UpdateSelection(tag, this.slider.LargeChange);
		}

		private void OnDecreaseSelectionButtonClick(object sender, RoutedEventArgs e)
		{
			var button = (RepeatButton)sender;
			var tag = button.Tag.ToString();
			UpdateSelection(tag, -this.slider.LargeChange);
		}

		private void UpdateSelection(string handleTag, double selectionChangeStep)
		{
			if (handleTag.Equals("Start"))
			{
				var oldStart = this.slider.SelectionStart;
				var newStart = oldStart + selectionChangeStep;
				this.slider.SelectionStart = newStart;
			}
			else if (handleTag.Equals("End"))
			{
				var oldEnd = this.slider.SelectionEnd;
				var newEnd = oldEnd + selectionChangeStep;
				this.slider.SelectionEnd = newEnd;
			}
		}
	}
}
