using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Diagrams;
using Telerik.Windows.Diagrams.Core;

namespace CustomConnectorsTool
{
    public class CustomConnectorAddTool : ToolBase, IMouseListener
    {
        private const string ToolName = "CustomConnectorsAdd Tool";
        private bool areCtrlShiftDown;
        private RadDiagram diagram;
        private IShape lastUsedShape;

        public CustomConnectorAddTool()
            : base(CustomConnectorAddTool.ToolName)
        { }

        private IHitTestService HitService
        {
            get
            {
                return this.Diagram.ServiceLocator.GetService<IHitTestService>();
            }
        }

        private RadDiagram Diagram
        {
            get
            {
                if (this.diagram == null)
                    this.diagram = this.Graph as RadDiagram;

                return this.diagram;
            }
        }

        public bool MouseDoubleClick(PointerArgs e)
        {
            return false;
        }

        public bool MouseDown(PointerArgs e)
        {
            this.areCtrlShiftDown = (Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift &&
                                    (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control;

            if (this.areCtrlShiftDown)
            {
                RadDiagramShape topShape = this.HitService.GetTopItemNearPoint(e.Point, 0) as RadDiagramShape;
                if (topShape != null)
                {
                    RadDiagramConnector connector = new RadDiagramConnector() { }; 
                    connector.Name = "NewConnector" + connector.GetHashCode().ToString();

                    double xRatio = (e.Point.X - topShape.Bounds.X) / topShape.Width;
                    double yRatio = (e.Point.Y - topShape.Bounds.Y) / topShape.Height;

                    connector.Offset = new Point(xRatio, yRatio);
                    topShape.Connectors.Add(connector);

                    this.lastUsedShape = topShape;
                    return true;
                }
            }
            return false;
        }

        public bool MouseMove(PointerArgs e)
        {
            return false;
        }

        public bool MouseUp(PointerArgs e)
        {
            if (areCtrlShiftDown && lastUsedShape != null)
            {
                this.diagram.SelectedItems.OfType<IDiagramItem>().ToList().ForEach(x => x.IsSelected = false);
                lastUsedShape.IsSelected = true;
                return true;
            }
            return false;
        }
    }
}
