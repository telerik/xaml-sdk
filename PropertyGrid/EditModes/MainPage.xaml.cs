using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using ReadOnlyEditorState;
using Telerik.Windows.Controls;

namespace SilverlightApplication1
{
    public partial class MainPage : UserControl
    {
        public MainPage()
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