using System;
using System.Linq;
using System.Windows;

namespace CustomAggregateFunctionEnumerableProperty
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
