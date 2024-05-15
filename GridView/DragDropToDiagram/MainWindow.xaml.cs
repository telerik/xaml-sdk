using DragDropToDiagram_WPF.ViewModels;
using System.Linq;
using System.Windows;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Diagrams.Extensions.ViewModels;
using Telerik.Windows.Controls.GridView;

namespace DragDropToDiagram_WPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel();
        }

        private void RadDiagram_PreviewDrop(object sender, System.Windows.DragEventArgs e)
        {
            var droppedRow = e.Data.GetData(typeof(GridViewRow));
            var employee = (droppedRow as GridViewRow).DataContext as Employee;

            NodeViewModelBase node = new NodeViewModelBase();
            node.Position = e.GetPosition(this.xDiagram);
            node.Content = employee.FirstName + " " + employee.LastName;

            var droppedUIElement = e.OriginalSource as UIElement;

            var droppedInsideContainer = droppedUIElement.ParentOfType<RadDiagramContainerShape>();

            if (droppedInsideContainer != null)
            {
                var division = droppedInsideContainer.DataContext as Division;
                node.Position = this.InsertAtPositionOfDroppedContainer(droppedInsideContainer);
                division.AddItem(node);
            }

            (this.DataContext as MainViewModel).EmployeeGraphSource.AddNode(node);
            (this.DataContext as MainViewModel).EmployeeData.Remove(employee);
        }

        private Point InsertAtPositionOfDroppedContainer(RadDiagramContainerShape container)
        {
            var childShapes = container.Items;
            if (childShapes.Count() == 0)
            {
                return new Point(container.Position.X + 20, container.Position.Y + 20);
            }
            else if (childShapes.Count() == 1)
            {
                var firstShape = childShapes[0] as NodeViewModelBase;
                return new Point(firstShape.Position.X + 120 + 20, firstShape.Position.Y);
            }
            else
            {
                var oddShape = childShapes[childShapes.Count - 2] as NodeViewModelBase;
                return new Point(oddShape.Position.X, oddShape.Position.Y + 30 + 20);
            }
        }
    }
}
