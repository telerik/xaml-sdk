using System;
using System.Linq;
using System.Windows.Controls;
using Telerik.Windows.Controls;

namespace MinusKeyCustomizations
{
    public class NumericInputMinusDeletable : RadMaskedNumericInput
    {
        private TextBox innerTextBox = null;

        public string TextInternal
        {
            get
            {
                return this.innerTextBox == null ? null : this.innerTextBox.Text;
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.innerTextBox = this.GetTemplateChild("EditorSite") as TextBox;
        }

        protected override bool CanModifyChar(char character)
        {
            if (character.ToString() == this.NegativeSign.Token)
            {
                return true;
            }
            return base.CanModifyChar(character);
        }

        protected override void HandleBackKeyNoMask()
        {
            this.TryClearAll();

            int selectionStart = this.SelectionStart;
            base.HandleBackKeyNoMask();
            this.TryDeleteMinusSign(selectionStart);
        }

        protected override void HandleDeleteKeyNoMask()
        {
            this.TryClearAll();

            int selectionStart = this.SelectionStart;
            base.HandleDeleteKeyNoMask();
            this.TryDeleteMinusSign(selectionStart);
        }

        private void TryDeleteMinusSign(int nextSelectionStart)
        {
            if (this.IsNegativeValue && !string.IsNullOrEmpty(this.TextInternal) && !this.TextInternal.Contains(this.NegativeSign.Token))
            {
                // Dispatcher makes so that Value is updated first, then the negativity change.
                Dispatcher.BeginInvoke(new Action(() =>
                    {
                        this.ToggleNegativeSignKey();
                        this.SelectionStart = nextSelectionStart;
                    }));
            }
        }

        private void TryClearAll()
        {
            if (this.TextInternal.Length == this.SelectionLength && this.ClearCommand != null)
            {
                this.ClearCommand.Execute(null);
                return;
            }
        }
    }
}
