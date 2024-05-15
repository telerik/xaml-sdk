using System;
using System.Linq;
using System.Windows;
using ComboboxEditorBooleanProperty;
using System.Windows.Input;
using Telerik.Windows.Controls;
using System.Windows.Controls;
using Telerik.Windows.Controls.Data.PropertyGrid;
using System.Collections.Generic;

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
                e.PropertyDefinition.EditorTemplate = TryFindResource("comboboxEditor") as DataTemplate;
            }
        }
    }
}
