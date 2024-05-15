using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Docking;

namespace MDILayout
{
    /// <summary>
    /// Interaction logic for Example.xaml
    /// </summary>
    public partial class Example : UserControl
    {
        public Example()
        {
            InitializeComponent();

            for (int i = 1; i <= 3; i++)
            {
                var newWindowModel = new WindowViewModel() { Header = "Pane " + i.ToString(), IsMinimized = false };
                (this.DataContext as ViewModel).Windows.Add(newWindowModel);
                AddPaneForViewModel(this.dock, newWindowModel, new Size(200, 200), new Point(i * 200, 0));
            }
        }

        private void RadButton_Click(object sender, RoutedEventArgs e)
        {
            var newWindowModel = new WindowViewModel() { Header = "New Pane", IsMinimized = false };
            (this.DataContext as ViewModel).Windows.Add(newWindowModel);
            AddPaneForViewModel(this.dock, newWindowModel, null, null);
        }

        private static void AddPaneForViewModel(RadDocking dock, WindowViewModel model, Size? floatingSize, Point? floatingLocation)
        {
            var pane = new RadPane { DataContext = model };
            pane.SetBinding(RadPane.IsHiddenProperty, new Binding("IsHidden") { Mode = BindingMode.TwoWay });
            pane.SetBinding(RadPane.HeaderProperty, new Binding("Header"));
            var group = new RadPaneGroup { Items = { pane } };
            var splitContainer = new RadSplitContainer { Items = { group } };
            splitContainer.InitialPosition = DockState.FloatingOnly;
            if (floatingSize.HasValue)
            {
                RadDocking.SetFloatingSize(splitContainer, floatingSize.Value);
            }

            if (floatingLocation.HasValue)
            {
                RadDocking.SetFloatingLocation(splitContainer, floatingLocation.Value);
            }

            dock.Items.Add(splitContainer);
        }

        private void dock_Show(object sender, StateChangeEventArgs e)
        {
            var pane = e.Panes.FirstOrDefault();
            if (pane != null)
            {
                pane.Focus();
            }
        }
    }
}
