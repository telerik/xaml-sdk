using System;
using System.IO;
using System.Linq;
using System.Windows;
using Telerik.Windows.Documents.Fixed;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf.Filters;

namespace CustomDecoder_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private JpxDecoder filter;

        public MainWindow()
        {
            filter = new JpxDecoder();
            FiltersManager.RegisterFilter(filter);

            InitializeComponent();

            var stream = File.OpenRead("../../SampleData/test.pdf");
            this.viewer.DocumentSource = new PdfDocumentSource(stream);
        }
    }
}
