using System;
using System.Net;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using Telerik.Windows.Diagrams.Core;

namespace CustomTools_SL
{
    public class ConnectShapesTool : ToolBase, IKeyboardListener
    {
        private const string ToolName = "ConnectShapes Tool";
        private RadDiagram diagram;

        public ConnectShapesTool()
            : base(ConnectShapesTool.ToolName)
        { }

        private RadDiagram Diagram
        {
            get
            {
                if (this.diagram == null)
                    this.diagram = this.Graph as RadDiagram;

                return this.diagram;
            }
        }

        public bool KeyDown(KeyArgs key)
        {
            if (key.Key == Key.Space && this.ToolService.IsControlDown)
            {
                if (this.Diagram != null && this.Diagram.SelectedItems.Count() > 1)
                {
                    var sourceContainer = this.Graph.GetContainerFromItem(this.Diagram.SelectedItem) as RadDiagramShape;
                    foreach (var item in this.Diagram.SelectedItems)
                    {
                        var targetContainer = this.Graph.GetContainerFromItem(item) as RadDiagramShape;
                        if (this.AreValidShapes(sourceContainer, targetContainer))
                        {
                            this.Graph.AddConnection(new RadDiagramConnection()
                            {
                                Source = sourceContainer,
                                SourceConnectorPosition = "Auto",
                                Target = targetContainer,
                                TargetConnectorPosition = "Auto",
                                TargetCapType = CapType.Arrow2
                            });
                        }
                    }

                    return true;
                }
            }

            return false;
        }

        private bool AreValidShapes(RadDiagramShape source, RadDiagramShape target)
        {
            if (source == target) return false;

            return !this.Graph.GetConnectionsForShape(source).Intersect(this.Graph.GetConnectionsForShape(target)).Any();
        }

        public bool KeyUp(KeyArgs key)
        {
            return false;
        }
    }
}
