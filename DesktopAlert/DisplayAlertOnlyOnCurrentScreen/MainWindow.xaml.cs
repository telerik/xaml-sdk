using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Interop;
using Telerik.Windows.Controls;

namespace DisplayAlertOnlyOnCurrentScreen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Dictionary<Screen, RadDesktopAlertManager> desktopAlertManagersDictionary = new Dictionary<Screen, RadDesktopAlertManager>();

        public MainWindow()
        {
            InitializeComponent();
            this.InitializeManagers();
        }

        private void InitializeManagers()
        {
            foreach (var scr in Screen.AllScreens)
            {
                this.desktopAlertManagersDictionary.Add(scr, new RadDesktopAlertManager(AlertScreenPosition.BottomRight, this.CalculateOffset(scr)));
            }
        }

        private Point CalculateOffset(Screen targetScreen)
        {
            var primaryScreen = Screen.PrimaryScreen;

            if (primaryScreen == targetScreen)
            {
                return new Point();
            }

            var offsetX = targetScreen.Bounds.X > 0
               ? targetScreen.Bounds.Width
               : -primaryScreen.Bounds.Width;

            var offsetY = targetScreen.Bounds.Y + ((targetScreen.Bounds.Height - primaryScreen.Bounds.Height));

            return new Point(offsetX, offsetY);
        }


        private void RadButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var manager in this.desktopAlertManagersDictionary.Values)
            {
                manager.ShowAlert(new RadDesktopAlert { Content = "New Alert", Header = "New Alert" });
            }
        }

        private void RadButton_Click_1(object sender, RoutedEventArgs e)
        {
            var handle = new WindowInteropHelper(this).Handle;
            var currentScreen = Screen.FromHandle(handle);
            this.desktopAlertManagersDictionary[currentScreen].ShowAlert(new RadDesktopAlert { Content = "New Alert", Header = "New Alert" });
        }
    }
}
