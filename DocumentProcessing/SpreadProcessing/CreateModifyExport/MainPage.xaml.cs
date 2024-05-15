using System;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.OpenXml.Xlsx;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.Pdf;

namespace CreateModifyExport
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            StyleManager.ApplicationTheme = new Windows8Theme();

            WorkbookFormatProvidersManager.RegisterFormatProvider(new PdfFormatProvider());
            WorkbookFormatProvidersManager.RegisterFormatProvider(new XlsxFormatProvider());

            InitializeComponent();
        }
    }
}
