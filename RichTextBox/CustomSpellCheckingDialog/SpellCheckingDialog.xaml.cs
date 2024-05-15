using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
#if SILVERLIGHT
using Telerik.Windows.Input;
#else
using System.Windows.Input;
#endif
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.RichTextBoxUI.Dialogs;
using Telerik.Windows.Documents.TextSearch;
using Telerik.Windows.Documents.UI.Extensibility;

namespace CustomSpellCheckingDialog
{
    [CustomSpellCheckingDialog]
    public partial class SpellCheckingDialog : RadRichTextBoxWindow, ISpellCheckingDialog
    {
        private const int SuggestionsCount = 10;

        private string currentIncorrectWord;
        private SpellCheckingUIManager spellCheckingUIManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpellCheckingDialog"/> class.
        /// </summary>
        public SpellCheckingDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Shows the dialog.
        /// </summary>
        /// <param name="spellCheckingUIManager">The spellchecking UI manager.</param>
        /// <param name="owner">The owner of the dialog.</param>
        public void ShowDialog(SpellCheckingUIManager spellCheckingUIManager, RadRichTextBox owner)
        {
            this.ShowDialogInternal(spellCheckingUIManager, owner);
        }

        private void ShowDialogInternal(SpellCheckingUIManager spellCheckingUIManager, RadRichTextBox owner)
        {
            this.spellCheckingUIManager = spellCheckingUIManager;
            this.SetOwner(owner);

            if (this.TryLoadIncorrectSentence())
            {
                this.buttonEditCustomDictionary.IsEnabled = spellCheckingUIManager.HasCustomDictionary();
                this.ShowDialog();
            }
        }

        private bool TryLoadIncorrectSentence()
        {
            if (!this.LoadIncorrectSentence())
            {
                this.Close();
                this.AlertForCompletion();

                return false;
            }

            return true;
        }

        private bool LoadIncorrectSentence()
        {
            WordInfo incorrectWordInfo = this.spellCheckingUIManager.MoveToNextError();
            if (incorrectWordInfo == null)
            {
                return false;
            }

            this.currentIncorrectWord = incorrectWordInfo.Word;
            this.rtbSpellCheckingContext.Document = this.spellCheckingUIManager.CreateSpellCheckingContextDocument(incorrectWordInfo.WordPosition);
            this.LoadSuggestions(incorrectWordInfo.Word);

            return true;
        }

        private void AlertForCompletion()
        {
            RadWindow.Alert(new DialogParameters()
            {
                Header = this.Header,
                Content = LocalizationManager.GetString("Documents_SpellCheckingDialog_SpellingCheckIsComplete")
            });
        }

        private void LoadSuggestions(string word)
        {
            this.suggestionsListBox.ItemsSource = this.spellCheckingUIManager.GetSuggestions(word).Take(SuggestionsCount);

            if (this.suggestionsListBox.Items.Count > 0)
            {
                this.suggestionsListBox.SelectedIndex = 0;
            }
        }

        private void ChangeWordWithSuggestion(string suggestion)
        {
            this.spellCheckingUIManager.ChangeWord(suggestion);
            this.TryLoadIncorrectSentence();
        }

        private void ButtonIgnoreAll_Click(object sender, RoutedEventArgs e)
        {
            this.spellCheckingUIManager.IgnoreAll(this.currentIncorrectWord);
            this.TryLoadIncorrectSentence();
        }

        private void ButtonAddToDictionary_Click(object sender, RoutedEventArgs e)
        {
            this.spellCheckingUIManager.AddToDictionary(this.currentIncorrectWord);
            this.TryLoadIncorrectSentence();
        }

        private void ButtonChange_Click(object sender, RoutedEventArgs e)
        {
            this.ChangeWordWithSuggestion((string)this.suggestionsListBox.SelectedValue);
        }

        private void ButtonChangeAll_Click(object sender, RoutedEventArgs e)
        {
            // TODO change all occurrences with spelling suggestion 
            ////string suggestion = (string)this.suggestionsListBox.SelectedValue;
            ////this.spellCheckingUIManager.ChangeAll(this.currentIncorrectWord, suggestion);
            ////this.TryLoadIncorrectSentence();
        }

        private void ButtonEditCustomDictionary_Click(object sender, RoutedEventArgs e)
        {
            this.spellCheckingUIManager.ShowEditCustomDictionaryDialog();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

#if SILVERLIGHT
        private void ListBoxItemTemplateTextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            Mouse.AddMouseDownHandler((TextBlock)sender, this.SuggestionsListBoxItem_MouseDownHandler, true);
        }

        private void ListBoxItemTemplateTextBlock_Unloaded(object sender, RoutedEventArgs e)
        {
            Mouse.RemoveMouseDownHandler((TextBlock)sender, this.SuggestionsListBoxItem_MouseDownHandler);
        }

        private void SuggestionsListBoxItem_MouseDownHandler(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && e.ClickCount == 2)
            {
                this.ChangeWordWithSuggestion(((TextBlock)sender).Text);
            }
        }
#else
        private void SuggestionsListBoxItem_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string suggestion = (string)((ListBoxItem)sender).Content;
            this.ChangeWordWithSuggestion(suggestion);
        }
#endif

        private void SuggestionsListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            this.buttonChange.IsEnabled = this.suggestionsListBox.SelectedIndex >= 0;
        }

        private void RadWindow_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Escape)
            {
                this.Close();
            }
        }

        protected override void OnClosed(WindowClosedEventArgs args)
        {
            base.OnClosed(args);

            this.spellCheckingUIManager = null;
            this.Owner = null;
        }

    }
}

