using System;
using System.Linq;
using System.Windows.Input;
using Telerik.Windows.Documents.UI;

namespace CustomCaret
{
    public class SogouCaret : Caret
    {
        private string lastUpdateString = string.Empty;

        protected override void OnTextInputStart(object sender, TextCompositionEventArgs e)
        {
            this.LastInputEvent = InputEvents.OnTextInputStart;
        }

        protected override void OnTextInputUpdate(object sender, TextCompositionEventArgs e)
        {
            this.LastInputEvent = InputEvents.OnTextInputUpdate;

            if (!string.IsNullOrEmpty(this.lastUpdateString) && !string.IsNullOrWhiteSpace(this.lastUpdateString))
            {
                this.OnTextInserted(this, new TextInsertedEventArgs(e.Text, InputEvents.OnTextInputUpdate, this.CurrentImeLanguage));
            }

            this.lastUpdateString = e.Text;
        }

        protected override void OnTextInput(TextCompositionEventArgs e)
        {
            this.LastInputEvent = InputEvents.OnTextInput;

            this.OnTextInserted(this, new TextInsertedEventArgs(e.Text, InputEvents.OnTextInput, this.CurrentImeLanguage));
            this.ClearText();
        }

        protected override void OnTextChanged()
        {
            if (this.IsCarriageReturnOrWhiteSpace())
            {
                this.OnTextInserted(this, new TextInsertedEventArgs(this.Text, InputEvents.OnTextInput, this.CurrentImeLanguage));
            }
        }

        private bool IsCarriageReturnOrWhiteSpace()
        {
            return this.Text == "\r" ||
                   this.Text == "\n" ||
                   this.Text == "\r\n" ||
                   this.Text == " ";
        }
    }
}