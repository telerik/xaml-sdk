using System;
using System.Linq;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.FixedDocumentViewersUI.Dialogs;
using Telerik.Windows.Documents.Fixed.UI.Extensibility;

namespace Localization
{
    public partial class MainPage : UserControl
    {
        public MainPage()
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
