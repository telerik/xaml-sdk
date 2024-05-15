using System;
using System.Linq;
using System.Windows;

namespace ToolBarDragAndDrop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            var mode = ToolBarTrayUtilities.NewBandMode.None;

            if (this.ButtonIndicator.IsChecked == true)
            {
                mode = ToolBarTrayUtilities.NewBandMode.Indicator;
            }
            else if (this.ButtonLive.IsChecked == true)
            {
                mode = ToolBarTrayUtilities.NewBandMode.Live;
            }

            ToolBarTrayUtilities.SetNewBandModeToTrays(mode);
        }
    }
}
