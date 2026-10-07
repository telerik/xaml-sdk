using System;
using System.Windows;
using Telerik.Windows;
using Telerik.Windows.Controls;

namespace Office2016InspiredRibbonView_WPF
{
    public partial class MainWindow : RadRibbonWindow
    {
        static MainWindow()
        {
            RadRibbonWindow.IsWindowsThemeEnabled = false;
        }

        public MainWindow()
        {
            InitializeComponent();

            MessageBoxResult result = MessageBox.Show("The approach used in this example is now outdated. The Office2016 theme can be applied instead. For more details you can read the Office2016 help article. Do you want to check it out?",
                    "Confirmation", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                System.Diagnostics.Process.Start("https://docs.telerik.com/devtools/wpf/styling-and-appearance/themes-suite/common-styling-appearance-office2016-theme", "_blank");
            }
        }
    }
}
