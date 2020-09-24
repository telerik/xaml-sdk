using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using Telerik.Windows.Controls;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.OpenXml.Xlsx;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.TextBased.Csv;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.TextBased.Txt;
using Telerik.Windows.Documents.Spreadsheet.Model;
using System.Windows.Controls;

namespace ConvertDocuments
{
    public class ExampleViewModel : ViewModelBase
    {
        private static readonly string SampleDocumentFile = "SampleDocument.xlsx";

        private readonly List<IWorkbookFormatProvider> providers;

        private Workbook workbook;
        public Workbook Workbook
        {
            get
            {
                return this.workbook;
            }
            set
            {
                if (this.workbook != value)
                {
                    this.workbook = value;
                    this.IsDocumentLoaded = value != null;
                    this.OnPropertyChanged("Workbook");
                }
            }
        }

        private bool isDocumentLoaded;
        public bool IsDocumentLoaded
        {
            get
            {
                return this.isDocumentLoaded;
            }
            set
            {
                if (this.isDocumentLoaded != value)
                {
                    this.isDocumentLoaded = value;
                    this.OnPropertyChanged("IsDocumentLoaded");
                }
            }
        }


        private ICommand openCommand = null;
        public ICommand OpenCommand
        {
            get
            {
                return this.openCommand;
            }
            set
            {
                if (this.openCommand != value)
                {
                    this.openCommand = value;
                    OnPropertyChanged("OpenCommand");
                }
            }
        }

        private ICommand openSampleCommand = null;
        public ICommand OpenSampleCommand
        {
            get
            {
                return this.openSampleCommand;
            }
            set
            {
                if (this.openSampleCommand != value)
                {
                    this.openSampleCommand = value;
                    OnPropertyChanged("OpenSampleCommand");
                }
            }
        }

        private ICommand saveCommand = null;
        public ICommand SaveCommand
        {
            get
            {
                return this.saveCommand;
            }
            set
            {
                if (this.saveCommand != value)
                {
                    this.saveCommand = value;
                    OnPropertyChanged("SaveCommand");
                }
            }
        }

        private string fileName;
        public string FileName
        {
            get
            {
                return this.fileName;
            }
            set
            {
                if (this.fileName != value)
                {
                    this.fileName = value;
                    this.OnPropertyChanged("FileName");
                }
            }
        }

        public IEnumerable<string> ExportFormats
        {
            get
            {
                return FileHelper.ExportFormats;
            }
        }

        string selectedExportFormat;
        public string SelectedExportFormat
        {
            get
            {
                return selectedExportFormat;
            }
            set
            {
                if (!object.Equals(selectedExportFormat, value))
                {
                    selectedExportFormat = value;

                    OnPropertyChanged("SelectedExportFormat");
                }
            }
        }

        public ExampleViewModel()
        {
            this.providers = new List<IWorkbookFormatProvider>()
            {
                new XlsxFormatProvider(),
                new CsvFormatProvider(),
                new TxtFormatProvider()
            };

            this.SelectedExportFormat = this.ExportFormats.First();
            this.SaveCommand = new DelegateCommand(this.Save);
            this.OpenCommand = new DelegateCommand(this.Open);
            this.OpenSampleCommand = new DelegateCommand(this.OpenSample);
        }

        private void Open(object obj)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Xlsx files|*.xlsx|Csv files|*.Csv|Text files|*.txt|All files (*.*)|*.*";
            dialog.FilterIndex = 1;
            if (dialog.ShowDialog() == true)
            {
#if SILVERLIGHT
                string fileName = dialog.File.Name;
#else
                string fileName = dialog.FileName;
#endif
                string extension = Path.GetExtension(fileName);
                IWorkbookFormatProvider provider = this.providers
                    .FirstOrDefault(p => p.SupportedExtensions
                        .Any(e => string.Compare(extension, e, StringComparison.InvariantCultureIgnoreCase) == 0));

                if (provider != null)
                {
#if SILVERLIGHT
                    using (Stream stream = dialog.File.OpenRead())
#else
                    using (Stream stream = dialog.OpenFile())
#endif
                    {
                        try
                        {
                            this.Workbook = provider.Import(stream);
                            this.FileName = Path.GetFileName(fileName);
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Could not open file.");
                            this.Workbook = null;
                            this.FileName = null;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Could not open file.");
                }
            }
        }

        private void OpenSample(object param)
        {
            using (Stream stream = FileHelper.GetSampleResourceStream(ExampleViewModel.SampleDocumentFile))
            {
                this.Workbook = new XlsxFormatProvider().Import(stream);
                this.FileName = ExampleViewModel.SampleDocumentFile;
            }
        }

        private void Save(object param)
        {
            string selectedFromat = this.SelectedExportFormat;
            FileHelper.SaveDocument(this.Workbook, selectedFromat);
        }
    }
}