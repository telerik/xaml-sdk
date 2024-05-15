using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;

namespace ToggleSwitchButton_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void RadToggleButton_Unchecked1(object sender, RoutedEventArgs e)
        {
            var button = sender as RadToggleButton;
            button.Content = "NO";
        }
        private void RadToggleButton_Checked1(object sender, RoutedEventArgs e)
        {
            var button = sender as RadToggleButton;
            button.Content = "YES";
        }

        private void RadToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            var button = sender as RadToggleButton;
            button.Content = "ON";
        }

        private void RadToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            var button = sender as RadToggleButton;
            button.Content = "OFF";
        }

    }
}
