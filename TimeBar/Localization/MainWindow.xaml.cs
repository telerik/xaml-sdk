using System.Globalization;
using System.Windows;
using Localization.Properties;
using Telerik.Windows.Controls;

namespace Localization
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			LocalizationManager.Manager = new LocalizationManager() { ResourceManager = RadTimeBarResources.ResourceManager };
			CultureInfo culture = new CultureInfo("de");
			System.Threading.Thread.CurrentThread.CurrentCulture = culture;
			System.Threading.Thread.CurrentThread.CurrentUICulture = culture;
			InitializeComponent();
		}
	}
}