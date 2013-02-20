using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace SelectedItems
{
	/// <summary>
	/// Interaction logic for Example.xaml
	/// </summary>
	public partial class Example : UserControl
	{
		private MyViewModel MyViewModel { get; set; }

		public Example()
		{
			InitializeComponent();
			this.MyViewModel = new MyViewModel();
			this.DataContext = this.MyViewModel;
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			var items = this.ListBox.ItemsSource as ObservableCollection<Country>;
			this.MyViewModel.SelectedCountries = new System.Collections.ObjectModel.ObservableCollection<Country>()
			{
				items[0],
				items[5]
			};
		}
	}
}
