using System;
using System.Collections.Generic;
using System.Windows;
using Telerik.Windows.Controls.Spreadsheet.Worksheets;
using Telerik.Windows.Documents.Spreadsheet.Model;

namespace CustomCellEditLayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RadWorksheetEditor worksheetEditor;

        public MainWindow()
        {
            InitializeComponent();
            this.radSpreadsheet.WorksheetUILayersBuilder = new CustomLayersBuilder(this.radSpreadsheet);
            this.radSpreadsheet.ActiveSheetEditorChanged += RadSpreadsheet_ActiveSheetEditorChanged;
            this.Populate();
        }

        private void Populate()
        {
            List<Person> records = Person.GetSampleData();
            int numberOfRecords = records.Count;

            Worksheet activeWorksheet = this.radSpreadsheet.ActiveWorksheet;

            for (int i = 0; i < numberOfRecords; i++)
            {
                activeWorksheet.Cells[i, 0].SetValue(records[i].Name);
                activeWorksheet.Cells[i, 1].SetValue(records[i].Age);
            }
        }

        private void RadSpreadsheet_ActiveSheetEditorChanged(object sender, EventArgs e)
        {
            if (this.worksheetEditor != null)
            {
                this.DetachToWorksheetEditorEvent();
            }

            this.worksheetEditor = this.radSpreadsheet.ActiveWorksheetEditor;

            if (this.worksheetEditor != null)
            {
                this.AttachToWorksheetEditorEvent();
            }
        }

        private void DetachToWorksheetEditorEvent()
        {
            this.worksheetEditor.Selection.ActiveCellModeChanged -= Selection_ActiveCellModeChanged;
        }

        private void AttachToWorksheetEditorEvent()
        {
            this.worksheetEditor.Selection.ActiveCellModeChanged += Selection_ActiveCellModeChanged;
        }

        private void Selection_ActiveCellModeChanged(object sender, EventArgs e)
        {
            Selection selection = this.worksheetEditor.Selection;
            if (selection.ActiveCellMode == ActiveCellMode.Edit && selection.ActiveCellIndex.ColumnIndex == Constants.ButtonColumnIndex)
            {
                selection.ActiveCellMode = ActiveCellMode.Display;
            }
        }
    }
}
