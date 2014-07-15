using System;
using System.Linq;
using System.Windows;
using ChangeCellBackgroundFromViewModel;

namespace WpfApplication1
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
