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
using Telerik.Windows.Controls;

namespace DependenciesEditing
{
    public partial class Example : UserControl
    {
        public Example()
        {
            InitializeComponent();
        }

        private void RadAutoCompleteBox_GotFocus_1(object sender, RoutedEventArgs e)
        {
            var radAutoCompleteBox = sender as RadAutoCompleteBox;
            if (!radAutoCompleteBox.IsDropDownOpen)
            {
                radAutoCompleteBox.Populate(radAutoCompleteBox.SearchText);
            }
        }
    }
}
