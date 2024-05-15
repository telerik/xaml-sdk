using System;
using System.Linq;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using System.Windows.Controls.Primitives;
using Telerik.Windows.Controls.GridView;
using System.Windows;

namespace SilverlightApplication1
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
            this.clubsGrid.LayoutUpdated += clubsGrid_LayoutUpdated;
        }

        void clubsGrid_LayoutUpdated(object sender, EventArgs e)
        {
            var controlPanelButton = this.clubsGrid.ChildrenOfType<RadDropDownButton>().FirstOrDefault(b => b.Name == "PART_ControlPanelItemControlDropDownButton");
            if (controlPanelButton != null)
            {
                controlPanelButton.DropDownOpened += controlPanelButton_DropDownOpened;
                controlPanelButton.DropDownClosed += controlPanelButton_DropDownClosed;
            }
            this.clubsGrid.LayoutUpdated -= clubsGrid_LayoutUpdated;
        }

        void controlPanelButton_DropDownClosed(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Control Panel is closed");
        }

        void controlPanelButton_DropDownOpened(object sender, RoutedEventArgs e)
        {
        }
    }
}