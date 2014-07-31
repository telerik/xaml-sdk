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
        private bool isPasting = false;

        public CustomDiagram()
        {
            this.ItemsChanged += this.OnItemsChanged;
        }

        public override void Paste()
        {
            this.pastedItems.Clear();
            IConnector hoveredConnector = this.GetHoveredConnector();
            Point mousePos = this.GetTransformedPoint(Mouse.GetPosition(this));

            // Base.Paste() will perform the actual paste operation which will trigger ItemsChanged.
            this.isPasting = true;
            base.Paste();
            this.isPasting = false;

            var groupOffset = this.pastedItems.GetEnclosingBounds().TopLeft();
            foreach (var item in this.pastedItems)
            {
                Point offset = new Point(item.Position.X - groupOffset.X, item.Position.Y - groupOffset.Y);

                var shape = item as IShape;
                if (shape != null)
                {
                    shape.Position = new Point() { X = mousePos.X + offset.X, Y = mousePos.Y + offset.Y };
                }
                else
                {
                    var connection = item as IConnection;
                    if (connection == null) return;

                    double endPointOffsetX = connection.EndPoint.X - connection.StartPoint.X;
                    double endPointOffsetY = connection.EndPoint.Y - connection.StartPoint.Y;

                    if (hoveredConnector != null)
                    {
                        // Pasting on hovered connector will attach the connection to the shape.
                        connection.Source = hoveredConnector.Shape;
                        connection.SourceConnectorPosition = hoveredConnector.Name;
                    }
                    else
                    {
                        // Pasting anywhere elese - start point of the connection will be udner mouse pointer.
                        connection.StartPoint = new Point() { X = mousePos.X + offset.X, Y = mousePos.Y + offset.Y };
                    }
                    connection.EndPoint = new Point() { X = connection.StartPoint.X + endPointOffsetX, Y = connection.StartPoint.Y + endPointOffsetY };
                }
            }
        }

        private void OnItemsChanged(object sender, Telerik.Windows.Controls.Diagrams.DiagramItemsChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add && this.isPasting)
            {
                e.NewItems.ToList().ForEach(x => this.pastedItems.Add(this.ContainerGenerator.ContainerFromItem(x)));
            }
        }

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
