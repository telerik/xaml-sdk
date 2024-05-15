using System;
using System.Linq;
using System.Windows.Controls;
using ComboboxEditorBooleanProperty;
using System.Windows;
using Telerik.Windows.Controls.Data.PropertyGrid;
using System.Windows.Data;
using Telerik.Windows.Controls;

namespace SilverlightApplication1
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
            PropertyGrid1.Item = new Employee()
                    {
                        FirstName = "Sarah",
                        LastName = "Blake",
                        Occupation = "Supplied Manager",
                        StartingDate = new DateTime(2005, 04, 12),
                        IsMarried = true,
                        Salary = 3500
                    };
            PropertyGrid2.Item = new Employee()
               {
                   FirstName = "Sarah",
                   LastName = "Blake",
                   Occupation = "Supplied Manager",
                   StartingDate = new DateTime(2005, 04, 12),
                   IsMarried = false,
                   Salary = 500
               };
        }

        private void PropertyGrid2_AutoGeneratingPropertyDefinition(object sender, AutoGeneratingPropertyDefinitionEventArgs e)
        {
            if (e.PropertyDefinition.SourceProperty.PropertyType == typeof(bool))
            {
                e.PropertyDefinition.EditorTemplate = this.Resources["comboboxEditor"] as DataTemplate;
            }
        }
    }
}