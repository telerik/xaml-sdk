using System;
using System.Linq;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using System.Windows.Controls.Primitives;
using Telerik.Windows.Controls.GridView;
using System.Windows;
using ChangeCellBackgroundFromViewModel;

namespace SilverlightApplication1
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void rb1_Checked(object sender, RoutedEventArgs e)
        {
            (clubsGrid.DataContext as MyViewModel).Threshold = 5;
            (clubsGrid.DataContext as MyViewModel).Clubs.Reset();
        }
        private void rb2_Checked(object sender, RoutedEventArgs e)
        {
            (clubsGrid.DataContext as MyViewModel).Threshold = 10;
            (clubsGrid.DataContext as MyViewModel).Clubs.Reset();
        }

        private void rb3_Checked(object sender, RoutedEventArgs e)
        {
            (clubsGrid.DataContext as MyViewModel).Threshold = 15;
            (clubsGrid.DataContext as MyViewModel).Clubs.Reset();
        }
    }
}