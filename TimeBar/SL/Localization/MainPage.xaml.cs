using System.Globalization;
using System.Windows.Controls;
using Localization.Properties;
using Telerik.Windows.Controls;

namespace Localization
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			LocalizationManager.Manager = new LocalizationManager() { ResourceManager = RadTimeBarResources.ResourceManager };
			CultureInfo culture = new CultureInfo("de");
			System.Threading.Thread.CurrentThread.CurrentCulture = culture;
			System.Threading.Thread.CurrentThread.CurrentUICulture = culture;
			InitializeComponent();
		}
	}
}
