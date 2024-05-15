using System.Windows;
using System.Windows.Controls;

namespace CustomFontPropertiesDialog
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void ShowFontPropertiesDialog_Click(object sender, RoutedEventArgs e)
        {
            this.radRichTextBox.Commands.ShowFontPropertiesDialogCommand.Execute();
        }
    }
}
