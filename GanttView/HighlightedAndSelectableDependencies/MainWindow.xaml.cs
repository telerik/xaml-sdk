using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Threading;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GanttView;

namespace HighlightedAndSelectableDependencies
{
    public partial class MainWindow : Window
    {
        private CustomDependency currentSelectedDependency = null;

        public MainWindow()
        {
            EventManager.RegisterClassHandler(typeof(CustomRelationContainer), RelationContainer.PreviewMouseLeftButtonUpEvent, new MouseButtonEventHandler(OnContainerClick), true);
            EventManager.RegisterClassHandler(typeof(Path), Path.MouseLeftButtonDownEvent, new MouseButtonEventHandler(OnPathClick), true);
            InitializeComponent();
            this.DataContext = new ViewModel();
        }

        private void OnContainerClick(object sender, MouseButtonEventArgs e)
        {
            var container = sender as CustomRelationContainer;
            container.IsHitTestVisible = false;

            Action action = () =>
            {
                MouseSimulator.ClickLeftMouseButton();
                container.IsHitTestVisible = true;
            };

            this.Dispatcher.BeginInvoke(action, DispatcherPriority.Background);
        }

        private void OnPathClick(object sender, MouseButtonEventArgs e)
        {
            var path = sender as Path;
            var relationContainer = path.ParentOfType<CustomRelationContainer>();
            if (relationContainer != null)
            {
                this.SetCurrentRelationContainer((relationContainer.DataItem as CustomRelationInfo).Dependency as CustomDependency);
            }
        }

        private void SetCurrentRelationContainer(CustomDependency newSelection)
        {
            if (currentSelectedDependency == newSelection)
            {
                return;
            }

            if (currentSelectedDependency != null)
            {
                currentSelectedDependency.IsSelected = false;
            }

            if (newSelection != null)
            {
                newSelection.IsSelected = true;
            }

            currentSelectedDependency = newSelection;
        }
    }
}
