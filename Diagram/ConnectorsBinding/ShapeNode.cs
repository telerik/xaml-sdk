using System.Collections.ObjectModel;
using Telerik.Windows.Controls.Diagrams.Extensions.ViewModels;
using Telerik.Windows.Diagrams.Core;

namespace ConnectorsBinding
{
    public class ShapeNode : NodeViewModelBase
    {
        public ShapeNode()
        {
            this.ShapeConnectors = new ObservableCollection<ShapeConnector>();
        }

        public ObservableCollection<ShapeConnector> ShapeConnectors { get; set; }
    }
}
