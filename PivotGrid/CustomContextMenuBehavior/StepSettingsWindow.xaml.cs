using System.Windows;
using Telerik.Windows.Controls;

namespace CustomContextMenuBehavior
{
    public partial class StepSettingsWindow : RadWindow
    {
        private bool dialogResult;
        internal double Step { get; set; }

        public StepSettingsWindow(double step)
        {
            InitializeComponent();
            this.Numeric.Value = step;
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
