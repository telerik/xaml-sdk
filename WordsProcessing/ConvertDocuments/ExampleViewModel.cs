using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using Telerik.Windows.Controls;
using Telerik.Windows.Documents.Common.FormatProviders;
using Telerik.Windows.Documents.Flow.FormatProviders.Docx;
using Telerik.Windows.Documents.Flow.FormatProviders.Rtf;
using Telerik.Windows.Documents.Flow.FormatProviders.Txt;
using Telerik.Windows.Documents.Flow.Model;

namespace ConvertDocuments
{
    public class ExampleViewModel : ViewModelBase
    {
        private static readonly string sampleDocumentFile = "SampleDocument.docx";

        private readonly List<IFormatProvider<RadFlowDocument>> providers;

        private RadFlowDocument document;

        public RadFlowDocument Document
        {
            get
            {
                return this.document;
            }
            set
            {
                if (this.document != value)
                {
                    this.document = value;
                    this.IsDocumentLoaded = value != null;
                    this.OnPropertyChanged("Document");
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

        private IEnumerable<string> exportFormats;

        public IEnumerable<string> ExportFormats
        {
            get
            {
                if (exportFormats == null)
                {
                    exportFormats = new string[] { "Docx", "Rtf", "Txt" };
                }

                return exportFormats;
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
            this.providers = new List<IFormatProvider<RadFlowDocument>>()
            {
                new DocxFormatProvider(),
                new RtfFormatProvider(),
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
            dialog.Filter = "Docx files|*.docx|Rtf files|*.rtf|Html files|*.html|Text files|*.txt|All files (*.*)|*.*";
            dialog.FilterIndex = 1;
            if (dialog.ShowDialog() == true)
            {
                string extension = Path.GetExtension(dialog.FileName);
                IFormatProvider<RadFlowDocument> provider = this.providers
                                                                .FirstOrDefault(p => p.SupportedExtensions
                                                                                      .Any(e => string.Compare(extension, e, StringComparison.InvariantCultureIgnoreCase) == 0));

                if (provider != null)
                {
                    using (Stream stream = dialog.OpenFile())
                    {
                        try
                        {
                            this.Document = provider.Import(stream);
                            this.FileName = Path.GetFileName(dialog.FileName);
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Could not open file.");
                            this.Document = null;
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
            using (Stream stream = FileHelper.GetSampleResourceStream(ExampleViewModel.sampleDocumentFile))
            {
                this.Document = new DocxFormatProvider().Import(stream);
                this.FileName = ExampleViewModel.sampleDocumentFile;
            }
        }

        private void Save(object param)
        {
            string selectedFromat = this.SelectedExportFormat;
            FileHelper.SaveDocument(this.Document, selectedFromat);
        }
    }
}