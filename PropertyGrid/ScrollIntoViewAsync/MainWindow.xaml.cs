using System;
using System.Linq;
using System.Windows;
using ScrollIntoViewAsync;
using System.Windows.Input;
using Telerik.Windows.Controls;
using System.Windows.Controls;
using Telerik.Windows.Controls.Data.PropertyGrid;

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
            PropertyGrid1.Item =  new Employee()
                {
                    FirstName = "Sarah",
                    LastName = "Blake",
                    Occupation = "Supplied Manager",
                    StartingDate = new DateTime(2005, 04, 12),
                    IsMarried = true,
                    Salary = 3500
                };
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            var propertyDefinition = this.PropertyGrid1.PropertyDefinitions.Where(x => x.DisplayName == "Salary").FirstOrDefault();
            if (propertyDefinition != null)
            {
                PropertyGrid1.ScrollIntoViewAsync(propertyDefinition, new Action<PropertyGridField>(f => f.IsSelected = true));
            }
        }
    }
}
