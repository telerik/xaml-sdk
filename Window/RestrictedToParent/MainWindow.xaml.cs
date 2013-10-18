using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RestrictedToParent
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MyRadWindow MyRadWindow { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            this.MyRadWindow = new MyRadWindow();
            this.MyRadWindow.Owner = this;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (!this.MyRadWindow.IsVisible)
            {
                this.MyRadWindow.Show();
            }
        }

        private void Window_SizeChanged_1(object sender, SizeChangedEventArgs e)
        {
            var primaryScreenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            var primaryScreenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            if (this.HasMultipleMonitors())
            {
                primaryScreenWidth = System.Windows.SystemParameters.PrimaryScreenWidth * 2;
            }

            var window = sender as Window;
            double rightMargin;
            double bottomMargin;
            double topMargin;
            double leftMargin;
            if (window.WindowState == System.Windows.WindowState.Maximized)
            {
                if (this.HasMultipleMonitors())
                {
                    var monitorNumber = this.GetMonitorNumber(window.Left);
                    if (monitorNumber > 1)
                    {
                        rightMargin = 0;
                        leftMargin = System.Windows.SystemParameters.PrimaryScreenWidth;
                    }
                    else
                    {
                        rightMargin = System.Windows.SystemParameters.PrimaryScreenWidth;
                        leftMargin = 0;
                    }
                    bottomMargin = 0;
                    topMargin = 0;
                }
                else
                {
                    rightMargin = primaryScreenWidth - System.Windows.SystemParameters.PrimaryScreenWidth;
                    bottomMargin = primaryScreenHeight - System.Windows.SystemParameters.PrimaryScreenHeight;
                    topMargin = 0;
                    leftMargin = 0;
                }
            }
            else
            {
                rightMargin = primaryScreenWidth - (window.Left + window.Width);
                bottomMargin = primaryScreenHeight - (window.Top + window.Height);
                topMargin = window.Top;
                leftMargin = window.Left;
            }

            if (window != null)
            {
                this.MyRadWindow.RestrictedAreaMargin = new Thickness(leftMargin, topMargin, rightMargin, bottomMargin);
            }
        }

        private int GetMonitorNumber(double monitorLeftCoordinate)
        {
            var primaryScreenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            var result = 1;
            while (primaryScreenWidth < monitorLeftCoordinate)
            {
                result++;
                primaryScreenWidth = primaryScreenWidth * 2;
            }

            return result;
        }

        private bool HasMultipleMonitors()
        {
            Screen[] screensArray = Screen.AllScreens;
            List<Screen> screensList = screensArray.OfType<Screen>().ToList();
            if (screensList.Count > 1)
            {
                return true;
            }

            return false;
        }

        private void Window_LocationChanged_1(object sender, EventArgs e)
        {
            var primaryScreenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;

            if (this.HasMultipleMonitors())
            {
                primaryScreenWidth = System.Windows.SystemParameters.PrimaryScreenWidth * 2;
            }

            var primaryScreenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            var window = sender as Window;
            double rightMargin = primaryScreenWidth - (window.Left + window.Width);
            double bottomMargin = primaryScreenHeight - (window.Top + window.Height);

            if (window != null)
            {
                this.MyRadWindow.RestrictedAreaMargin = new Thickness(window.Left, window.Top, rightMargin, bottomMargin);
            }
        }
    }
}
