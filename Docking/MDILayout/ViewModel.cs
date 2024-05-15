using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Docking;

namespace MDILayout
{
    public class ViewModel : ViewModelBase
    {
        #region Fields
        #endregion

        #region Properties
        public ObservableCollection<WindowViewModel> Windows { get; set; }
        #endregion

        #region Ctor
        static ViewModel()
        {
            CommandManager.RegisterClassCommandBinding(typeof(ToolWindow), new CommandBinding(WindowCommands.Minimize, OnMinimized, (s, e) => e.CanExecute = true));
        }

        public ViewModel()
        {
            this.Windows = new ObservableCollection<WindowViewModel>();
        }
        #endregion

        #region Methods

        private static void OnMinimized(object s, ExecutedRoutedEventArgs e)
        {
            var pane = ((e.OriginalSource as FrameworkElement).ParentOfType<ToolWindow>().Content as RadSplitContainer).EnumeratePanes().FirstOrDefault();
            if (pane != null)
            {
                var viewModel = pane.DataContext as WindowViewModel;
                viewModel.IsClosedClicked = true;
                viewModel.IsMinimized = true;
            }
        }
        #endregion
    }
}
