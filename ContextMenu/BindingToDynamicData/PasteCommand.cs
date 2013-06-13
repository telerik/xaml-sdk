using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BindingToDynamicData
{
    public class PasteCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            var textBox = parameter as TextBox;

            if (Clipboard.ContainsText() && textBox != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            var textToPaste = Clipboard.GetText();
            var textBox = parameter as TextBox;
            var text = textBox.Text;
            text = text.Remove(textBox.SelectionStart, textBox.SelectionLength);
            text = text.Insert(textBox.SelectionStart, textToPaste);
            textBox.Text = text;
            textBox.Focus();
        }
    }
}
