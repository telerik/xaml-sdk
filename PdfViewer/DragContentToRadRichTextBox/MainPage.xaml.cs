using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Telerik.Windows.Controls;
using Telerik.Windows.Documents.Model;
using Telerik.Windows.Documents.Model.Styles;
using Telerik.Windows.DragDrop;

namespace DragContentToRadRichTextBox
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();

            this.pdfViewer.MouseLeftButtonDown += pdfViewer_MouseLeftButtonDown;
            this.pdfViewer.MouseLeftButtonUp += pdfViewer_MouseLeftButtonUp;

            DragDropManager.AddDragInitializeHandler(this.pdfViewer, OnDragInitialize);
            DragDropManager.AddDropHandler(this.radRichTextBox, OnDrop);
        }

        void pdfViewer_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (this.pdfViewer.GetSelectedText() != string.Empty)
            {
                DragDropManager.SetAllowCapturedDrag(this.pdfViewer, true);
            }
        }

        void pdfViewer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.pdfViewer.GetSelectedText() == string.Empty)
            {
                DragDropManager.SetAllowCapturedDrag(this.pdfViewer, false);
            }
        }

        private void OnDragInitialize(object sender, DragInitializeEventArgs args)
        {
            args.AllowedEffects = DragDropEffects.All;
            var payload = DragDropPayloadManager.GeneratePayload(null);
            string data = this.pdfViewer.GetSelectedText();
            payload.SetData("DragData", data);
            args.Data = payload;
        }

        private void OnDrop(object sender, Telerik.Windows.DragDrop.DragEventArgs args)
        {
            var data = ((IDataObject)args.Data).GetData("DragData");
            (sender as RadRichTextBox).Document.Insert(data.ToString(),new StyleDefinition());
        }
    }
}
