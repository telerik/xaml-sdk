using System;
using System.Net;
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
    public class ShapeTool : ToolBase, IMouseListener
    {
        private const string ToolName = "Shape Tool";

        private Point startPoint;
        private bool isShiftDown;
        private Rect rectangleTransformed;

        public ShapeTool()
            : base(ShapeTool.ToolName)
        { }

        public bool MouseDoubleClick(PointerArgs e)
        {
            return false;
        }

        public bool MouseDown(PointerArgs e)
        {
            this.isShiftDown = (Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift;
            if (this.isShiftDown)
            {
                this.ToolService.ActivateTool(ShapeTool.ToolName);
                this.startPoint = e.TransformedPoint;
                return true;
            }

            return false;
        }

        public bool MouseMove(PointerArgs e)
        {
            if (this.IsActive)
            {
                var width = Math.Abs(this.startPoint.X - e.TransformedPoint.X);
                var height = Math.Abs(this.startPoint.Y - e.TransformedPoint.Y);
                var x = Math.Min(this.startPoint.X, e.TransformedPoint.X);
                var y = Math.Min(this.startPoint.Y, e.TransformedPoint.Y);
                this.rectangleTransformed = new Rect(x, y, width, height);

                this.Graph.UpdateRectSelection(this.rectangleTransformed);

                return true;
            }

            return false;
        }

        public bool MouseUp(PointerArgs e)
        {
            if (this.IsActive)
            {
                this.Graph.AddShape(new RadDiagramShape()
                {
                    Width = this.rectangleTransformed.Width,
                    Height = this.rectangleTransformed.Height,
                    Position = this.rectangleTransformed.TopLeft()
                }, isUndoable: true);

                this.Graph.UpdateRectSelection(Rect.Empty);
                this.ToolService.ActivatePrimaryTool();
            }

            return false;
        }
    }
}
