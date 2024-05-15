using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using Telerik.Windows.Documents.FormatProviders;
using Telerik.Windows.Documents.FormatProviders.Html;
using Telerik.Windows.Documents.FormatProviders.OpenXml.Docx;
using Telerik.Windows.Documents.FormatProviders.Rtf;
using Telerik.Windows.Documents.FormatProviders.Xaml;
using Telerik.Windows.Documents.Model;

namespace ThreadSafeDocumentConversion
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string toPath;

        public MainWindow()
        {
            InitializeComponent();

            LinkedList<FormatProviderPair> toFormatProviders = new LinkedList<FormatProviderPair>();
            toFormatProviders.AddLast(new FormatProviderPair(new DocxFormatProvider(), "Docx"));
            toFormatProviders.AddLast(new FormatProviderPair(new RtfFormatProvider(), "Rtf"));
            toFormatProviders.AddLast(new FormatProviderPair(new XamlFormatProvider(), "Xaml"));
            toFormatProviders.AddLast(new FormatProviderPair(new HtmlFormatProvider(), "Html"));

            this.toFormatProviderComboBox.ItemsSource = toFormatProviders;
            this.toFormatProviderComboBox.SelectedIndex = 0;

            LinkedList<FormatProviderPair> fromFormatProviders = new LinkedList<FormatProviderPair>(toFormatProviders);
            fromFormatProviders.AddFirst(new FormatProviderPair(null, "Any"));

            this.fromFormatProviderComboBox.ItemsSource = fromFormatProviders;
            this.fromFormatProviderComboBox.SelectedIndex = 0;
        }

        private void FromPathButton_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                this.fromPathTextBox.Text = dialog.SelectedPath;
            }
        }

        private void ToPathButton_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                this.toPathTextBox.Text = dialog.SelectedPath;
            }
        }

        private void ExecuteButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.fromPathTextBox.Text))
            {
                System.Windows.MessageBox.Show("From path is not specified.");
                return;
            }

            if (string.IsNullOrEmpty(this.toPathTextBox.Text))
            {
                System.Windows.MessageBox.Show("To path is not specified.");
                return;
            }

            if (this.toFormatProviderComboBox.SelectedItem == null)
            {
                System.Windows.MessageBox.Show("Please specify desired format provider.");
                return;
            }

            this.toPath = this.toPathTextBox.Text;

            IDocumentFormatProvider fromFormatProvider = (this.fromFormatProviderComboBox.SelectedItem as FormatProviderPair).FormatProvider;
            IDocumentFormatProvider toFormatProvider = (this.toFormatProviderComboBox.SelectedItem as FormatProviderPair).FormatProvider;

            DirectoryInfo directoryInfo = new DirectoryInfo(this.fromPathTextBox.Text);
            FileInfo[] fileInfoArray = directoryInfo.GetFiles();

            LinkedList<Task> tasks = new LinkedList<Task>();

            foreach (FileInfo fileInfo in fileInfoArray)
            {
                if (fromFormatProvider != null && !fromFormatProvider.SupportedExtensions.Contains(fileInfo.Extension))
                {
                    continue;
                }

                Action<object> action = new Action<object>(this.ConvertFiles);
                ThreadParameters threadParameters = new ThreadParameters(fileInfo, fromFormatProvider, toFormatProvider);

                Task task = new Task(action, threadParameters);
                tasks.AddLast(task);
                task.Start();
            }

            try
            {
                Task.WaitAll(tasks.ToArray());

                System.Windows.MessageBox.Show("All files are converted.");
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Exception occur during convertion of file: " + ex.Message);
            }
        }

        private void ConvertFiles(object parameter)
        {
            ThreadParameters threadParameters = parameter as ThreadParameters;

            IDocumentFormatProvider fromFormatProvider = 
                    (threadParameters.FromFormatProvider == null) ? DocumentFormatProvidersManager.GetProviderByExtension(threadParameters.FileInfo.Extension) : 
                    threadParameters.FromFormatProvider;

            RadDocument document = null;
            using (Stream readStream = threadParameters.FileInfo.OpenRead())
            {
                document = fromFormatProvider.Import(readStream);
            }

            string fileName = Path.GetFileNameWithoutExtension(threadParameters.FileInfo.Name) + threadParameters.ToFormatProvider.SupportedExtensions.First();

            using (FileStream writeStream = new FileStream(this.toPath + "\\" + fileName, FileMode.Create))
            {
                document.EnsureDocumentMeasuredAndArranged();
                threadParameters.ToFormatProvider.Export(document, writeStream);
            }
        }
    }
}
