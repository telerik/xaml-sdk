using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace RestrictedToParent
{
    public partial class MainPage : UserControl
    {
        private MyRadWindow MyRadWindow { get; set; }

        public MainPage()
        {
            InitializeComponent();
            this.MyRadWindow = new MyRadWindow();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.MyRadWindow.Show();
        }
    }
}
