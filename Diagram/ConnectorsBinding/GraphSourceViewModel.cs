using Telerik.Windows.Controls.Diagrams;
using Telerik.Windows.Controls.Diagrams.Extensions.ViewModels;

namespace ConnectorsBinding
{
    public class GraphSourceViewModel : ObservableGraphSourceBase<NodeViewModelBase, LinkViewModelBase<NodeViewModelBase>>
    {
        public GraphSourceViewModel()
        {
            ShapeNode shape = new ShapeNode()
            {
                Content = "Shape 1",
                Position = new System.Windows.Point(320, 140),
            };

            ShapeConnector shape1Connector1 = new ShapeConnector()
            {
                Name = "Shape1Connector1",
                Position = new System.Windows.Point(1, 0.25),
            };

            ShapeConnector shape1Connector2 = new ShapeConnector()
            {
                Name = "Shape1Connector2",
                Position = new System.Windows.Point(1, 0.75),
            };

            shape.ShapeConnectors.Add(shape1Connector1);
            shape.ShapeConnectors.Add(shape1Connector2);

            this.AddNode(shape);
        }
    }

}
