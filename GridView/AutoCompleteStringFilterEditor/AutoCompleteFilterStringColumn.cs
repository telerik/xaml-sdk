using System;
using System.Linq;
using System.Windows;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace AutoCompleteStringFilterEditor
{
    /// <summary>
    /// A string column, whose field filter editors have the auto-complete functionality.
    /// The ItemsSource of the RadAutoCompleteBox are the distinct values of the column.
    /// </summary>
    public class AutoCompleteFilterStringColumn : GridViewDataColumn
    {
        public override FrameworkElement CreateFieldFilterEditor()
        {
            return FilterEditorFactory.CreateAutoCompleteStringEditor();
        } 
    }
}
