using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        protected override void OnKeyDown(System.Windows.Input.KeyEventArgs e)
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

        protected override void HandleDeleteKeyNoMask()
        {
            if (this.SelectionStart > this.Text.IndexOf(this.Culture.NumberFormat.NumberDecimalSeparator))
            {
                this.PerformSingleDigitDelete();
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
                this.PerformSingleDigitDelete();
            }
            else
            {
                base.HandleBackKeyNoMask();
            }
        }

        private void PerformSingleDigitDelete()
        {
            StringBuilder builder = new StringBuilder(this.Text);
            builder.Remove(this.SelectionStart, Math.Max(1, this.SelectionLength));
            this.Value = double.Parse(builder.ToString());
            this.UpdateFormatString();
        }

        private void UpdateFormatString()
        {
            int decimalIndex = this.Value.ToString().IndexOf(this.Culture.NumberFormat.NumberDecimalSeparator);
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
