using System;
using System.Collections.Generic;
using System.Windows;
using Telerik.Windows.Controls;
using Telerik.Windows.Diagrams.Core;
using System.Linq;

namespace DiagramCustomPasteSL
{
    public class CustomDiagramSL : RadDiagram
    {
        private bool isPastePerformed = false;
        private List<IDiagramItem> pastedItems;
        private Point mousePos = default(Point);

        public CustomDiagramSL()
        {
            this.ItemsChanged += this.OnItemsChanged;
            this.MouseMove += CustomDiagram_MouseMove;
        }

        private void CustomDiagram_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.mousePos = e.GetPosition(this);
        }

        public override void Paste()
        {
            this.isPastePerformed = true;
            this.pastedItems = new List<IDiagramItem>();
            IConnector activeConnector = this.GetHoveredConnector();

            // Base.Paste() will perform the actual paste operation which will trigger ItemsChanged.
            base.Paste();

            var groupOffset = this.pastedItems.GetEnclosingBounds().TopLeft();
            foreach (var item in this.pastedItems)
            {
                Point offset = new Point()
                {
                    X = item.Position.X - groupOffset.X,
                    Y = item.Position.Y - groupOffset.Y
                };

                var shape = item as IShape;
                if (shape != null)
                    shape.Position = new Point() { X = mousePos.X + offset.X, Y = mousePos.Y + offset.Y };
                else
                {
                    var con = item as IConnection;
                    double endPointOffsetX = con.EndPoint.X - con.StartPoint.X;
                    double endPointOffsetY = con.EndPoint.Y - con.StartPoint.Y;

                    if (activeConnector != null)
                    {
                        // Pasting on activ Shape Connector will attach the connection to the shape.
                        con.Source = activeConnector.Shape;
                        con.SourceConnectorPosition = activeConnector.Name;
                    }
                    else
                    {
                        // Pasting anywhere elese - start point of the connection will be udner mouse pointer.
                        con.StartPoint = new Point() { X = mousePos.X + offset.X, Y = mousePos.Y + offset.Y };
                    }
                    con.EndPoint = new Point() { X = mousePos.X + endPointOffsetX, Y = mousePos.Y + endPointOffsetY };
                }
            }
            this.pastedItems.Clear();
            this.isPastePerformed = false;
        }

        private void OnItemsChanged(object sender, Telerik.Windows.Controls.Diagrams.DiagramItemsChangedEventArgs e)
        {
            if (this.isPastePerformed)
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
