using System;
using System.Linq;
using Telerik.Windows.Controls;

namespace MinusKeyCustomizations
{
    public class NumericInputMinusClearsSelection : RadMaskedNumericInput
    {
        protected override bool HandleSubstractKey()
        {
            // Show Negative symnbol when all is selected.
            if (!string.IsNullOrEmpty(this.Text) && this.Text.Length == this.SelectionLength)
            {
                if (this.ClearCommand != null)
                {
                    this.ClearCommand.Execute(null);                
                }

                this.ToggleNegativeSignKey();
                return true;
            }

            return base.HandleSubstractKey();
        }
    }
}
