using ExportToPDF;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Diagrams;
using Telerik.Windows.Controls.Diagrams.Extensions;
using Telerik.Windows.Diagrams.Core;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf;
using Telerik.Windows.Documents.Fixed.Model;

namespace ExportToPDF_SL
{
    public partial class MainPage : UserControl
    {
        private FileManager fileManager;

        static MainPage()
        {
            var saveBinding = new CommandBinding(DiagramCommands.Save, ExecuteSave, CanExecutedSave);
            var openBinding = new CommandBinding(DiagramCommands.Open, ExecuteOpen);

            CommandManager.RegisterClassCommandBinding(typeof(MainPage), saveBinding);
            CommandManager.RegisterClassCommandBinding(typeof(MainPage), openBinding);
        }

        public MainPage()
        {
            InitializeComponent();

            this.fileManager = new FileManager(this.diagram);
            this.toolBox.ItemsSource = new HierarchicalGalleryItemsCollection();
        }

        private static void CanExecutedSave(object sender, CanExecuteRoutedEventArgs e)
        {
            var owner = sender as MainPage;
            e.CanExecute = owner != null && owner.diagram != null && owner.diagram.Items.Count > 0;
        }

        private static void ExecuteSave(object sender, ExecutedRoutedEventArgs e)
        {
            var owner = sender as MainPage;
            if (owner != null)
                owner.fileManager.SaveToFile();
        }

        private static void ExecuteOpen(object sender, ExecutedRoutedEventArgs e)
        {
            var owner = sender as MainPage;
            if (owner != null)
            {
                owner.fileManager.LoadFromFile();
            }
        }

        private void OnExportClick(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog
            {
                DefaultExt = "pdf",
                Filter = "Diagram files|*.pdf|All Files (*.*)|*.*",
				FileName = "Diagram.pdf"
            };
            if (dialog.ShowDialog() == true)
            {
                var enclosingBounds = this.diagram.CalculateEnclosingBounds();
                enclosingBounds = enclosingBounds.InflateRect(30);

                RadFixedDocument document = new RadFixedDocument();
                document.Pages.Add(ExportHelper.ExportDiagram(this.diagram, enclosingBounds));

                PdfFormatProvider provider = new PdfFormatProvider();
                using (var output = dialog.OpenFile())
                {
                    provider.Export(document, output);
                }
            }
        }
    }
}
