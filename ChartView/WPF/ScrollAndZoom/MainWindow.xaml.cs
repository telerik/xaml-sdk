using System.Windows;

namespace ScrollAndZoom
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

            this.KeyDown += this.MainWindow_KeyDown;
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
