using System;
using System.Linq;
using System.Windows;
using ReadOnlyEditorState;
using Telerik.Windows.Controls;

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
             propertyGrid.Item = new Employee()
                {
                    FirstName = "Sarah",
                    LastName = "Blake",
                    Occupation = "Supplied Manager",
                    StartingDate = new DateTime(2005, 04, 12),
                    IsMarried = true,
                    Salary = 3500
                };
            propertyGrid1.Item = new Employee()
              {
                  FirstName = "Sarah",
                  LastName = "Blake",
                  Occupation = "Supplied Manager",
                  StartingDate = new DateTime(2005, 04, 12),
                  IsMarried = true,
                  Salary = 3500
              };
        }
    }
}
