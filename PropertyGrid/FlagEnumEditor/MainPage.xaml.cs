using System;
using System.Linq;
using System.Windows.Controls;
using FlagEnumEditor;

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
        }
    }
}