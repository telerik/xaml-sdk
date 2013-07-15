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

namespace SpecialSlotsZIndex
{
    public partial class Example : UserControl
    {
        MyViewModel model;
        public Example()
        {
            InitializeComponent();
            model = LayoutRoot.DataContext as MyViewModel;
        }

        private void ToggleButton_Checked_1(object sender, RoutedEventArgs e)
        {
            model.ZValue = 100;
        }

        private void ToggleButton_Unchecked_1(object sender, RoutedEventArgs e)
        {
            model.ZValue = 0;
        }
    }
}
