using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Controls;
using Telerik.Windows.Controls;

namespace Localization
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            // This is an example of localization using resource files
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
            LocalizationManager.Manager = new LocalizationManager()
            {
                ResourceManager = RadSpreadsheetResources.ResourceManager
            };

            // This is an example of localization using Custom Localization Manager.
            // Uncomment the below line to use this option.
            // LocalizationManager.Manager = new CustomLocalizationManager();

            InitializeComponent();
        }
    }
}
