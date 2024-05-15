using System;
using System.Linq;
using System.Windows;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.FixedDocumentViewersUI.Dialogs;
using Telerik.Windows.Documents.Fixed.UI.Extensibility;

namespace Localization
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            LocalizationManager.Manager = new LocalizationManager()
            {
                ResourceManager = PdfViewerResources.ResourceManager
            };

            ExtensibilityManager.RegisterFindDialog(new FindDialog());

            InitializeComponent();
        }

        private void Button_Click_1(object sender, System.Windows.RoutedEventArgs e)
        {
            this.pdfViewer.CommandDescriptors.ShowFindDialogCommandDescriptor.Command.Execute(null);
        }
    }
}
