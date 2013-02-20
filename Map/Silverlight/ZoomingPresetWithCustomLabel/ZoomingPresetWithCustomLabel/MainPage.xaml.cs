using System;
using System.Linq;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Map;

namespace ZoomingPresetWithCustomLabel
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();

            this.radMap.InitializeCompleted += new EventHandler(radMap_InitializeCompleted);
        }

        private void radMap_InitializeCompleted(object sender, EventArgs e)
        {
            this.SetCustomZoomLevelLabel(18, "My Level");
        }

        private void SetCustomZoomLevelLabel(int zoomLevel, string label)
        {
            CommandDescription description = (from cmd in this.radMap.MapZoomBar.Commands
                                              where (int)cmd.CommandParameter == zoomLevel
                                              select cmd).FirstOrDefault();
            if (description != null)
            {
                RoutedUICommand command = description.Command as RoutedUICommand;
                if (command != null)
                {
                    command.Text = label;
                }
            }
        }
    }
}
