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
            this.MyRadWindow.Show();
        }

        private void Window_SizeChanged_1(object sender, SizeChangedEventArgs e)
        {
            var primaryScreenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            var primaryScreenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            var window = sender as Window;
            double rightMargin = primaryScreenWidth - (window.Left + window.Width);
            double bottomMargin = primaryScreenHeight - (window.Top + window.Height);

            if (window != null)
            {
                this.MyRadWindow.RestrictedAreaMargin = new Thickness(window.Left, window.Top, rightMargin, bottomMargin);
            }
        }

        private void Window_LocationChanged_1(object sender, EventArgs e)
        {
            Screen[] screensArray = Screen.AllScreens;
            List<Screen> screensList = screensArray.OfType<Screen>().ToList();
            var primaryScreenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            if (screensList.Count > 0)
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
