using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CustomTabControlRegionAdapter
{
	/// <summary>
	/// Interaction logic for Shell.xaml
	/// </summary>
	public partial class Shell : Window
	{
		public Shell()
		{
			InitializeComponent();
		}

		private void ActivateFirstTab(object sender, RoutedEventArgs e)
		{
			Microsoft.Practices.Prism.Regions.IRegionManager rm = Microsoft.Practices.ServiceLocation.ServiceLocator.Current.GetInstance<Microsoft.Practices.Prism.Regions.IRegionManager>();
			Microsoft.Practices.Prism.Regions.IRegion clr = rm.Regions["TabControlRegion"];
			var itemToSelect = clr.Views.OfType<Module1.TabItemHomeView>().FirstOrDefault();
			clr.Activate(itemToSelect);
		}

		private void ActivateSecondTab(object sender, RoutedEventArgs e)
		{
			Microsoft.Practices.Prism.Regions.IRegionManager rm = Microsoft.Practices.ServiceLocation.ServiceLocator.Current.GetInstance<Microsoft.Practices.Prism.Regions.IRegionManager>();
			Microsoft.Practices.Prism.Regions.IRegion regions = rm.Regions["TabControlRegion"];
			var itemToSelect = regions.Views.OfType<Module2.TabItemButtonsView>().FirstOrDefault();
			regions.Activate(itemToSelect);
		}
	}
}
