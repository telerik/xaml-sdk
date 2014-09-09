using HelpFunctionality;
using System.Windows;
using System.Windows.Input;
using Telerik.Windows.Controls;

namespace HelpFunctionality_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RadRibbonWindow
    {
        static MainWindow()
        {
            IsWindowsThemeEnabled = false;
        }

        public MainWindow()
        {
            InitializeComponent();
            OpenHelpPageCommand = new DelegateCommand(ExecuteOpenHelpPage);
            this.DataContext = OpenHelpPageCommand;
            this.InputBindings.Add(new KeyBinding(this.OpenHelpPageCommand, new KeyGesture(Key.F1)));
        }

        private void ExecuteOpenHelpPage(object obj)
        {
            var helpWindow = MyHelpWindow.Instance;
            helpWindow.Show();
            helpWindow.Focus();
        }

        public DelegateCommand OpenHelpPageCommand { get; set; }
    }
}
