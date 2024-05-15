using System.Windows;

namespace ScrollAndZoom
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

        private void RadCartesianChart_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Application.Current.MainWindow.KeyDown += this.MainWindow_KeyDown;
        }

        private void RadCartesianChart_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Application.Current.MainWindow.KeyDown -= this.MainWindow_KeyDown;
        }

        private void MainWindow_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Escape)
            {
                this.panZoomBehavior.CancelDragToZoom();
            }
        }
	}
}
