using System.Windows;
using Telerik.Windows.Documents.Fixed.UI.Extensibility;

namespace CustomFindDialog
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            FindDialog findDialog = new FindDialog();
            findDialog.Owner = this;
            ExtensibilityManager.RegisterFindDialog(findDialog);
            InitializeComponent();
        }
    }
}
