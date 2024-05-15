using System;
using System.Linq;
using System.Windows;
using Telerik.Windows.Data;
using System.Collections;
using System.ComponentModel;
using System.Collections.Specialized;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
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
