using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using Telerik.Windows.Controls;

namespace DesktopAlertWithScreenPositionOffset
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public RadDesktopAlertManager Manager { get; set; }
        public RadDesktopAlert Alert { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            this.SizeChanged += MainWindow_SizeChanged;
            this.LocationChanged += MainWindow_LocationChanged;
        }

        private void MainWindow_LocationChanged(object sender, EventArgs e)
        {
            this.CreateAlert("LOCATION CHANGED", "The Location has been changed");
            this.AlertPositioning();
        }

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.CreateAlert("SIZE CHANGED", "The Size has been changed");
            this.AlertPositioning();
        }

        private void RadButton_Click(object sender, RoutedEventArgs e)
        {
            this.Manager.ShowAlert(this.Alert);
            this.CreateAlert("Common Alert", "A new Alert has been visualized!");
        }

        private void AlertPositioning()
        {
            if (this.Manager != null)
            {
                this.Manager.CloseAllAlerts();
            }

            var workingArea = System.Windows.SystemParameters.WorkArea;
            if (this.WindowState != System.Windows.WindowState.Maximized)
            {
               this.Manager = new RadDesktopAlertManager(AlertScreenPosition.BottomRight, new Point(
                    -(this.Left + this.Width + workingArea.Width - 2 * (this.Left + this.Width)),
                    -(this.Top + this.Height + workingArea.Height - 2 * (this.Top + this.Height))));
            }
            else
            {
                var rect = GetWindowRectangle();

                this.Manager = new RadDesktopAlertManager(AlertScreenPosition.BottomRight, new Point(rect.Right - workingArea.BottomRight.X, 0));
            }
        }

        private void CreateAlert(string header, string content)
        {
            this.Alert = new RadDesktopAlert();
            this.Alert.Header = header;
            this.Alert.Content = content;
            this.Alert.ShowDuration = 2500;
        }

        private System.Drawing.Rectangle GetWindowRectangle()
        {
            System.Drawing.Rectangle windowRectangle;

            if (this.WindowState == System.Windows.WindowState.Maximized)
            {
                windowRectangle = System.Windows.Forms.Screen.GetWorkingArea(
                    new System.Drawing.Point((int)this.Left, (int)this.Top));
            }
            else
            {
                windowRectangle = new System.Drawing.Rectangle(
                    (int)this.Left, (int)this.Top,
                    (int)this.ActualWidth, (int)this.ActualHeight);
            }

            return windowRectangle;
        }
    }
}
