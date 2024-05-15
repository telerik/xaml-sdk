using Microsoft.Win32;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Documents.Base;
using Telerik.Windows.Documents.FormatProviders;
using Telerik.Windows.Documents.FormatProviders.Txt;
using Telerik.Windows.Documents.Model;
using Telerik.Windows.Documents.RichTextBoxCommands;

namespace CustomizeCommands
{
    /// <summary>
    /// Interaction logic for TelerikEditor.xaml
    /// </summary>
    public partial class TelerikEditor : UserControl
    {
        public TelerikEditor()
        {
            InitializeComponent();

            IconSources.ChangeIconsSet(IconsSet.Modern);

            this.radRichTextBox.CommandExecuting += RadRichTextBox_CommandExecuting;
            this.radRichTextBox.CommandExecuted += RadRichTextBox_CommandExecuted;
        }

        private void RadRichTextBox_CommandExecuted(object sender, CommandExecutedEventArgs e)
        {
            if (e.Command is InsertPictureCommand)
            {
                // After inserting an image, ensure that its width is not more than 200px.
                double maxWidth = 200;

                foreach (var image in this.radRichTextBox.Document.EnumerateChildrenOfType<ImageInline>())
                {
                    if (image.Width > maxWidth)
                    {
                        double ratio = image.Height / image.Width;
                        this.radRichTextBox.ChangeImageSize(image, new Size(maxWidth, maxWidth * ratio));
                    }
                }
            }
        }

        private void RadRichTextBox_CommandExecuting(object sender, CommandExecutingEventArgs e)
        {
            if (e.Command is SaveCommand)
            {
                e.Cancel = true;
                SaveDocument(); // A custom logic for saving document so you can change the properties of the Save File dialog.
            }

            if (e.Command is PasteCommand)
            {
                // Altering the PasteCommand to ensure only plain text is pasted in RadRichTextBox.
                // Obtain the content from the clipboard.
                RadDocument documentFromClipboard = ClipboardEx.GetDocument().ToDocument();

                TxtFormatProvider provider = new TxtFormatProvider();
                // Convert it to plain text.
                string plainText = provider.Export(documentFromClipboard);

                // Create a RadDocument instance from the plain text.
                RadDocument documentToInsert = provider.Import(plainText);
                // Set this document as a content to the clipboard.
                ClipboardEx.SetDocument(new DocumentFragment(documentToInsert));
            }

            if (e.Command is InsertTableCommand)
            {
                // Disable the possibility to insert tables into the document.
                MessageBox.Show("Inserting tables is not allowed.");
                e.Cancel = true;
            }
        }

        private void SaveDocument()
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            var formatProviders = DocumentFormatProvidersManager.FormatProviders.Where(fp => fp.CanExport);
            saveDialog.Filter = string.Join("|", formatProviders.Select(fp => GetFilter(fp)).ToArray()) + "|All Files|*.*";

            saveDialog.FilterIndex = 3;

            bool? dialogResult = saveDialog.ShowDialog();
            if (dialogResult == true)
            {
                string extension = Path.GetExtension(saveDialog.SafeFileName);               
                IDocumentFormatProvider provider = DocumentFormatProvidersManager.GetProviderByExtension(extension);

                using (Stream output = saveDialog.OpenFile())
                {
                    provider.Export(this.radRichTextBox.Document, output);
                }
            }
        }

        public static string GetFilter(IDocumentFormatProvider formatProvider)
        {
            return
                formatProvider.FilesDescription +
                " (" +
                string.Join(", ", formatProvider.SupportedExtensions.Select(ext => "*" + ext).ToArray()) +
                ")|" +
                string.Join(";", formatProvider.SupportedExtensions.Select(ext => "*" + ext).ToArray());
        }
    }
}