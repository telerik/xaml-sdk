using System.Windows;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.RichTextBoxUI.Dialogs;
using Telerik.Windows.Documents.UI.Extensibility;

namespace CustomInsertTableDialogDemo
{
    [CustomInsertTableDialog]
    public partial class CustomInsertTableDialog : RadRichTextBoxWindow, IInsertTableDialog
    {
        InsertTableDialogContext context;

        public CustomInsertTableDialog()
        {
            InitializeComponent();
        }

        public void ShowDialog(InsertTableDialogContext context)
        {
            this.context = context;
            this.SetOwner(context.Owner);
            this.ShowDialog();
        }

        private void buttonOK_Click(object sender, RoutedEventArgs e)
        {
            if (!this.radNumRows.Value.HasValue)
            {
                this.ShowAlert((int)radNumRows.Minimum, (int)radNumRows.Maximum);
                return;
            }

            int rows = (int)radNumRows.Value.Value;

            if (!radNumColumns.Value.HasValue)
            {
                this.ShowAlert((int)radNumColumns.Minimum, (int)radNumColumns.Maximum);
                return;
            }

            int columns = (int)radNumColumns.Value.Value;

            this.context.InsertTableCallback(rows, columns, false);
            this.Close();
        }

        private void ShowAlert(int minValue, int maxValue)
        {
            string message = string.Format(LocalizationManager.GetString("Documents_InsertTableDialog_NumberMustBeBetween"), minValue, maxValue);
            RadWindow.Alert(new DialogParameters() { Header = this.Header, Content = message });
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        protected override void OnClosed(WindowClosedEventArgs args)
        {
            base.OnClosed(args);

            this.Owner = null;
        }
    }
}
