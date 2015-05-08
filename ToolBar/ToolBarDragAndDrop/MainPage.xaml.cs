using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using Telerik.Windows.DragDrop;

namespace ToolBarDragAndDrop_SL
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            var mode = ToolBarTrayUtilitiesSL.NewBandMode.None;

            if (this.ButtonIndicator.IsChecked == true)
            {
                mode = ToolBarTrayUtilitiesSL.NewBandMode.Indicator;
            }
            else if (this.ButtonLive.IsChecked == true)
            {
                mode = ToolBarTrayUtilitiesSL.NewBandMode.Live;
            }

            ToolBarTrayUtilitiesSL.SetNewBandModeToTrays(mode);
        }
        
    }
}
