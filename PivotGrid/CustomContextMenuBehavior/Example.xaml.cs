using System.Windows.Controls;
using Telerik.Windows.Controls.FieldList;

namespace CustomContextMenuBehavior
{
    /// <summary>
    /// Interaction logic for Example.xaml
    /// </summary>
    public partial class Example : UserControl
    {
        public string ReadMe { get; set; }

        public Example()
        {
            InitializeComponent();
            this.ReadMe = GetText();
            this.DataContext = this;

            var customContextMenuBehavior = new MyCustomContextMenuBehavior();
            customContextMenuBehavior.Pivot = this.radPivotGrid;
            FieldListContextMenuBehavior.SetBehavior(this.radPivotFieldList, customContextMenuBehavior);
        }

        private string GetText()
        {
            return @"This example demonstrates how to implement a custom ContextMenuBehavior, which will allow you to modify the ContextMenus in RadPivotFieldList.
    The following scenarios are covered in the example:
    - The Label Filter of the PropertyGroupDescription items is removed
    - There is option added to change the step of the DoubleGroupDescription items 
    - There is option added to color the cells of the AggregateDescription items depending on some condition";
        }
    }
}
