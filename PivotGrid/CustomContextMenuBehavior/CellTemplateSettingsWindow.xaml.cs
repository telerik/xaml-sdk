using System.Windows;
using Telerik.Windows.Controls;

namespace CustomContextMenuBehavior
{
    /// <summary>
    /// Interaction logic for CellTemplateSettingsWindow.xaml
    /// </summary>
    public partial class CellTemplateSettingsWindow : RadWindow
    {
        private bool dialogResult;

        public CellTemplateSettingsWindow()
        {
            InitializeComponent();
        }

        protected override void OnClosed(WindowClosedEventArgs args)
        {
            var newArgs = new WindowClosedEventArgs { DialogResult = this.dialogResult, PromptResult = this.Numeric.Value.ToString() };
            base.OnClosed(newArgs);
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            this.dialogResult = true;
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.dialogResult = false;
            this.Close();
        }
    }
}
