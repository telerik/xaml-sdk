using System;
using System.Linq;
using System.Windows.Controls;
using GroupNameTooltip;
using System.Windows.Input;
using System.Windows;
using Telerik.Windows.Controls;

namespace SilverlightApplication1
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
            PropertyGrid1.Item = new Order()
            {
                ShipAddress = "Luisenstr. 48",
                ShipCountry = "Germany",
                ShipName = "Toms Spezialitaten",
                ShipPostalCode = "44087",
                Employee = new Employee()
                {
                    FirstName = "Nancy",
                    LastName = "Davolio",
                    Title = "Sales Representative"
                },

            };

            PropertyGrid1.MouseMove += rpg1_MouseMove;
        }

        void rpg1_MouseMove(object sender, MouseEventArgs e)
        {
            var element = e.OriginalSource;
            var toggleButton = (element as FrameworkElement).ParentOfType<RadToggleButton>();

            if (toggleButton != null && toggleButton.Content != null)
            {
                ToolTip toolTip = new ToolTip();
                toolTip.Content = toggleButton.Content.ToString();
                ToolTipService.SetToolTip(toggleButton, toolTip);
            }
        }

        private void PropertyGrid1_AutoGeneratingPropertyDefinition(object sender, Telerik.Windows.Controls.Data.PropertyGrid.AutoGeneratingPropertyDefinitionEventArgs e)
        {
            if (e.PropertyDefinition.DisplayName == "Employee")
            {
                e.PropertyDefinition.GroupName = "Information in a long group name";
            }
        }
    }
}