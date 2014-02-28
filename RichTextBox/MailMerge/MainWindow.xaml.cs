using System;
using System.IO;
using System.Linq;
using System.Windows;
using Telerik.Windows.Documents.FormatProviders.Xaml;
using Telerik.Windows.Documents.Model;

namespace MailMerge
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Stream stream = Application.GetResourceStream(new Uri("MailMerge;component/SampleData/SampleData.xaml", UriKind.Relative)).Stream;

            XamlFormatProvider provider = new XamlFormatProvider();

            RadDocument document = provider.Import(stream);

            this.editor.Document = document;

            this.editor.Document.MailMergeDataSource.ItemsSource = new ExamplesDataContext().Employees;
        }
    }
}
