using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CustomAggregateFunctionEnumerableProperty
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button1_Unchecked(object sender, RoutedEventArgs e)
        {
            this.clubsGrid.ShowColumnFooters = false;
        }

        private void Button1_Checked(object sender, RoutedEventArgs e)
        {
            this.clubsGrid.ShowColumnFooters = true;
        }
    }
}
