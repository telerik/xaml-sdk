using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Windows.Input;
using Telerik.Windows.Controls;

namespace CreateModifyExport
{
    public class ToolBarViewModel : ViewModelBase
    {
        private readonly ExpenseViewModel expenseViewModel;
        private string exportDirectory;
        private ICommand exportReportsCommand;
        private ICommand chooseDirectoryCommand;

        public ToolBarViewModel(ExpenseViewModel expenseViewModel)
        {
            this.expenseViewModel = expenseViewModel;
            this.ExportDirectory = Directory.GetCurrentDirectory();
            this.exportReportsCommand = new DelegateCommand((parameter) => { this.ExportReports(); });
            this.chooseDirectoryCommand = new DelegateCommand((parameter) => { this.ChooseDirectory(); });
        }

        public string ExportDirectory
        {
            get
            {
                return this.exportDirectory;
            }
            set
            {
                if (this.exportDirectory != value)
                {
                    this.exportDirectory = value;
                    this.OnPropertyChanged("ExportDirectory");
                }
            }
        }

        public ICommand ExportReportsCommand
        {
            get
            {
                return this.exportReportsCommand;
            }
            set
            {
                if (this.exportReportsCommand != value)
                {
                    this.exportReportsCommand = value;
                    this.OnPropertyChanged("ExportReportsCommand");
                }
            }
        }

        public ICommand ChooseDirectoryCommand
        {
            get
            {
                return this.chooseDirectoryCommand;
            }
            set
            {
                if (this.chooseDirectoryCommand != value)
                {
                    this.chooseDirectoryCommand = value;
                    this.OnPropertyChanged("ChooseDirectoryCommand");
                }
            }
        }              

        private void ChooseDirectory()
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.ExportDirectory = dialog.SelectedPath;
            }
        }

        private void ExportReports()
        {
            this.expenseViewModel.ExportReports();
            Process.Start(this.ExportDirectory);
        }
    }
}
