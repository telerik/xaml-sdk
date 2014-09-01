using System;
using System.Linq;
using System.Windows.Controls;
using ScrollIntoViewAsyncMvvm;
using Telerik.Windows.Controls;
using System.Windows.Controls.Primitives;
using Telerik.Windows.Controls.GridView;
using System.Windows;

namespace SilverlightApplication1
{
    public partial class MainPage : UserControl
    {
        public MainPage()
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