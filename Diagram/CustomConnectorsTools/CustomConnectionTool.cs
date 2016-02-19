using System;
using System.Linq;
using System.Windows;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Diagrams;
using Telerik.Windows.Diagrams.Core;

namespace CustomConnectorsTool
{
    public class CustomConnectionTool : ConnectionTool
    {
        private RadDiagramConnector lastCustomConnector;

        private RadDiagram diagram;
        private RadDiagram Diagram
        {
            get
            {
                if (this.diagram == null)
                    this.diagram = this.Graph as RadDiagram;

                return this.diagram;
            }
        }

        private IHitTestService HitService
        {
            get
            {
                return this.Diagram.ServiceLocator.GetService<IHitTestService>();
            }
        }

        public override bool MouseUp(PointerArgs e)
        {
            RadDiagramShape shape = this.HitService.GetTopItemNearPoint(e.Point, 0) as RadDiagramShape;

            // Normal execution if we release the mouse over connector.
            if (shape != null && this.IsActiveConnectorUnderPoint(shape, e.Point))
            {
                return base.MouseUp(e);
            }

            // Add custom connector.
            if (shape != null && this.InitialPoint != null && shape != this.HitItem && this.HitItem != null)
            {
                Point intersectPoint = new Point();
                Utils.IntersectionPointOnRectangle(shape.Bounds, this.InitialPoint.Value, e.Point, ref intersectPoint);
                lastCustomConnector = new RadDiagramConnector() { };
                lastCustomConnector.Name = "CustomConnector" + lastCustomConnector.GetHashCode().ToString();

                double xRatio = (intersectPoint.X - shape.Bounds.X) / shape.Width;
                double yRatio = (intersectPoint.Y - shape.Bounds.Y) / shape.Height;

                lastCustomConnector.Offset = new Point(xRatio, yRatio);
                shape.Connectors.Add(lastCustomConnector);
            }

            // This will create the new connection.
            base.MouseUp(e);

            // Repositions the added connection to the custom connector we created.
            if (this.ActiveConnection != null && this.lastCustomConnector != null)
            {
                this.ActiveConnection.Attach(this.ActiveConnection.SourceConnectorResult, lastCustomConnector);
            }
            return false;
        }

        private bool IsActiveConnectorUnderPoint(RadDiagramShape shape, Point point)
        {
            foreach (var conn in shape.Connectors.Where(x => x.IsActive == true))
            {
                var distance = conn.AbsolutePosition.Distance(point);
                if (distance < DiagramConstants.ConnectorHitTestRadius)
                {
                    return true;
                }            
            }
            return false;
        }
    }
}