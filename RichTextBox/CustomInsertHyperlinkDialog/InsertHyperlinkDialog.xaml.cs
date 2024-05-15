using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.RichTextBoxUI.Dialogs;
using Telerik.Windows.Documents.Model;
using Telerik.Windows.Documents.UI.Extensibility;

namespace CustomInsertHyperlinkDialog
{
    /// <summary>
    /// Represents dialog for inserting hyperlinks.
    /// </summary>
    [CustomInsertHyperlink]
    public partial class InsertHyperlinkDialog : RadRichTextBoxWindow, IInsertHyperlinkDialog
    {
        private static readonly string httpProtocol = "http://";

        private bool callbackCalled;
        private Action<string, HyperlinkInfo> insertHyperlinkCallback;
        private Action cancelCallback;

#if SILVERLIGHT
        private string hyperlinkPattern = @"^(((http|https|ftp)://)|(mailto:)|(onenote:))(\S+)$";
#else
        private string hyperlinkPattern = @"^(((http|https|ftp|file)://)|(mailto:)|(\\)|(onenote:)|(www\.))(\S+)$";
#endif
        public string HyperlinkPattern
        {
            get { return hyperlinkPattern; }
            set { hyperlinkPattern = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InsertHyperlinkDialog"/> class.
        /// </summary>
        public InsertHyperlinkDialog()
        {
            InitializeComponent();
            this.comboBookmarks.EmptyText = LocalizationManager.GetString("Documents_InsertHyperlinkDialog_SelectBookmark");
            this.Loaded += (s, a) =>
            {
                {
                    if (txtAddress.Visibility == System.Windows.Visibility.Visible)
                    {
                        this.txtAddress.Focus();
                        this.txtAddress.SelectAll();
                    }
                    else
                    {
                        this.txtText.Focus();
                        this.txtText.SelectAll();
                    }
                }
            };

#if WPF
            this.buttonOK.IsDefault = true;
            this.buttonCancel.IsCancel = true;
#endif
        }
#if WPF
        /// <summary>
        /// Shows the dialog for inserting hyperlinks.
        /// </summary>
        /// <param name="text">The text of the hyperlink.</param>
        /// <param name="currentHyperlinkInfo">The current hyperlink info. Null if we are not in edit mode.</param>
        /// <param name="bookmarkNames">Names of all existing bookmarks.</param>
        /// <param name="insertHyperlinkCallback">The callback that will be called on confirmation to insert the hyperlink.</param>
        /// <param name="cancelCallback">The callback that will be called on cancelation.</param>
        /// <param name="owner">The owner of the dialog.</param>
        public void ShowDialog(string text, HyperlinkInfo currentHyperlinkInfo, IEnumerable<string> bookmarkNames, Action<string, HyperlinkInfo> insertHyperlinkCallback, Action cancelCallback, RadRichTextBox owner)
        {
            this.ShowDialogInternal(text, currentHyperlinkInfo, bookmarkNames, insertHyperlinkCallback, cancelCallback, owner);
        }
#else

        /// <summary>
        /// Shows the dialog for inserting hyperlinks.
        /// </summary>
        /// <param name="text">The text of the hyperlink.</param>
        /// <param name="currentHyperlinkInfo">The current hyperlink info. Null if we are not in edit mode.</param>
        /// <param name="bookmarkNames">Names of all existing bookmarks.</param>
        /// <param name="insertHyperlinkCallback">The callback that will be called on confirmation to insert the hyperlink.</param>
        /// <param name="cancelCallback">The callback that will be called on cancelation.</param>
        public void ShowDialog(string text, HyperlinkInfo currentHyperlinkInfo, IEnumerable<string> bookmarkNames, Action<string, HyperlinkInfo> insertHyperlinkCallback, Action cancelCallback)
        {
            this.ShowDialogInternal(text, currentHyperlinkInfo, bookmarkNames, insertHyperlinkCallback, cancelCallback, null);
        }
#endif
        private void ShowDialogInternal(string text, HyperlinkInfo currentHyperlinkInfo, IEnumerable<string> bookmarkNames, Action<string, HyperlinkInfo> insertHyperlinkCallback, Action cancelCallback, RadRichTextBox owner)
        {
            this.ResetDialog();

            this.comboBookmarks.DataContext = bookmarkNames;
            this.insertHyperlinkCallback = insertHyperlinkCallback;
            this.cancelCallback = cancelCallback;
            this.callbackCalled = false;
            this.SetOwner(owner);

            if (text == null)
            {
                this.txtText.IsEnabled = false;
                this.txtText.Text = LocalizationManager.GetString("Documents_InsertHyperlinkDialog_SelectionInDocument");
            }
            else
            {
                this.txtText.IsEnabled = true;
                this.txtText.Text = text;
            }

            if (currentHyperlinkInfo != null)
            {
                this.PreselectTarget(currentHyperlinkInfo.Target);

                if (!currentHyperlinkInfo.IsAnchor)
                {
                    this.txtAddress.Text = (currentHyperlinkInfo.NavigateUri != null)? currentHyperlinkInfo.NavigateUri: string.Empty;
                }
                else
                {
                    this.PreselectBookmark(currentHyperlinkInfo.NavigateUri);
                    this.rbBookmark.IsChecked = true;
                    this.ChangeUriUIVisibility(true);
                }
            }

            this.ShowDialog();
        }

        private void PreselectTarget(HyperlinkTargets target)
        {
            string targetAsString = target.ToString();
            foreach (RadComboBoxItem item in comboTarget.Items)
            {
                if (targetAsString.Equals(item.Tag))
                {
                    comboTarget.SelectedItem = item;
                    break;
                }
            }
        }

        private void PreselectBookmark(string bookmarkName)
        {
            if (this.comboBookmarks.Items.Contains(bookmarkName))
            {
                this.comboBookmarks.SelectedValue = bookmarkName;
            }
            else
            {
                this.comboBookmarks.SelectedValue = null;
            }
        }

        private void ChangeUriUIVisibility(bool showBookmarkCombo)
        {
            if (showBookmarkCombo)
            {
                this.comboBookmarks.Visibility = Visibility.Visible;
                this.txtAddress.Visibility = Visibility.Collapsed;
            }
            else
            {
                this.comboBookmarks.Visibility = Visibility.Collapsed;
                this.txtAddress.Visibility = Visibility.Visible;
            }
        }
    
        private void ResetDialog()
        {
            this.comboTarget.SelectedIndex = 0;
            //this.comboBookmarks.SelectedIndex = 0;
            this.comboBookmarks.SelectedValue = null;
            this.txtAddress.Text = string.Empty;
            this.txtText.Text = string.Empty;
            this.txtValidation.Text = string.Empty;
            this.rbURL.IsChecked = true;

            this.txtAddress.Visibility = Visibility.Visible;
            this.comboBookmarks.Visibility = Visibility.Collapsed;
        }

        private void OnOkClicked()
        {
            txtValidation.Text = string.Empty;

            if (txtText.IsEnabled)
            {
                if (string.IsNullOrEmpty(txtText.Text))
                {
                    txtValidation.Text = LocalizationManager.GetString("Documents_InsertHyperlinkDialog_InvalidText");
                    return;
                }
            }

            bool isUrl = (this.rbURL.IsChecked == true);

            if (isUrl)
            {
                if (string.IsNullOrEmpty(txtAddress.Text.Trim()))
                {
                    txtValidation.Text = LocalizationManager.GetString("Documents_InsertHyperlinkDialog_InvalidAddress");
                    return;
                }
            }
            else
            {
                if (comboBookmarks.SelectedValue == null)
                {
                    txtValidation.Text = LocalizationManager.GetString("Documents_InsertHyperlinkDialog_InvalidBookmark");
                    return;
                }
            }

            HyperlinkInfo hyperlinkInfo = new HyperlinkInfo();

            if (isUrl)
            {
                string navigateUri = txtAddress.Text.Trim();
                if (!Regex.IsMatch(navigateUri, this.HyperlinkPattern))
                {
                    navigateUri = InsertHyperlinkDialog.httpProtocol + navigateUri;
                }
                hyperlinkInfo.NavigateUri = navigateUri;
            }
            else
            {
                hyperlinkInfo.NavigateUri = (string)comboBookmarks.SelectedValue;
                hyperlinkInfo.IsAnchor = true;
            }

            string targetStr = ((RadComboBoxItem)this.comboTarget.SelectedItem).Tag.ToString();
            hyperlinkInfo.Target = (HyperlinkTargets)Enum.Parse(typeof(HyperlinkTargets), targetStr, false);

            string hyperlinkText = this.txtText.IsEnabled ? this.txtText.Text : string.Empty;
            if (this.insertHyperlinkCallback != null)
            {
                this.insertHyperlinkCallback(hyperlinkText, hyperlinkInfo);
            }
            this.callbackCalled = true;
            this.Close();
        }

        private void buttonOK_Click(object sender, RoutedEventArgs e)
        {
            this.OnOkClicked();
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RadWindow_Closed(object sender, WindowClosedEventArgs e)
        {
            if (!this.callbackCalled && this.cancelCallback != null)
            {
                this.cancelCallback();
            }

            this.insertHyperlinkCallback = null;
            this.cancelCallback = null;
            this.callbackCalled = true;
            this.Owner = null;
        }

        private void RadRadioButton_Click(object sender, RoutedEventArgs e)
        {
            this.ChangeUriUIVisibility(rbBookmark.IsChecked.Value);
        }

#if SILVERLIGHT
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (!e.Handled)
            {
                if (e.Key == Key.Escape)
                {
                    this.Close();
                }
                else if (e.Key == Key.Enter)
                {
                    this.OnOkClicked();
                }
            }
        }
#endif
    }
}
