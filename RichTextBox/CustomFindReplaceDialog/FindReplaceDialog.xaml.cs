using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Telerik.Windows.Documents;
using Telerik.Windows.Documents.Model;
using Telerik.Windows.Documents.TextSearch;
using Telerik.Windows.Documents.UI.Extensibility;
using System.Text.RegularExpressions;
using System.ComponentModel.Composition;

namespace Telerik.Windows.Controls.RichTextBoxUI.Dialogs
{
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [CustomFindReplace]
    public partial class FindReplaceDialog : RadRichTextBoxWindow, IFindReplaceDialog
    {
        private bool passedThroughEnd;
        private Func<string, bool> replaceCallback;
        private RadRichTextBox richTextBox;
        private DocumentSelectReplaceStateService selectReplaceStateService;
        private DocumentPosition initialFindPosition;
        private DocumentPosition initialCaretPosition;

        public FindReplaceDialog()
        {
            InitializeComponent();
            this.btnFindNext.IsDefault = true;
            this.btnClose.IsCancel = true;
        }

        public string TextToFind
        {
            get
            {
                return this.tbFindText.Text;
            }
            set
            {
                this.tbFindText.Text = value;
            }
        }

        public RadDocument Document
        {
            get
            {
                return this.richTextBox.Document;
            }
        }

        private void btnFindNext_Click(object sender, RoutedEventArgs e)
        {
            this.FindNext();
        }

        public void ResetFindDialog()
        {
            this.passedThroughEnd = false;
            this.initialFindPosition = null;
        }

        public void FindNext()
        {
            if (this.Document.Selection.IsEmpty)
            {
                this.FindNext(this.Document.CaretPosition);
            }
            else
            {
                DocumentPosition fromPosition = new DocumentPosition(this.Document.Selection.Ranges.Last.StartPosition);
                fromPosition.MoveToNext();
                this.FindNext(fromPosition);
            }
        }

        public void FindNext(DocumentPosition fromPosition)
        {
            this.tbFindText.Focus();
            this.tbFindText.SelectAll();

            if (this.selectReplaceStateService.IsLastFoundSuccessful)
            {
                this.selectReplaceStateService.UnsubscribeFromDocumentEvents();
                this.selectReplaceStateService.SubscribeForDocumentEvents();
            }

            if (this.initialFindPosition == null)
            {
                this.initialFindPosition = new DocumentPosition(fromPosition);
            }

            DocumentTextSearch textSearch = new DocumentTextSearch(this.Document);
            TextRange find = textSearch.Find(this.GetSearchText(), fromPosition);

            if (find != null)
            {
                if (find.StartPosition >= this.initialFindPosition && !this.passedThroughEnd
                    || find.StartPosition < this.initialFindPosition && this.passedThroughEnd)
                {
                    this.selectReplaceStateService.SelectFoundRange(find);
                    this.richTextBox.ActiveEditorPresenter.UpdateScrollOffsetFromDocumentPosition(find.StartPosition);
                    this.RepositionDialog(find.StartPosition);
                }
                else
                {
                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        RadWindow.Alert(new DialogParameters()
                        {
                            Header = this.Header,
                            Content = LocalizationManager.GetString("Documents_FindReplaceDialog_FinishedSearching")
                        });
                    }));
                    this.ResetFindDialog();
                    this.passedThroughEnd = false;
                    this.Document.Selection.Clear();
                }
            }
            else
            {
                if (!passedThroughEnd)
                {
                    passedThroughEnd = true;
                    this.FindNext(new DocumentPosition(this.Document));
                }
                else
                {
                    string content;
                    if (this.selectReplaceStateService.IsLastFoundSuccessful)
                    {
                        content = LocalizationManager.GetString("Documents_FindReplaceDialog_FinishedSearching");
                    }
                    else
                    {
                        content = string.Format(LocalizationManager.GetString("Documents_FindReplaceDialog_SearchedTextNotFound"), this.tbFindText.Text);
                    }
                    Dispatcher.BeginInvoke(new Action(() => RadWindow.Alert(new DialogParameters()
                    {
                        Header = this.Header,
                        Content = content
                    })));
                    this.Document.Selection.Clear();
                    this.ResetFindDialog();
                }
            }
        }

        private void RepositionDialog(DocumentPosition targetPosition)
        {
            var targetLocation = new Point();
            var point = this.richTextBox.ActiveEditorPresenter.GetViewPointFromDocumentPosition(targetPosition);
            targetLocation.X = point.X;
            targetLocation.Y = point.Y;

            if (System.Windows.Interop.BrowserInteropHelper.IsBrowserHosted)
            {
                targetLocation = this.richTextBox.TransformToVisual(Application.Current.MainWindow).Transform(targetLocation);
            }
            else
            {
                targetLocation = this.richTextBox.PointToScreen(targetLocation);
            }

            Rect dialogRect = new Rect()
            {
                X = this.Left,
                Y = this.Top,
                Width = this.ActualWidth,
                Height = this.ActualHeight
            };

            // TODO: smarter move
            if (dialogRect.Contains(targetLocation))
            {
                this.Top = targetLocation.Y - this.ActualHeight - 5;
                if (this.Top < 0)
                {
                    this.Top = targetLocation.Y + (targetPosition.GetCurrentInlineBox().ControlBoundingRectangle.Height * this.richTextBox.ScaleFactor.Height) + 5;
                }
            }
        }

        private string GetSearchText()
        {
            return Regex.Escape(TelerikHelper.CleanUpNewLines(this.tbFindText.Text));
        }

        private void btnReplace_Click(object sender, RoutedEventArgs e)
        {
            if (this.selectReplaceStateService.IsLastFoundSuccessful)
            {
                this.replaceCallback(this.tbReplaceText.Text);
            }

            this.FindNext();
        }

        private void tbFindText_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.ResetFindDialog();
            this.EnableButtonsAccordingToText();
        }

        private void tbReplaceText_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.EnableButtonsAccordingToText();
        }

        private void EnableButtonsAccordingToText()
        {
            bool canFind = this.tbFindText.Text != string.Empty;
            this.btnFindNext.IsEnabled = canFind;

            bool canReplace = !this.richTextBox.IsReadOnly && canFind;
            this.btnReplace.IsEnabled = this.btnReplaceAll.IsEnabled = canReplace;
        }

        /// <summary>
        /// Shows the dialog.
        /// </summary>
        /// <param name="richTextBox">The associated <see cref="RadRichTextBox"/>.</param>
        /// <param name="replaceCallback">The callback that will be invoked to perform replace.</param>
        /// <param name="textToFind">The text to initially set in the search field.</param>
        public void Show(RadRichTextBox richTextBox, Action<string> replaceCallback, string textToFind)
        {
            Func<string, bool> func = (param) => { replaceCallback(param); return true; };
            this.Show(richTextBox, func, textToFind);
        }

        /// <summary>
        /// Shows the dialog.
        /// </summary>
        /// <param name="richTextBox">The associated <see cref="RadRichTextBox"/>.</param>
        /// <param name="replaceCallback">The callback that will be invoked to perform replace.</param>
        /// <param name="textToFind">The text to initially set in the search field.</param>
        public void Show(RadRichTextBox richTextBox, Func<string, bool> replaceCallback, string textToFind)
        {
            this.richTextBox = richTextBox;
            this.replaceCallback = replaceCallback;
            this.initialCaretPosition = new DocumentPosition(this.richTextBox.Document.CaretPosition);
            this.initialCaretPosition.AnchorToNextFormattingSymbol();

            if (!this.IsOpen && textToFind != null)
            {
                this.TextToFind = textToFind;
            }

            this.SetOwner(richTextBox);

            if (this.selectReplaceStateService != null)
            {
                this.selectReplaceStateService.UnsubscribeFromDocumentEvents();
            }

            this.selectReplaceStateService = new DocumentSelectReplaceStateService(this.Document, this.ResetFindDialog);

            this.SetupUIAccordingToReadOnly(richTextBox);
            this.EnableButtonsAccordingToText();

            if (!this.IsOpen)
            {
                this.ResetFindDialog();
                this.Show();

                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    this.tbFindText.Focus();
                    this.tbFindText.SelectAll();
                }));
            }
        }

        private void SetupUIAccordingToReadOnly(RadRichTextBox richTextBox)
        {
            this.tbReplaceText.IsEnabled = !richTextBox.IsReadOnly;
            if (!this.tbReplaceText.IsEnabled)
            {
                this.tbReplaceText.Text = string.Empty;
            }

            this.textBlockReplaceWith.Opacity = richTextBox.IsReadOnly ? 0.5 : 1;
        }

        private void EnableEditableFields(RadRichTextBox richTextBox)
        {
            this.tbReplaceText.IsEnabled = richTextBox.IsReadOnly;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnReplaceAll_Click(object sender, RoutedEventArgs e)
        {
            int foundCount = 0;

            this.richTextBox.SuspendUpdateLayout();
            this.Document.BeginUpdate();
            try
            {
                using (DocumentPosition startFindPosition = new DocumentPosition(this.Document.DocumentLayoutBox, true))
                {
                    var found = true;

                    while (found)
                    {
                        DocumentTextSearch textSearch = new DocumentTextSearch(this.Document);
                        TextRange find = textSearch.Find(this.GetSearchText(), startFindPosition);

                        found = find != null;

                        if (found)
                        {
                            startFindPosition.MoveToPosition(find.EndPosition);
                            startFindPosition.AnchorToNextFormattingSymbol();

                            //This is needed to update the current style for editing
                            this.Document.CaretPosition.MoveToPosition(find.StartPosition);
                            find.SetSelection(this.Document);
                            if (this.replaceCallback(this.tbReplaceText.Text))
                            {
                                foundCount++;
                            }
                            startFindPosition.RemoveAnchorFromNextFormattingSymbol();
                            startFindPosition.MoveToPosition(this.Document.CaretPosition);
                        }
                    }
                }
            }
            finally
            {
                this.Document.EndUpdate();
                this.richTextBox.ResumeUpdateLayout();
            }

            RadWindow.Alert(new DialogParameters()
            {
                Header = this.Header,
                Content = string.Format(LocalizationManager.GetString("Documents_FindReplaceDialog_MadeReplacements"), foundCount),
                Owner = this
            });
        }

        protected override void OnClosed(WindowClosedEventArgs args)
        {
            base.OnClosed(args);

            this.selectReplaceStateService.UnsubscribeFromDocumentEvents();

            this.replaceCallback = null;
            if (this.richTextBox.Document.Selection.IsEmpty)
            {
                initialCaretPosition.RemoveAnchorFromNextFormattingSymbol();
                this.richTextBox.Document.CaretPosition.MoveToPosition(initialCaretPosition);
            }
            else
            {
                this.richTextBox.ActiveEditorPresenter.HideCaret();
            }
            this.richTextBox.Focus();
            this.richTextBox = null;
            this.Owner = null;
        }
    }
}