using Microsoft.Win32;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;
using Telerik.Windows.Controls;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.OpenXml.Xlsx;
using Telerik.Windows.Documents.Spreadsheet.Model;
using ThreadSafeFormatProvider.Resources;

namespace ThreadSafeFormatProvider
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BackgroundWorker saver;
        private BackgroundWorker loader;
        private const string ResourceFilePath = "Resources/SlowImport.xlsx";
        private const string CouldNotLoadDocumentMessage = "Could not import file!";
        private const string SuccessfulSaveMessage = "File was saved successfully!";
        private const string UnsuccessfulSaveMessage = "Error occured while saving the file!";
        private const string Success = "Success!";
        private const string Error = "Error!";

        public MainWindow()
        {
            InitializeComponent();

            this.loader = new BackgroundWorker();
            this.loader.DoWork += Loader_DoWork;
            this.loader.RunWorkerCompleted += Loader_RunWorkerCompleted;

            this.saver = new BackgroundWorker();
            this.saver.DoWork += Saver_DoWork;
            this.saver.RunWorkerCompleted += Saver_RunWorkerCompleted;
        }

        private bool IsSavingOrLoading()
        {
            return this.loader.IsBusy || this.saver.IsBusy;
        }

        void Saver_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null || !((bool) e.Result))
            {
                ShowMessage(UnsuccessfulSaveMessage, Error);
                return;
            }

            ShowMessage(SuccessfulSaveMessage, Success);
        }

        void Saver_DoWork(object sender, DoWorkEventArgs e)
        {
            var arguments = (KeyValuePair<Workbook, Stream>) e.Argument;
            try
            {
                SaveWorkbook(arguments.Key, arguments.Value);
                e.Result = true;
            }
            catch
            {
                e.Result = false;
            }
        }

        private void SaveFileAsynchronously_Click(object sender, RoutedEventArgs e)
        {
            if (this.IsSavingOrLoading())
            {
                return;
            }

            Stream stream = GetSaveStream();
            if (stream != null)
            {
                KeyValuePair<Workbook, Stream> arguments = new KeyValuePair<Workbook, Stream>(this.radSpreadsheet.Workbook, stream);
                saver.RunWorkerAsync(arguments);
            }
        }

        private void SaveFileSynchronously_Click(object sender, RoutedEventArgs e)
        {
            if (this.IsSavingOrLoading())
            {
                return;
            }

            Stream stream = GetSaveStream();
            if (stream != null)
            {
                SaveWorkbook(this.radSpreadsheet.Workbook, stream);
                ShowMessage(SuccessfulSaveMessage, Success);
            }
        }

        private static void SaveWorkbook(Workbook workbook, Stream stream)
        {
            XlsxFormatProvider provider = new XlsxFormatProvider();

            using (stream)
            {
                provider.Export(workbook, stream, null);
            }
        }

        private static Stream GetSaveStream()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Xlsx files (*.xlsx)|*.xlsx";

            if (saveFileDialog.ShowDialog() == true)
            {
                return saveFileDialog.OpenFile();
            }

            return null;
        }

        void Loader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.busyIndicator.IsBusy = false;

            if (e.Cancelled || e.Result == null)
            {
                ShowMessage(CouldNotLoadDocumentMessage, Error);
                return;
            }

            this.radSpreadsheet.Workbook = (Workbook) e.Result;
        }

        void Loader_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = this.LoadWorkbook();
        }

        private void OpenFileAsynchronously_Click(object sender, RoutedEventArgs e)
        {
            if (this.IsSavingOrLoading())
            {
                return;
            }

            this.busyIndicator.IsBusy = true;
            loader.RunWorkerAsync();
        }

        private void OpenFileSynchronously_Click(object sender, RoutedEventArgs e)
        {
            if (this.IsSavingOrLoading())
            {
                return;
            }

            this.busyIndicator.IsBusy = true;
            Workbook workbook = this.LoadWorkbook();
            this.busyIndicator.IsBusy = false;

            if (workbook == null)
            {
                ShowMessage(CouldNotLoadDocumentMessage, Error);
                return;
            }

            this.radSpreadsheet.Workbook = workbook;
        }

        private Workbook LoadWorkbook()
        {
            Workbook result = null;

            try
            {
                XlsxFormatProvider formatProvider = new XlsxFormatProvider();

                using (Stream stream = ResourceHelper.GetResourceStream(ResourceFilePath))
                {
                    result = formatProvider.Import(stream, null);
                }
            }
            catch
            {
                return null;
            }

            return result;
        }

        private static void ShowMessage(string message, string header)
        {
            RadWindow.Alert(new DialogParameters() { Header = header, Content = message });
        }
    }
}
