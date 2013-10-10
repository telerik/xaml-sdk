using System;
using System.Linq;
using System.Windows.Controls;
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
            this.radSpreadsheet.Loaded += radSpreadsheet_Loaded;
        }

        void radSpreadsheet_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            this.radSpreadsheet.Focus();
        }


    }
}

