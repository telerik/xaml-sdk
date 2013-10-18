using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Telerik.Windows.Controls;
using Telerik.Windows.Documents;
using Telerik.Windows.Documents.Model;
using Telerik.Windows.DragDrop;

namespace DragAndDrop
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
            this.radRichTextBox.Loaded += radRichTextBox_Loaded;
        }

        void radRichTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            RadDocument document = new RadDocument();
            string randomText = @"On the Insert tab, the galleries include items that are designed to coordinate with the overall look of your document. You can use these galleries to insert tables, headers, footers, lists, cover pages, and other document building blocks. When you create pictures, charts, or diagrams, they also coordinate with your current document look";

            RadDocumentEditor documentEditor = new RadDocumentEditor(document);
            documentEditor.Insert(randomText);

            this.radRichTextBox.Document = (RadDocument)document.CreateDeepCopy();
            this.radRichTextBox.Document.Sections.First.Headers.Default.Body = document;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DragDropManager.AddDragInitializeHandler(this.radTreeView, OnDragInitialize);
            DragDropManager.AddDragOverHandler(this.radRichTextBox, OnDragOver);
            DragDropManager.AddDropHandler(this.radRichTextBox, OnDrop);
        }

        private void OnDragOver(object sender, Telerik.Windows.DragDrop.DragEventArgs args)
        {
            dropPosition.Visibility = Visibility.Collapsed;
            string payloadData = DragDropPayloadManager.GetDataFromObject(args.Data, "DragData") as string;
            if (string.IsNullOrEmpty(payloadData))
            {
                return;
            }

            RadRichTextBox mainEditor = sender as RadRichTextBox;
            RadRichTextBox richTextBox = mainEditor.ActiveDocumentEditor as RadRichTextBox;

            Point point = args.GetPosition(richTextBox);
            DocumentPosition pos = richTextBox.ActiveEditorPresenter.GetDocumentPositionFromViewPoint(point);
            richTextBox.Document.CaretPosition.MoveToPosition(pos);
        }

        private void OnDragInitialize(object sender, DragInitializeEventArgs args)
        {
            args.AllowedEffects = DragDropEffects.All;

            RadTreeView radTreeView = sender as RadTreeView;

            var payload = DragDropPayloadManager.GeneratePayload(null);
            payload.SetData("DragData", radTreeView.SelectedItem.ToString());
            args.Data = payload;
        }

        private void OnDrop(object sender, Telerik.Windows.DragDrop.DragEventArgs args)
        {
            string payloadData = DragDropPayloadManager.GetDataFromObject(args.Data, "DragData") as string;
            if (string.IsNullOrEmpty(payloadData))
            {
                return;
            }
            RadRichTextBox mainEditor = sender as RadRichTextBox;

            RadRichTextBox richTextBox = mainEditor.ActiveDocumentEditor as RadRichTextBox;
            richTextBox.CurrentEditingStyle.SpanProperties.ForeColor = Colors.Red;
            Dispatcher.BeginInvoke(new Action(delegate()
            {
                mainEditor.Focus();
                richTextBox.Insert(payloadData);
            }));
        }

    }
}
