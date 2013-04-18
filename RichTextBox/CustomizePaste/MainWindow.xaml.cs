using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using Telerik.Windows.Documents.Base;
using Telerik.Windows.Documents.FormatProviders;

namespace CustomizePaste
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetUpPlainTextPasteFromExternalSources();
        }

        private void SetUpPlainTextPasteFromExternalSources()
        {
            //Clears default paste handlers (RTF -> HTML -> TXT).
            ClipboardEx.ClipboardHandlers.Clear();

            //Instantiate and register only plain-text clipboard handler.
            ClipboardHandler clipboardHandler = new ClipboardHandler();
            clipboardHandler.ClipboardDataFormat = DataFormats.Text;
            clipboardHandler.DocumentFormatProvider = DocumentFormatProvidersManager.GetProviderByExtension("txt");
            ClipboardEx.ClipboardHandlers.Add(clipboardHandler);
        }

        /// <summary>
        /// Sets up default rich text paste that uses the RTF content of the clipboard first,
        /// then HTML and lastly - plain text.
        /// </summary>
        private void SetUpDefaultRichTextPasteFromExternalSources()
        {
            //Clears previously registered handlers and resets them to the default fallback values.
            ClipboardEx.ClipboardHandlers.Clear();

            ClipboardHandler rtfHandler = new ClipboardHandler();
            rtfHandler.DocumentFormatProvider = DocumentFormatProvidersManager.GetProviderByExtension("rtf");
            rtfHandler.ClipboardDataFormat = DataFormats.Rtf;
            ClipboardEx.ClipboardHandlers.Add(rtfHandler);

            ClipboardHandler htmlHandler = new ClipboardHandler();
            htmlHandler.DocumentFormatProvider = DocumentFormatProvidersManager.GetProviderByExtension("html");
            htmlHandler.ClipboardDataFormat = DataFormats.Html;
            htmlHandler.ClipboardStringFilter = ClipboardEx.StripHtmlClipboardFormatHeaders;
            ClipboardEx.ClipboardHandlers.Add(htmlHandler);

            ClipboardHandler txtHandler = new ClipboardHandler();
            txtHandler.DocumentFormatProvider = DocumentFormatProvidersManager.GetProviderByExtension("txt");
            txtHandler.ClipboardDataFormat = DataFormats.Text;
            ClipboardEx.ClipboardHandlers.Add(txtHandler);
        }

        private void RadRibbonToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            RadToggleButton button = (RadToggleButton)sender;
            if (button.IsChecked.Value)
            {
                this.SetUpDefaultRichTextPasteFromExternalSources();
            }
            else
            {
                this.SetUpPlainTextPasteFromExternalSources();
            }
        }

    }
}