using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Controls;
using System.Windows.Input;
using Telerik.Windows.Controls;

namespace CreateModifyExport
{
    public class ToolBarViewModel : ViewModelBase
    {
        private readonly ExpenseViewModel expenseViewModel;
        private readonly ObservableCollection<string> reports;
        private string selectedReport;
        private ICommand exportReportCommand;

        public ToolBarViewModel(ExpenseViewModel expenseViewModel)
        {
            this.expenseViewModel = expenseViewModel;
            this.reports = new ObservableCollection<string>(new string[] { "Sales", "Marketing", "Engineering" });
            this.SelectedReport = this.Reports[0];
            this.exportReportCommand = new DelegateCommand((parameter) => { this.ExportReport(); });
        }

        public ObservableCollection<string> Reports
        {
            get
            {
                return this.reports;
            }
        }

        public string SelectedReport
        {
            get
            {
                return this.selectedReport;
            }
            set
            {
                if (this.selectedReport != value)
                {
                    this.selectedReport = value;
                    this.OnPropertyChanged("SelectedReport");
                }
            }
        }

        public ICommand ExportReportCommand
        {
            get
            {
                return this.exportReportCommand;
            }
            set
            {
                if (this.exportReportCommand != value)
                {
                    this.exportReportCommand = value;
                    this.OnPropertyChanged("ExportReportCommand");
                }
            }
        }

        private void ExportReport()
        {
            string departmentName = this.SelectedReport;
            string reportFileName = string.Format("{0}Expenses.pdf", departmentName);
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.DefaultFileName = reportFileName;
            dialog.Filter = "PDF files|*.pdf";

            if (dialog.ShowDialog() == true)
            {
                this.expenseViewModel.ExportReport(departmentName, dialog.OpenFile());
            }
        }
    }
}
