using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Documents.FormatProviders.Xaml;
using Telerik.Windows.Documents.Model;

namespace MailMerge
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();

            using (Stream stream = Application.GetResourceStream(new Uri("MailMerge;component/SampleData/SampleData.xaml", UriKind.Relative)).Stream)
            {
                XamlFormatProvider provider = new XamlFormatProvider();
                RadDocument document = provider.Import(stream);
                this.editor.Document = document;
            }

            this.editor.Document.MailMergeDataSource.ItemsSource = new ExamplesDataContext().Employees;
        }
    }
}
