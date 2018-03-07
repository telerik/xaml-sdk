using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using CustomizingContextMenuSpreadsheet;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Spreadsheet.Worksheets;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.OpenXml.Xlsx;
using Telerik.Windows.Documents.Spreadsheet.Model;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.Pdf;

namespace CustomContextMenu
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string ContextMenuItemHeader = "Highlight";
        private readonly PatternFill highlightFill;

        public MainWindow()
        {
            this.highlightFill = new PatternFill(PatternType.Solid, Colors.Yellow, Colors.Transparent);

            InitializeComponent();

            WorkbookFormatProvidersManager.RegisterFormatProvider(new PdfFormatProvider());
            WorkbookFormatProvidersManager.RegisterFormatProvider(new XlsxFormatProvider());

            this.AddCustomContextMenuItem();
        }

        private void AddCustomContextMenuItem()
        {
            DelegateCommand highlightCommand = null;

            Action<object> highlightCommandAction = new Action<object>((parameter) =>
            {
                this.radSpreadsheet.ActiveWorksheetEditor.Selection.Cells.SetFill(highlightFill);
                highlightCommand.InvalidateCanExecute();
            });

            Predicate<object> highlightCommandPredicate = new Predicate<object>((obj) =>
            {
                RadWorksheetEditor editor = this.radSpreadsheet.ActiveWorksheetEditor;
                if (editor != null)
                {
                    PatternFill patternFill = editor.Worksheet.Cells[editor.Selection.ActiveRange.SelectedCellRange].GetFill().Value as PatternFill;
                    if (patternFill != null)
                    {
                        return !patternFill.Equals(highlightFill);
                    }
                }

                return false;
            });

            highlightCommand = new SelectionDependentCommand(this.radSpreadsheet, highlightCommandAction, highlightCommandPredicate);

            RadMenuItem newItem = new RadMenuItem();
            newItem.Header = ContextMenuItemHeader;
            newItem.Command = highlightCommand;

            this.radSpreadsheet.WorksheetEditorContextMenu.Items.Add(newItem);
        }
    }
}
