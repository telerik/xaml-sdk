using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using PdfViewerCustomSaveCommand.Commands;

namespace PdfViewerCustomSaveCommand
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public CustomCommands Commands
        {
            get { return (CustomCommands)GetValue(CommandsProperty); }
            set { SetValue(CommandsProperty, value); }
        }

        public static readonly DependencyProperty CommandsProperty =
            DependencyProperty.Register("Commands", typeof(CustomCommands), typeof(MainWindow), new PropertyMetadata(null));

        public MainWindow()
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
    }
}
