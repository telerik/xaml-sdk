using Microsoft.Win32;
using System.Windows.Input;
using Telerik.Windows.Controls;
using Telerik.Documents.Fixed.FormatProviders.Pdf;
using Telerik.Documents.Fixed.Model;
using Telerik.Documents.Fixed.Model.Collections;
using Telerik.Documents.Fixed.Model.Common;
using Telerik.Documents.Fixed.Model.Graphics;
using Telerik.Documents.Fixed.Model.Objects;
using Telerik.Documents.Fixed.Model.Text;

namespace PdfElementsEditor
{
    public class ElementModel
    {
        internal int pageIndex { get; private set; }
        internal ContentElementBase Element { get; private set; }
        public int ElementID { get; private set; }
        public string ElementText { get; private set; }

        /// <summary>
        /// Gets the vector icon that visualizes the type of the content element in the tree view.
        /// </summary>
        public System.Windows.Media.Geometry ElementIcon
        {
            get
            {
                if (this.Element is Image)
                {
                    return IconGeometries.Image;
                }

                if (this.Element is TextFragment)
                {
                    return IconGeometries.Text;
                }

                if (this.Element is Path)
                {
                    return IconGeometries.Path;
                }

                return IconGeometries.Unknown;
            }
        }

        /// <summary>
        /// Gets the brush used to fill the <see cref="ElementIcon"/>.
        /// </summary>
        public System.Windows.Media.Brush ElementIconBrush
        {
            get
            {
                if (this.Element is Image)
                {
                    return IconGeometries.ImageBrush;
                }

                if (this.Element is TextFragment)
                {
                    return IconGeometries.TextBrush;
                }

                if (this.Element is Path)
                {
                    return IconGeometries.PathBrush;
                }

                return IconGeometries.UnknownBrush;
            }
        }

        public ICommand DeleteCommand { get; set; }
        public ICommand SaveSelectionCommand { get; set; }
        public ICommand EditTextCommand { get; set; }

        public ElementModel(int id, string text, ContentElementBase element, int pageIndex)
        {
            this.ElementID = id;
            this.ElementText = text;
            this.Element = element;
            this.pageIndex = pageIndex;

            this.DeleteCommand = new DelegateCommand(OnDeleteCommandExecuted);
            this.SaveSelectionCommand = new DelegateCommand(OnSaveSelectionCommandExecuted);
            this.EditTextCommand = new DelegateCommand(OnEditCommandExecuted);
        }

        private void OnEditCommandExecuted(object obj)
        {
            var element = this.Element as TextFragment;
            if (element != null)
            {
                var dialog = new TextEditDialog();
                dialog.Model.Text = element.Text;
                dialog.Model.FontSize = element.FontSize;

                if (dialog.ShowDialog() == true)
                {
                    element.Text = dialog.Model.Text;
                    element.FontSize = dialog.Model.FontSize;
                }
            }

            RefreshPdfViewer(obj);
        }

        private void OnSaveSelectionCommandExecuted(object obj)
        {
            var element = this.Element;

            RadFixedDocument document = new RadFixedDocument();
            RadFixedPage page = document.Pages.AddPage();

            if (element is Image image)
            {
                var newImage = new Image();
                newImage.ImageSource = image.ImageSource;
                newImage.Width = image.Width;
                newImage.Height = image.Height;
                newImage.Position = image.Position;
                page.Content.Add(newImage);
            }
            else if (element is TextFragment textFragment)
            {
                var newTextFragment = new TextFragment(textFragment.Text);
                newTextFragment.Text = textFragment.Text;
                newTextFragment.FontSize = textFragment.FontSize;
                newTextFragment.Font = textFragment.Font;
                newTextFragment.Position = textFragment.Position;
                page.Content.Add(newTextFragment);
            }
            else if (element is Path path)
            {
                var newPath = new Path();
                newPath.Geometry = path.Geometry;
                newPath.Fill = path.Fill;
                newPath.Stroke = path.Stroke;
                newPath.StrokeThickness = path.StrokeThickness;
                newPath.IsFilled = path.IsFilled;
                newPath.IsStroked = path.IsStroked;
                newPath.Position = path.Position;
                page.Content.Add(newPath);
            }
            else
            {
                return;
            }

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Pdf Files|*.pdf";
            dialog.DefaultExt = "pdf";
            dialog.FileName = "output.pdf";

            if (dialog.ShowDialog() == true)
            {
                using (var stream = dialog.OpenFile())
                {
                    new PdfFormatProvider().Export(document, stream, null);
                }
            }
        }

        private void OnDeleteCommandExecuted(object obj)
        {
            var pdfViewer = obj as RadPdfViewer;
            if (pdfViewer == null || pdfViewer.Document == null)
            {
                return;
            }

            // The element is not necessarily a direct child of the page - it can live inside
            // the content of a Form (FormSource), so the owning collection has to be found first.
            foreach (var page in pdfViewer.Document.Pages)
            {
                if (this.TryRemove(page.Content))
                {
                    RefreshPdfViewer(obj);
                    return;
                }
            }
        }

        private bool TryRemove(ContentElementCollection content)
        {
            if (content.Contains(this.Element))
            {
                content.Remove(this.Element);
                return true;
            }

            foreach (var item in content)
            {
                var form = item as Form;
                if (form != null && form.FormSource != null && this.TryRemove(form.FormSource.Content))
                {
                    return true;
                }
            }

            return false;
        }

        private static void RefreshPdfViewer(object obj)
        {
            var pdfViewer = obj as RadPdfViewer;
            var document = pdfViewer.Document;
            pdfViewer.Document = null;
            pdfViewer.Document = document;
        }
    }
}
