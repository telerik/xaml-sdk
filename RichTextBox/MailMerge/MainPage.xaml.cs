using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Documents.FormatProviders.Xaml;
using Telerik.Windows.Documents.Model;
using Telerik.Windows.Documents.RichTextBoxCommands;

namespace MailMerge
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();

            using (Stream stream = Application.GetResourceStream(new Uri("MailMerge;component/SampleData/SampleDocument.xaml", UriKind.Relative)).Stream)
            {
                XamlFormatProvider provider = new XamlFormatProvider();
                RadDocument document = provider.Import(stream);
                this.editor.Document = document;
            }

            this.editor.Document.MailMergeDataSource.ItemsSource = new ExamplesDataContext().Employees;

            this.editor.CommandExecuting += editor_CommandExecuting;
        }

        void editor_CommandExecuting(object sender, Telerik.Windows.Documents.RichTextBoxCommands.CommandExecutingEventArgs e)
        {
            if (e.Command is InsertFieldCommand && e.CommandParameter is MergeField)
            {
                string fieldName = (e.CommandParameter as MergeField).PropertyPath;

                if ((e.CommandParameter as MergeField).PropertyPath.ToUpper() == "RECIPIENTPHOTO")
                {
                    e.Cancel = true;

                    MergeField mf = new MergeField();
                    mf.PropertyPath = fieldName;
                    IncludePictureField picField = new IncludePictureField();
                    picField.SetPropertyValue(IncludePictureField.ImageUriProperty, mf);
                    this.editor.InsertField(picField);
                }
            }
        }
    }
}
