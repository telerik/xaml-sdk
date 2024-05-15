using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using PdfViewerCustomSaveCommand.Commands;

namespace PdfViewerCustomSaveCommand
{
    public partial class MainPage : UserControl
    {
        public CustomCommands Commands
        {
            get { return (CustomCommands)GetValue(CommandsProperty); }
            set { SetValue(CommandsProperty, value); }
        }

        public static readonly DependencyProperty CommandsProperty =
            DependencyProperty.Register("Commands", typeof(CustomCommands), typeof(MainPage), new PropertyMetadata(null));

        public MainPage()
        {
            InitializeComponent();
            this.Commands = new CustomCommands(this.pdfViewer);
        }

        private void tbCurrentPage_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                if (e.Key == System.Windows.Input.Key.Enter)
                {
                    textBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                }
            }
        }

        private void tbFind_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                this.pdfViewer.CommandDescriptors.FindCommandDescriptor.Command.Execute(this.tbFind.Text);
                this.btnPrev.Visibility = System.Windows.Visibility.Visible;
                this.btnNext.Visibility = System.Windows.Visibility.Visible;
            }
        }
    }
}
