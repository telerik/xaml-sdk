using System;
using System.Windows.Controls;
using System.Linq;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.OpenXml.Xlsx;

namespace FirstLook
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            WorkbookFormatProvidersManager.RegisterFormatProvider(new XlsxFormatProvider());
            InitializeComponent();
        }
    }
}

