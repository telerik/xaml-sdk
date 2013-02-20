using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Diagrams;
using Telerik.Windows.Diagrams.Core;

namespace GlidingConnector
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			InitializeComponent();
			Loaded += OnLoaded;
		}
		private readonly Effect effect = new DropShadowEffect { Opacity = .25, BlurRadius = 5 };

		private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			var shapes = new Dictionary<string, RadDiagramShape>
				{
					{ "Start", new RadDiagramShape { 
													Geometry = ShapeFactory.GetShapeGeometry(FlowChartShapeType.StartShape), 
													GlidingStyle = GlidingStyle.Ellipse, 
													Position = new Point(380, 50), 
													Background = new SolidColorBrush(Colors.Green),
													Width = 50, 
													Height = 50,
													BorderBrush = new SolidColorBrush(Colors.Transparent),
													Effect = effect
													} },
					{ "Stop", new RadDiagramShape { 
													Geometry = ShapeFactory.GetShapeGeometry(FlowChartShapeType.StartShape), 
													GlidingStyle = GlidingStyle.Ellipse, 
													Position = new Point(386, 573), 
													Background = new SolidColorBrush(Colors.Orange),
													Width = 50, 
													Height = 50,
													BorderBrush = new SolidColorBrush(Colors.Transparent),
													Effect = effect
													} },
					{ "New?", new RadDiagramShape { Geometry = ShapeFactory.GetShapeGeometry(FlowChartShapeType.DecisionShape) ,
													GlidingStyle = GlidingStyle.Diamond, 
													Position = new Point(350, 150), 
													Background = new SolidColorBrush(Colors.Orange),
													Content = "New?",
													BorderBrush = new SolidColorBrush(Colors.Transparent),
													Effect = effect
													} },
					{ "CreateCart", new RadDiagramShape {	Geometry = ShapeFactory.GetShapeGeometry(FlowChartShapeType.CreateRequestShape), 
															GlidingStyle = GlidingStyle.Rectangle, 
															Position = new Point(222,280), 
															Background = new SolidColorBrush(Colors.Blue),
															Content = "Create New",
															BorderBrush = new SolidColorBrush(Colors.Transparent),
															Effect = effect
													} },
					{ "FetchCart", new RadDiagramShape {	Geometry = ShapeFactory.GetShapeGeometry(FlowChartShapeType.CreateRequestShape), 
															GlidingStyle = GlidingStyle.Rectangle, 
															Position = new Point(478,280), 
															Background = new SolidColorBrush(Colors.Blue),
															Content = "Fetch Existing",
															BorderBrush = new SolidColorBrush(Colors.Transparent),
															Effect = effect
													} },
					{ "Database", new RadDiagramShape {	Geometry = ShapeFactory.GetShapeGeometry(FlowChartShapeType.Database1Shape), 
															GlidingStyle = GlidingStyle.Rectangle, 
															Position = new Point(725,255), 
															Background = new SolidColorBrush(Colors.LightGray),
															Content = "Oracle",
															BorderBrush = new SolidColorBrush(Colors.Transparent),
															Effect = effect,
															Opacity = .85
													} },
					{ "BizTalk", new RadDiagramShape {	Geometry = ShapeFactory.GetShapeGeometry(CommonShapeType.RoundedRectangleShape), 
															GlidingStyle = GlidingStyle.Rectangle, 
															Position = new Point(890,255), 
															Background = new SolidColorBrush(Colors.Gray),
															Content = "BizTalk",
															BorderBrush = new SolidColorBrush(Colors.Transparent),
															Effect = effect,
															Opacity = .85
													} },
					{ "Azure", new RadDiagramShape {	Geometry = ShapeFactory.GetShapeGeometry(CommonShapeType.CloudShape), 
															GlidingStyle = GlidingStyle.Rectangle, 
															Position = new Point(1070,255), 
															Background =new SolidColorBrush(Colors.Blue),
															Content = "Azure",
															BorderBrush = new SolidColorBrush(Colors.Transparent),
															Effect = effect,
															Opacity = .6
													} },
					{ "Present", new RadDiagramShape {	Geometry = ShapeFactory.GetShapeGeometry(FlowChartShapeType.DocumentShape), 
															GlidingStyle = GlidingStyle.Rectangle, 
															Position = new Point(355,432), 
															Background = new SolidColorBrush(Colors.Green),
															Content = "Present",
															BorderBrush = new SolidColorBrush(Colors.Transparent),
															Effect = effect,
															Opacity = 1
													} },
					{ "FSM", new RadDiagramShape {	Geometry = null, 
															GlidingStyle = GlidingStyle.Rectangle, 
															Position = new Point(830,400), 
															Background = new SolidColorBrush(Colors.Blue),
															Foreground = new SolidColorBrush(Colors.Black),
															Content = "FSM",
															BorderBrush = new SolidColorBrush(Colors.Transparent),
															Effect = effect,
															Opacity = 1
													} },
				};
			// add all shapes
			shapes.ForEach(s => diagram.AddShape(s.Value));

			var ctShapes = new[] { "Database", "FSM", "Azure", "BizTalk" };
			var container = new RadDiagramContainerShape
			{
				Position = new Point(692, 204),
				Width = 565,
				Height = 305,
				Content = "Backend",
				Stroke = new SolidColorBrush(Colors.Red),
				StrokeDashArray = new DoubleCollection { 1, 1 },
				Background = new SolidColorBrush(Colors.LightGray),
				Foreground = new SolidColorBrush(Colors.Gray),
				ZIndex = -5
			};
			ctShapes.ForEach(s => container.Items.Add(shapes[s]));
			diagram.AddShape(container);
			shapes["CreateCart"].RotationAngle = 315;
			shapes["FetchCart"].RotationAngle = 45;

			var c1 = diagram.AddConnection(shapes["Start"], shapes["New?"]) as RadDiagramConnection;
			c1.Content = "always";

			var c2 = diagram.AddConnection(shapes["New?"], shapes["CreateCart"]) as RadDiagramConnection;
			c2.Content = "yes";

			var c3 = diagram.AddConnection(shapes["New?"], shapes["FetchCart"]) as RadDiagramConnection;
			c3.Content = "no";

			var c4 = diagram.AddConnection(container, shapes["FetchCart"]) as RadDiagramConnection;
			c4.StrokeDashArray = new DoubleCollection { 2, 2 };
			c4.SourceCapType = CapType.Arrow6Filled;
			c4.Opacity = .5;

			var c5 = diagram.AddConnection(shapes["Database"], shapes["BizTalk"]) as RadDiagramConnection;
			c5.StrokeDashArray = new DoubleCollection { 3, 2 };
			c5.SourceCapType = CapType.Arrow5Filled;
			c5.Content = "Orchestration";
			c5.Opacity = .5;

			var c6 = diagram.AddConnection(shapes["Azure"], shapes["BizTalk"]) as RadDiagramConnection;
			c6.StrokeDashArray = new DoubleCollection { 3, 2 };
			c6.SourceCapType = CapType.Arrow5Filled;
			c6.Content = "Async";
			c6.Opacity = .5;

			var c7 = diagram.AddConnection(shapes["FSM"], shapes["BizTalk"]) as RadDiagramConnection;
			c7.StrokeDashArray = new DoubleCollection { 3, 2 };
			c7.SourceCapType = CapType.Arrow5Filled;
			c7.Content = "Async";
			c7.Opacity = .5;

			var c8 = diagram.AddConnection(shapes["FSM"], shapes["Database"]) as RadDiagramConnection;
			c8.StrokeDashArray = new DoubleCollection { 3, 2 };
			c8.SourceCapType = CapType.Arrow5Filled;
			c8.Content = "Async";
			c8.ConnectionType = ConnectionType.RoundedPolyline;
			c8.BezierTension = .72;
			c8.ManipulationPoints[0].Position = new Point(50, 550);
			c8.UpdateLayout();
			c8.Opacity = .5;

			var c9 = diagram.AddConnection(shapes["CreateCart"], shapes["Present"]) as RadDiagramConnection;
			c9.TargetCapType = CapType.Arrow1Filled;

			var c10 = diagram.AddConnection(shapes["FetchCart"], shapes["Present"]) as RadDiagramConnection;

			var c11 = diagram.AddConnection(shapes["Present"], shapes["Stop"]) as RadDiagramConnection;
		}
	}
}
