using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Diagrams;
using Telerik.Windows.Diagrams.Core;

namespace CustomConnectors
{
	public partial class Example : UserControl
	{
		public Example()
		{
			InitializeComponent();
			this.diagram.Loaded += new RoutedEventHandler(DiagramLoaded);
		}

		private void DiagramLoaded(object sender, RoutedEventArgs e)
		{
			this.diagram.Shapes.ToList().ForEach(x =>
			{
				var connectorUpRight = new RadDiagramConnector() { Offset = new Point(1, 0.25), Name = x.Name + "Connector1Right" };
				var connectorDownRight = new RadDiagramConnector() { Offset = new Point(1, 0.75), Name = x.Name + "Connector2Right" };
				var connectorLeftUp = new RadDiagramConnector() { Offset = new Point(0, 0.25), Name = x.Name + "Connector3Left" };
				var connectorLeftDown = new RadDiagramConnector() { Offset = new Point(0, 0.75), Name = x.Name + "Connector4Left" };

				x.Connectors.Add(connectorUpRight);
				x.Connectors.Add(connectorDownRight);
				x.Connectors.Add(connectorLeftUp);
				x.Connectors.Add(connectorLeftDown);
			});

			var shape = new RadDiagramShape();
			var connector = new RadDiagramConnector() { Offset = new Point(1, 0.5), Name = "CustoMConnector1" };
			shape.Connectors.Add(connector);
		}

		private void AddConnectionsClick(object sender, RoutedEventArgs e)
		{
			var connection = this.diagram.AddConnection(this.diagram.Shapes[0], this.diagram.Shapes[1], "match1shapeConnector1Right", "match2shapeConnector3Left");
			(connection as RadDiagramConnection).Stroke = new SolidColorBrush() { Color = Color.FromArgb(255, 255, 116, 2) };

			var connection2 = this.diagram.AddConnection(this.diagram.Shapes[1], this.diagram.Shapes[2], "match2shapeConnector1Right", "match3shapeConnector4Left");
			(connection2 as RadDiagramConnection).Stroke = new SolidColorBrush() { Color = Color.FromArgb(255, 255, 116, 2) };


			var connection3 = this.diagram.AddConnection(this.diagram.Shapes[0], this.diagram.Shapes[4], "match1shapeConnector2Right", "match5shapeConnector3Left");
			(connection3 as RadDiagramConnection).Stroke = new SolidColorBrush() { Color = Color.FromArgb(255, 242, 8, 8) };

			var connection4 = this.diagram.AddConnection(this.diagram.Shapes[4], this.diagram.Shapes[5], "match5shapeConnector1Right", "match6shapeConnector3Left");
			(connection4 as RadDiagramConnection).Stroke = new SolidColorBrush() { Color = Color.FromArgb(255, 242, 8, 8) };

			var connection5 = new RadDiagramConnection() { Stroke = new SolidColorBrush() { Color = Color.FromArgb(255, 30, 30, 27) } };
			connection5.Attach(this.diagram.Shapes[3].Connectors[5], this.diagram.Shapes[1].Connectors[8]);
			this.diagram.Items.Add(connection5);

			var connection6 = new RadDiagramConnection() { Stroke = new SolidColorBrush() { Color = Color.FromArgb(255, 30, 30, 27) } };
			connection6.Attach(this.diagram.Shapes[1].Connectors[6], this.diagram.Shapes[5].Connectors[8]);
			this.diagram.Items.Add(connection6);

			var connection7 = new RadDiagramConnection() { Stroke = new SolidColorBrush() { Color = Color.FromArgb(255, 23, 156, 72) } };
			connection7.Attach(this.diagram.Shapes[3].Connectors[6], this.diagram.Shapes[4].Connectors[8]);
			this.diagram.Items.Add(connection7);

			var connection8 = new RadDiagramConnection() { Stroke = new SolidColorBrush() { Color = Color.FromArgb(255, 23, 156, 72) } };
			connection8.Attach(this.diagram.Shapes[4].Connectors[6], this.diagram.Shapes[2].Connectors[7]);
			this.diagram.Items.Add(connection8);

			SetConnectorsCenterPoints();
			(sender as RadButton).Visibility = Visibility.Collapsed;
		}

		private void SetConnectorsCenterPoints()
		{
			RadDiagramConnection.SetConnectorCenterPoint(this.diagram.Shapes[0] as DependencyObject, new Point(0.5, 0.25));
			RadDiagramConnection.SetConnectorCenterPoint(this.diagram.Shapes[1] as DependencyObject, new Point(0.5, 0.25));
			RadDiagramConnection.SetConnectorCenterPoint(this.diagram.Shapes[2] as DependencyObject, new Point(0.5, 0.25));
			RadDiagramConnection.SetConnectorCenterPoint(this.diagram.Shapes[3] as DependencyObject, new Point(0.5, 0.25));
			RadDiagramConnection.SetConnectorCenterPoint(this.diagram.Shapes[4] as DependencyObject, new Point(0.5, 0.25));
			RadDiagramConnection.SetConnectorCenterPoint(this.diagram.Shapes[5] as DependencyObject, new Point(0.5, 0.25));
		}
	}
}
