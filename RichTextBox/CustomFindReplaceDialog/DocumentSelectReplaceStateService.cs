using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.Windows.Documents.Model;

namespace Telerik.Windows.Documents.TextSearch
{
    internal class DocumentSelectReplaceStateService
    {
        #region Fields

        private RadDocument document;
        private bool selectionChangingInternally;
        private Action resetStateCallBack;

        #endregion

        #region Properties

        public bool IsLastFoundSuccessful { get; set; }

        #endregion

        #region Constructors

        public DocumentSelectReplaceStateService(RadDocument document, Action resetStateCallBack)
        {
            this.document = document;
            this.resetStateCallBack = resetStateCallBack;
            this.SubscribeForDocumentEvents();
        }

        #endregion

        #region Methods

        public void SubscribeForDocumentEvents()
        {
            this.document.Selection.SelectionChanged += this.Selection_SelectionChanged;
            this.document.CaretPosition.PositionChanged += this.CaretPosition_PositionChanged;
        }

        public void UnsubscribeFromDocumentEvents()
        {
            this.document.Selection.SelectionChanged -= this.Selection_SelectionChanged;
            this.document.CaretPosition.PositionChanged -= this.CaretPosition_PositionChanged;
        }

        public void SelectFoundRange(TextRange find)
        {
            this.selectionChangingInternally = true;
            this.document.CaretPosition.MoveToPosition(find.StartPosition);
            find.SetSelection(this.document);

            this.selectionChangingInternally = false;
            this.IsLastFoundSuccessful = true;
        }

        private void CaretPosition_PositionChanged(object sender, EventArgs e)
        {
            if (!this.selectionChangingInternally)
            {
                this.ResetState();
            }
        }

        private void ResetState()
        {
            if (resetStateCallBack != null)
            {
                resetStateCallBack();
            }
            this.IsLastFoundSuccessful = false;
        }

        private void Selection_SelectionChanged(object sender, EventArgs e)
        {
            if (!this.selectionChangingInternally)
            {
                this.ResetState();
            }
        }        

        #endregion
    }
}
