using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ScrollIntoViewAsyncMvvm;
using Telerik.Windows.Data;
using System.Collections;
using System.ComponentModel;
using System.Collections.Specialized;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Controls;
using System.Windows.Controls.Primitives;

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

		private void Button1_Click(object sender, RoutedEventArgs e)
		{
			MyViewModel vm = (this.clubsGrid.DataContext as MyViewModel);
			vm.Clubs.Add(new Club("My added Club", DateTime.Now, 123345));
		}
    }
}
