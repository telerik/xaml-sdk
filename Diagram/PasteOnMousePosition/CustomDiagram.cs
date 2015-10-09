using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Diagrams;
using Telerik.Windows.Diagrams.Core;

namespace DiagramCustomPaste
{
    public class CustomDiagram : RadDiagram
    {
        private readonly List<IDiagramItem> pastedItems = new List<IDiagramItem>();
        private List<Point> offsetsInSelection;
        private Point mousePos;
        private Point groupOffset;
        private bool isPasting = false;

        public CustomDiagram()
        {
            this.ItemsChanged += this.OnItemsChanged;
        }

        public override void Paste()
        {
            this.pastedItems.Clear();

            this.SelectedItem = null;

            // Base.Paste() will perform the actual paste operation which will trigger ItemsChanged.
            this.isPasting = true;
            base.Paste();
            this.isPasting = false;

            var lastPastedItem = this.pastedItems[this.pastedItems.Count - 1] as RadDiagramItem;
            if (lastPastedItem == null)
                return;

            lastPastedItem.Loaded += (oo, ee) =>
            {
                // Calculate the GroupOffset of All Selected Items that are about to be pasted.
                this.groupOffset = this.CalculateGroupOffset();
                this.CalculateRelativeOffsets();

                mousePos = this.GetTransformedPoint(Mouse.GetPosition(this));

                this.RepositionPastedItems();
            }; 
        }

        private Point CalculateGroupOffset()
        {
            var topLevelShapes = new List<IDiagramItem>();
            foreach (var item in this.pastedItems)
            {
                var container = this.ContainerGenerator.ContainerFromItem(item) as IDiagramItem;
                if (container != null && container.ParentContainer == null)
                {
                    topLevelShapes.Add(container);
                }
            }
            return topLevelShapes.GetEnclosingBounds().TopLeft();
        }

        // Calculates relative offsets of the items in the pasted group (if pasted items are more than 1).
        private void CalculateRelativeOffsets()
        {
            this.offsetsInSelection = new List<Point>();
            foreach (var item in this.pastedItems)
            {
                var container = this.ContainerGenerator.ContainerFromItem(item) as IDiagramItem;
                Point offset = new Point(container.Position.X - this.groupOffset.X, container.Position.Y - groupOffset.Y);

                this.offsetsInSelection.Add(offset);
            }
        }

        // Gets the containers of the pasted objects and adds them in the pastedItems list.
        private void OnItemsChanged(object sender, Telerik.Windows.Controls.Diagrams.DiagramItemsChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add && this.isPasting)
            {
                e.NewItems.ToList().ForEach(x => this.pastedItems.Add(this.ContainerGenerator.ContainerFromItem(x)));
            }
        }

        // Repositions pasted shapes and connections.
        private void RepositionPastedItems()
        {
            for (int i = 0; i < this.pastedItems.Count; i++)
            {
                var shape = this.pastedItems[i] as IShape;
                Point prevOffset = this.offsetsInSelection[i];

                bool isShapeContainer = shape != null && shape is IContainerShape;
                bool isNotContainerChild = shape != null && shape.ParentContainer == null;

                if (isShapeContainer || isNotContainerChild)
                {
                    shape.Position = new Point() { X = mousePos.X + prevOffset.X, Y = mousePos.Y + prevOffset.Y };
                }
                else
                {
                    var connection = this.pastedItems[i] as IConnection;
                    if (connection == null) continue;

                    double endPointOffsetX = connection.EndPoint.X - connection.StartPoint.X;
                    double endPointOffsetY = connection.EndPoint.Y - connection.StartPoint.Y;

                    IConnector hoveredConnector = this.GetHoveredConnector();

                    if (hoveredConnector != null)
                    {
                        // Pasting on hovered connector will attach the connection to the shape.
                        connection.Source = hoveredConnector.Shape;
                        connection.SourceConnectorPosition = hoveredConnector.Name;
                    }
                    else
                    {
                        // Pasting anywhere elese - start point of the connection will be udner mouse pointer.
                        connection.StartPoint = new Point() { X = mousePos.X + prevOffset.X, Y = mousePos.Y + prevOffset.Y };
                    }
                    connection.EndPoint = new Point() { X = connection.StartPoint.X + endPointOffsetX, Y = connection.StartPoint.Y + endPointOffsetY };
                }
            }
        }

        // Gets the currently active connectors.
        private IConnector GetHoveredConnector()
        {
            foreach (var shape in this.Shapes)
            {
                foreach (var connector in shape.Connectors)
                {
                    if (connector.IsMouseOver)
                    {
                        return connector;
                    }
                }
            }
            return null;
        }
    }
}
