using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf.Export;
using Telerik.Windows.Documents.Fixed.Model;
using Telerik.Windows.Documents.Fixed.Model.Editing;

namespace ExportUIElement
{
    /// <summary>
    /// Interaction logic for Example.xaml
    /// </summary>
    public partial class Example : UserControl
    {
        public Example()
        {
            InitializeComponent();

            this.DataContext = new MainViewModel();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            this.Export(this.chart1Container);
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            this.Export(this.chart2Container);
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            this.Export(this.calendarContainer);
        }

        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            this.Export(this.ganttContainer);
        }

        private void Export(System.Windows.Controls.Border element)
        {
            PrepareForExport(element);

            var dialog = new SaveFileDialog
            {
                DefaultExt = "pdf",
                Filter = "Pdf files|*.pdf|All Files (*.*)|*.*",
            };
            if (dialog.ShowDialog() == true)
            {
                RadFixedDocument document = this.CreateDocument(element);
                PdfFormatProvider provider = new PdfFormatProvider();
                provider.ExportSettings.ImageQuality = ImageQuality.High;

                using (var output = dialog.OpenFile())
                {
                    provider.Export(document, output);
                }
            }
        }

        private RadFixedDocument CreateDocument(System.Windows.Controls.Border element)
        {
            RadFixedDocument document = new RadFixedDocument();

            RadFixedPage page = this.CreatePage(element);
            document.Pages.Add(page);

            return document;
        }

        private RadFixedPage CreatePage(System.Windows.Controls.Border element)
        {
            RadFixedPage page = new RadFixedPage();
            page.Size = new Size(1000, 1000);
            FixedContentEditor editor = new FixedContentEditor(page, Telerik.Windows.Documents.Fixed.Model.Data.MatrixPosition.Default);

            ExportHelper.ExportToPdf(element, editor);

            return page;
        }

        private static void PrepareForExport(FrameworkElement element)
        {
            if (element.ActualWidth == 0 && element.ActualHeight == 0)
            {
                double width = element.Width > 0 ? element.Width : 500;
                double height = element.Height > 0 ? element.Height : 300;
                element.Measure(Size.Empty);
                element.Measure(new Size(width, height));
                element.Arrange(new Rect(0, 0, width, height));
                element.UpdateLayout();
            }
        }
    }
}
