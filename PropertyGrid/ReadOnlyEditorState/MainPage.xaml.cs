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

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            this.propertyGrid1.ReadOnlyEditorState = ReadOnlyEditorStates.ReadOnly;
            ResetItem();
            this.button1.Content = "ReadOnlyEditorStates: ReadOnly";
        }

        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            this.propertyGrid1.ReadOnlyEditorState = ReadOnlyEditorStates.Disabled;
            ResetItem();
            this.button1.Content = "ReadOnlyEditorStates: Disabled";
        }

        private void button1_Indeterminate(object sender, RoutedEventArgs e)
        {
            this.propertyGrid1.ReadOnlyEditorState = ReadOnlyEditorStates.Default;
            ResetItem();
            this.button1.Content = "ReadOnlyEditorStates: Default";
        }

        private void ResetItem()
        {
            this.propertyGrid1.Item = null;
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