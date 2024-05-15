using System.Windows;
using System.Windows.Controls;

namespace TransitionWithoutBindings
{
	public partial class Example : UserControl
	{
		private ScheduleView ScheduleViewUserControl { get; set; }

		private Docking DockingUserControl { get; set; }

		public Example()
		{
			InitializeComponent();
			this.ScheduleViewUserControl = new ScheduleView();
			this.DockingUserControl = new Docking();
			this.RadTransitionControl.Content = ScheduleViewUserControl;
			this.ShowScheduleViewUserControlButton.IsEnabled = false;
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			this.RadTransitionControl.Content = ScheduleViewUserControl;
			this.ShowScheduleViewUserControlButton.IsEnabled = false;
			this.ShowDockingUserControlButton.IsEnabled = true;
		}

		private void Button_Click_2(object sender, RoutedEventArgs e)
		{
			this.RadTransitionControl.Content = this.DockingUserControl;
			this.ShowDockingUserControlButton.IsEnabled = false;
			this.ShowScheduleViewUserControlButton.IsEnabled = true;
		}
	}
}
