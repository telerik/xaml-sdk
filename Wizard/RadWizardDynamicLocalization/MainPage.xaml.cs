using System;
using System.Globalization;
using System.Windows.Controls;
using Telerik.Windows.Controls;

namespace DynamicLocalization_SL
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			InitializeComponent();
			this.LanguageComboBox.SelectedIndex = 0;
		}

		private void RadComboBox_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangedEventArgs e)
		{
			switch (LanguageComboBox.SelectionBoxItem.ToString())
			{
				case "en":
					LocalizationManager.Manager.Culture = new CultureInfo("en");
					break;
				case "de":
					LocalizationManager.Manager.Culture = new CultureInfo("de");
					break;
				case "es":
					LocalizationManager.Manager.Culture = new CultureInfo("es");
					break;
				case "fr":
					LocalizationManager.Manager.Culture = new CultureInfo("fr");
					break;
				case "it":
					LocalizationManager.Manager.Culture = new CultureInfo("it");
					break;
				case "nl":
					LocalizationManager.Manager.Culture = new CultureInfo("nl");
					break;
				case "tr":
					LocalizationManager.Manager.Culture = new CultureInfo("tr");
					break;
				default:
					LocalizationManager.Manager.Culture = new CultureInfo("en");
					break;
			}

		}
	}
}
