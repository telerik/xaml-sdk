using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Documents.Fixed;

namespace FirstLook
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void tbCurrentPage_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                if (e.Key == System.Windows.Input.Key.Enter)
                {
                    textBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                }
            }
        }

        private void tbFind_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                this.pdfViewer.Commands.FindCommand.Execute(this.tbFind.Text);
                this.btnPrev.Visibility = System.Windows.Visibility.Visible;
                this.btnNext.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void LoadFromStream(object sender, System.Windows.RoutedEventArgs e)
        {
            Stream str = App.GetResourceStream(new System.Uri("FirstLook;component/SampleData/Sample.pdf", System.UriKind.Relative)).Stream;
            this.pdfViewer.DocumentSource = new PdfDocumentSource(str);
        }

        private void LoadFromUri(object sender, System.Windows.RoutedEventArgs e)
        {
            this.pdfViewer.DocumentSource = new PdfDocumentSource(new System.Uri("FirstLook;component/SampleData/Sample.pdf", System.UriKind.Relative));
        }
    }
}
