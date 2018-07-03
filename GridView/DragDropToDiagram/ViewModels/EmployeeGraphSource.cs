using System.Windows;
using Telerik.Windows.Controls.Diagrams.Extensions.ViewModels;

namespace DragDropToDiagram_WPF.ViewModels
{
    public class EmployeeGraphSource : ObservableGraphSourceBase<NodeViewModelBase, LinkViewModelBase<NodeViewModelBase>>
    {
        public EmployeeGraphSource()
        {
            Division divisionOne = new Division() { Position = new Point(100, 100), Content = "Sales" };
            Division divisionTwo = new Division() { Position = new Point(720, 100), Content = "Marketing" };
            Division divisionThree = new Division() { Position = new Point(400, 420), Content = "Development" };

            this.AddNode(divisionOne);
            this.AddNode(divisionTwo);
            this.AddNode(divisionThree);
        }
    }
}
