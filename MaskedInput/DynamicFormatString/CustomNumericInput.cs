using System;
using System.Text;
using System.Windows.Input;
using Telerik.Windows.Controls;

namespace DynamicFormatString
{
    public class CustomNumericInput : RadMaskedNumericInput
    {
        private int decimalNum = 1;

        public int MaxDigitsOnRight
        {
            get;
            set;
        }

        public CustomNumericInput()
        {
            this.FormatString = "n" + this.decimalNum;
            this.ValueChanged += (s, e) => this.UpdateFormatString();
        }

#if WPF
        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(this.Mask) && (e.Key == Key.Delete || e.Key == Key.Back))
            {
                if (this.Text.Length > this.SelectionStart &&
                    this.SelectionStart > this.Text.IndexOf(this.DecimalSeparator.Token))
                {
                    this.PerformCustomDigitDelete();
                    e.Handled = true;
                }
            }

            base.OnPreviewKeyDown(e);
        }
#else
        protected override void HandleDeleteKeyNoMask()
        {
            if (this.SelectionStart > this.Text.IndexOf(this.Culture.NumberFormat.NumberDecimalSeparator))
            {
                this.PerformCustomDigitDelete();
            }
            else
            {
                base.HandleDeleteKeyNoMask();
            }
        }

        protected override void HandleBackKeyNoMask()
        {
            if (this.SelectionStart > this.Text.IndexOf(this.Culture.NumberFormat.NumberDecimalSeparator))
            {
                this.PerformCustomDigitDelete();
            }
            else
            {
                base.HandleBackKeyNoMask();
            }
        }
#endif

        protected override void OnKeyDown(KeyEventArgs e)
        {
            string stringKey = e.Key.ToString();
            bool isDigit = stringKey.StartsWith("D") || stringKey.StartsWith("NumPad");
            bool isLastPos = this.Text.Length == this.SelectionStart;

            if (isDigit && isLastPos)
            {
                if (this.MaxDigitsOnRight > 0 && this.MaxDigitsOnRight == this.decimalNum)
                    return;

                this.FormatString = "n" + (++this.decimalNum);
            }

            base.OnKeyDown(e);
        }

        private void PerformCustomDigitDelete()
        {
            StringBuilder builder = new StringBuilder(this.Text);
            builder.Remove(this.SelectionStart, Math.Max(1, this.SelectionLength));
            this.Value = double.Parse(builder.ToString());
            this.UpdateFormatString();
        }

        private void UpdateFormatString()
        {
            int decimalIndex = this.Value.ToString().IndexOf(this.DecimalSeparator.Token);
            if (decimalIndex != -1)
            {
                string decimalString = this.Value.ToString().Substring(decimalIndex);
                if (decimalString.Length > 1)
                {
                    this.decimalNum = decimalString.Length - 1;
                    this.FormatString = "n" + this.GetCoercedFormatStringNumber();
                }
            }
        }

        private int GetCoercedFormatStringNumber()
        {
            if (this.MaxDigitsOnRight > 0)
            {
                return Math.Min(this.decimalNum, this.MaxDigitsOnRight);
            }
            else
            {
                return Math.Min(15, this.decimalNum);
            }
        }
    }
}
