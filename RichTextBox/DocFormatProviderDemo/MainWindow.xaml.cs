using System.IO;
using System.Windows;
using Telerik.Windows.Controls;

namespace DocFormatProviderDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            RadRichTextBox.DefaultTextRenderingMode = Telerik.Windows.Documents.UI.TextBlocks.TextBlockRenderingMode.TextBlockWithPropertyCaching;

            InitializeComponent();

            var provider = new DocFormatProvider();
            this.radRichTextBox.Document = provider.Import(new FileStream("../../SampleData/DocFormat.doc", FileMode.Open));
        }

        private void btnImport_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.radRichTextBox.Commands.OpenDocumentCommand.Execute();
        }
    }
}
