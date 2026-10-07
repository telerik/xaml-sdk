using System.Threading;
using System.Windows;
using Telerik.Windows.Controls;

namespace Localization
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
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
