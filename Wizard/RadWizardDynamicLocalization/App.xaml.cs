using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using Telerik.Windows.Controls;

namespace RadWizardDynamicLocalization
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		public App()
		{
			LocalizationManager.UseDynamicLocalization = true;
			LocalizationManager.DefaultCulture = new CultureInfo("en");
		}
	}
}
