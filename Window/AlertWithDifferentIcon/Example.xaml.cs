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

namespace AlertWithDifferentIcon
{
    /// <summary>
    /// Interaction logic for Example.xaml
    /// </summary>
    public partial class Example : UserControl
    {
        public Example()
        {
            InitializeComponent();
        }

        private void OnOpenIcon1Clicked(object sender, RoutedEventArgs e)
        {
            RadWindow.Alert(new DialogParameters { ContentStyle = Application.Current.Resources["Icon1AlertStyle"] as Style, Content = "Dialog with Icon 1.", Header = "Dialog with Icon 1" });
        }

        private void OnOpenIcon2Clicked(object sender, RoutedEventArgs e)
        {
            RadWindow.Alert(new DialogParameters { ContentStyle = Application.Current.Resources["Icon2AlertStyle"] as Style, Content = "Dialog with Icon 2.", Header = "Dialog with Icon 2" });
        }
    }
}
