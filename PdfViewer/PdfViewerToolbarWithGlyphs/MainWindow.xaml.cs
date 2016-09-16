using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls.FixedDocumentViewersUI.Dialogs;
using Telerik.Windows.Documents.Fixed.UI.Extensibility;

namespace PdfViewerToolbarWithGlyphs
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ExtensibilityManager.RegisterFindDialog(new FindDialog());
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
    }
}