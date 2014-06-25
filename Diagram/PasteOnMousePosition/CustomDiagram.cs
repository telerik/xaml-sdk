using System;
using System.Collections.Generic;
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
        private bool isPastePErformed = false;
        private List<IDiagramItem> pastedItems;

        public CustomDiagram()
        {
            this.ItemsChanged += this.OnItemsChanged;
        }

        public override void Paste()
        {
            this.isPastePErformed = true;
            this.pastedItems = new List<IDiagramItem>();
            IConnector activeConnector = this.GetHoveredConnector();

            // Base.Paste() will perform the actual paste operation which will trigger ItemsChanged.
            base.Paste();

            var groupOffset = this.pastedItems.GetEnclosingBounds().TopLeft();
            foreach (var item in this.pastedItems)
            {
                Point mousePos = this.GetTransformedPoint(Mouse.GetPosition(this));
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
            this.isPastePErformed = false;
        }

        private void OnItemsChanged(object sender, Telerik.Windows.Controls.Diagrams.DiagramItemsChangedEventArgs e)
        {
            if (this.isPastePErformed)
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
