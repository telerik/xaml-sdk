using System;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Spreadsheet.Worksheets;

namespace CustomizingContextMenuSpreadsheet
{
    public class SelectionDependentCommand : DelegateCommand
    {
        private readonly RadSpreadsheet radSpreadsheet;
        private RadWorksheetEditor worksheetEditor;

        public SelectionDependentCommand(RadSpreadsheet radSpreadsheet, Action<object> action, Predicate<object> predicate)
            : base(action, predicate)
        {
            this.radSpreadsheet = radSpreadsheet;

            this.radSpreadsheet.ActiveSheetEditorChanged += this.RadSpreadsheetActiveSheetEditorChanged;
        }

        private void RadSpreadsheetActiveSheetEditorChanged(object sender, EventArgs e)
        {
            if (this.worksheetEditor != null)
            {
                this.worksheetEditor.Selection.SelectionChanged -= this.Selection_SelectionChanged;
            }

            this.worksheetEditor = this.radSpreadsheet.ActiveWorksheetEditor;

            if (this.worksheetEditor != null)
            {
                this.worksheetEditor.Selection.SelectionChanged += this.Selection_SelectionChanged;
            }
        }

        private void Selection_SelectionChanged(object sender, EventArgs e)
        {
            this.InvalidateCanExecute();
        }
    }
}