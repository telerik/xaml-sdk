using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

using Telerik.Windows.Controls;
using Telerik.Windows.Diagrams.Core;

using Edge = Telerik.Windows.Diagrams.Core.Edge<object, object>;
using Graph = Telerik.Windows.Diagrams.Core.GraphBase<Telerik.Windows.Diagrams.Core.Node<object, object>, Telerik.Windows.Diagrams.Core.Edge<object, object>>;
using Node = Telerik.Windows.Diagrams.Core.Node<object, object>;
using Orientation = Telerik.Windows.Diagrams.Core.Orientation;

namespace Diagrams.Layout
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView
    {
        private List<IShape> containerSampleRoots;

        /// <summary>
        /// The brush used to highlight paths and subgraphs.
        /// </summary>
        private readonly Brush HighlighBrush = new SolidColorBrush(Colors.Green);

        /// <summary>
        /// The current layout orientation
        /// </summary>
        private Orientation layoutOrientation = Orientation.Vertical;

        /// <summary>
        /// Gets the current specifications from the UI switches.
        /// </summary>
        private GraphGenerationSpecifications CurrentSpecifications
        {
            get
            {
                // this bucket is just an easy way to gather the different slider values in the UI
                return new GraphGenerationSpecifications
                           {
                               Connected = this.ConnectedCheck.IsChecked.HasValue && this.ConnectedCheck.IsChecked.Value,
                               Connections = this.ConnectionsCheck.IsChecked.HasValue && this.ConnectionsCheck.IsChecked.Value,
                               NodeCount = (int)(this.CountSpinner.Value.HasValue ? this.CountSpinner.Value.Value : 150),
                               RandomShapeSize = this.RandomSizeCheck.IsChecked.HasValue && this.RandomSizeCheck.IsChecked.Value,
                               TreeGraph = this.TreeCheck.IsChecked.HasValue && this.TreeCheck.IsChecked.Value,
                               VerticalSeparation = this.VerticalSeparationSlider.Value,
                               UnderneathHorizontalOffset = this.UnderneathHorizontalOffsetSlider.Value,
                               UnderneathVerticalSeparation = this.UnderneathVerticalOffsetSlider.Value,
                               KeepComponentsInOneRadialLayout = true,
                               HorizontalSeparation = this.HorizontalSeparationSlider.Value,
                               CreateShape = GraphExtensions.CreateShape,
                               HighlightBrush = this.HighlighBrush,
                               RadialSeparation = this.RadialSeparationSlider.Value
                           };
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MainView" /> class.
        /// </summary>
        public MainView()
        {
            // various constants can be statically changed via the DiagramConstants class
            DiagramConstants.MinimumZoom = .1;

            this.InitializeComponent();
            this.CountSpinner.Maximum = 1000;
            this.CountSpinner.Minimum = 5;
            this.CountSpinner.Value = 50;

            this.Loaded += this.OnLoaded;

            // select items when partially overlapping with the selection rectangle
            this.diagram.RectSelectionMode = RectSelectionMode.Partial;
        }

        /// <summary>
        /// Adds several samples of special interest, highlighting the capabilities of RadDiagram's graph analysis framework.
        /// </summary>
        private void AddSamples()
        {
            this.SamplesCombo.Items.Add(new SampleItem { Title = "Longest path", Description = "Finds and displays the longest path in the graph.", Run = this.LongestPath });
            this.SamplesCombo.Items.Add(new SampleItem { Title = "Shortest path", Description = "Finds and displays the shortest path between two nodes.", Run = this.ShortestPath });
            this.SamplesCombo.Items.Add(new SampleItem { Title = "Cycles", Description = "Finds and displays the cycles in a graph.", Run = this.Cycles });
            this.SamplesCombo.Items.Add(new SampleItem { Title = "Prim's algorithm", Description = "Finds and displays the minimal spanning tree in a graph.", Run = this.Prims });
            this.SamplesCombo.Items.Add(new SampleItem { Title = "Balanced radial tree", Description = "Displays a balance radial tree.", Run = this.BalancedRadialTree });
            this.SamplesCombo.Items.Add(new SampleItem { Title = "Forest of balanced radial trees", Description = "Displays a forest of balanced radial trees.", Run = BalancedRadialForest });
            this.SamplesCombo.Items.Add(new SampleItem { Title = "Forest of random radial trees", Description = "Displays a forest of random radial trees.", Run = this.RandomRadialForest });
        }

        /// <summary>
        /// Handles the OnKeyDown event of the CountSpinner control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs" /> instance containing the event data.</param>
        private void CountSpinner_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && this.CountSpinner != null) this.CreateGraph();
        }

        /// <summary>
        /// Handles the OnClick event of the CreateButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void CreateButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (this.CountSpinner != null) this.CreateGraph();
        }

        /// <summary>
        /// Creates a new (random) graph with constraints set by the various switches in the UI.
        /// </summary>
        private void CreateGraph()
        {
            this.diagram.CreateGraph(this.CurrentSpecifications, GraphExtensions.CreateShape);
        }

        /// <summary>
        /// Called when the app has loaded.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="routedEventArgs">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            this.AddSamples();

            // just showing one of the samples rather than having a blank canvas at start
            this.BalancedRadialTree(this.diagram, this.CurrentSpecifications);
        }

        /// <summary>
        /// Handles the OnSelectionChanged event of the SamplesCombo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs" /> instance containing the event data.</param>
        private void SamplesCombo_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var sample = this.SamplesCombo.SelectedItem as SampleItem;
            if (sample != null) sample.Run(this.diagram, this.CurrentSpecifications);
        }

        /// <summary>
        /// Handles the OnClick event of the TreeLayoutButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void TreeLayoutButton_OnClick(object sender, RoutedEventArgs e)
        {
            var settings = new TreeLayoutSettings
            {
                TreeLayoutType = this.GetTreeLayoutType(),
                HorizontalSeparation = this.HorizontalSeparationSlider.Value,
                VerticalSeparation = this.VerticalSeparationSlider.Value,
                UnderneathHorizontalOffset = this.UnderneathHorizontalOffsetSlider.Value,
                UnderneathVerticalSeparation = this.UnderneathVerticalOffsetSlider.Value,
                KeepComponentsInOneRadialLayout = true,
                RadialSeparation = this.RadialSeparationSlider.Value,
                RadialFirstLEvelSeparation = this.RadialFirstLevelSeparationSlider.Value
            };
            var layout = new TreeLayout();
            settings.Roots.Add(this.diagram.Items[1] as IShape);
            layout.Layout(this.diagram, settings);
            this.diagram.AutoFit();
        }

        /// <summary>
        /// Handles the OnClick event of the VerticalOption control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void VerticalOption_OnClick(object sender, RoutedEventArgs e)
        {
            this.HorizontalOption.IsChecked = false;
            this.layoutOrientation = Orientation.Vertical;
        }

        private TreeLayoutType GetTreeLayoutType()
        {
            if (this.TreeDown.IsChecked.HasValue && this.TreeDown.IsChecked.Value) return TreeLayoutType.TreeDown;
            if (this.TreeUp.IsChecked.HasValue && this.TreeUp.IsChecked.Value) return TreeLayoutType.TreeUp;
            if (this.TreeLeft.IsChecked.HasValue && this.TreeLeft.IsChecked.Value) return TreeLayoutType.TreeLeft;
            if (this.TreeRight.IsChecked.HasValue && this.TreeRight.IsChecked.Value) return TreeLayoutType.TreeRight;
            if (this.Radial.IsChecked.HasValue && this.Radial.IsChecked.Value) return TreeLayoutType.RadialTree;
            if (this.Tipovertree.IsChecked.HasValue && this.Tipovertree.IsChecked.Value) return TreeLayoutType.TipOverTree;
            if (this.MindmapHorizontal.IsChecked.HasValue && this.MindmapHorizontal.IsChecked.Value) return TreeLayoutType.MindmapHorizontal;
            if (this.MindmapVertical.IsChecked.HasValue && this.MindmapVertical.IsChecked.Value) return TreeLayoutType.MindmapVertical;
            return TreeLayoutType.TreeDown;
        }

        /// <summary>
        /// Highlights the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="nodeMap">The node map.</param>
        /// <param name="edgeMap">The edge map.</param>
        /// <param name="brush">The brush for highlighting.</param>
        private void Highlight(GraphPath<Node, Edge> path, IDictionary<Node, RadDiagramShape> nodeMap, IDictionary<Edge, RadDiagramConnection> edgeMap, Brush brush)
        {
            if (path == null) return;
            foreach (var node in path.Nodes) nodeMap[node].Background = brush;
            foreach (var link in path.Links)
            {
                edgeMap[link].Background = brush;
                edgeMap[link].Stroke = brush;
                edgeMap[link].StrokeThickness = 2d;
            }
        }

        /// <summary>
        /// Highlights the specified graph.
        /// </summary>
        /// <param name="g">The graph to highlight.</param>
        /// <param name="nodeMap">The node map.</param>
        /// <param name="edgeMap">The edge map.</param>
        /// <param name="brush">The brush for highlighting.</param>
        private void Highlight(Graph g, IDictionary<Node, RadDiagramShape> nodeMap, IDictionary<Edge, RadDiagramConnection> edgeMap, Brush brush)
        {
            if (g == null) return;
            foreach (var node in g.Nodes) nodeMap[node].Background = brush;
            foreach (var link in g.Links)
            {
                edgeMap[link].Background = brush;
                edgeMap[link].Stroke = brush;
                edgeMap[link].StrokeThickness = 2d;
            }
        }

        private void HorizontalOption_OnClick(object sender, RoutedEventArgs e)
        {
            this.VerticalOption.IsChecked = false;
            this.layoutOrientation = Orientation.Horizontal;
        }

        /// <summary>
        /// Applies the Sugiyama or layered layout to the diagram with the specs sets using the UI sliders.
        /// </summary>
        private void Layout()
        {
            this.diagram.Layout(LayoutType.Sugiyama, new SugiyamaSettings
            {
                ComponentMargin = new Size(this.ComponentMarginWidthSlider.Value, this.ComponentMarginHeightSlider.Value),
                ComponentsGridWidth = this.ComponentGridWidthSlider.Value,
                HorizontalDistance = this.HorizontalDistanceSlider.Value,
                VerticalDistance = this.VerticalDistanceSlider.Value,
                ShapeMargin = new Size(this.ShapeMarginWidthSlider.Value, this.ShapeMarginHeightSlider.Value),
                TotalMargin = new Size(this.TotalMarginWidthSlider.Value, this.TotalMarginHeightSlider.Value),
                Orientation = this.layoutOrientation
            });
        }

        private void LayoutButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.Layout();
        }

		/// <summary>
		/// Colorizes the specified graph by means of a traversal.
		/// </summary>
		/// <remarks>This is just an example of how to use the traversal algorithms within RadDiagram.</remarks>
		/// <param name="g">The graph to colorize.</param>
		/// <param name="nodeMap">The node map.</param>
		/// <param name="center">The center from which the traversal starts.</param>
		private void Colorize(Graph g, IDictionary<Node, RadDiagramShape> nodeMap, Node center)
		{

			//var geo = App.Current.Resources["glass"] as PathGeometry;
			//var glassfill = App.Current.Resources["glassfill"] as RadialGradientBrush;
			//var spherefill = App.Current.Resources["spherefill"] as RadialGradientBrush;
		
			// you can use a directed traversal but it will obviously not always reach all nodes
			g.IsDirected = false;
		
			var colors = new List<Color>();
			for (var i = 0; i < 30; i++) colors.Add(ColorUtilities.RandomBlues);

			var group = new TransformGroup();
			@group.Children.Add(new ScaleTransform(.7, .7, 05, 05));
			@group.Children.Add(new TranslateTransform(0.02, 0.3));
			g.DepthFirstTraversal((node, i) =>
				{
					var shape = nodeMap[node];
					shape.Width = 30;
					shape.Height = 30;
					//shape.Geometry = geo;
					shape.BorderBrush = Brushes.LightSteelBlue;

					var spherefill = new RadialGradientBrush(new GradientStopCollection { new GradientStop(colors[i], 1d), new GradientStop(Colors.White, .4d), }) { Transform = @group };

					shape.Background = spherefill;
					shape.Content = i;
				}, center);
		}

        #region Samples

        /// <summary>
        /// Creates a specific graph which has some obvious cycles and lets the graph analysis highlight them, thus confirming the cycles
        /// which can be easily found manually. The analysis goes of course beyond what the human eye can see and would find cycles in an arbitrary graph.
        /// </summary>
        /// <param name="diagram">The diagram.</param>
        /// <param name="specs">The specs.</param>
        private void Cycles(RadDiagram diagram, GraphGenerationSpecifications specs)
        {
            diagram.Clear();
            Dictionary<Node, RadDiagramShape> nodeMap;
            Dictionary<Edge, RadDiagramConnection> edgeMap;
            var g = diagram.CreateDiagram(new List<string> { "1,2", "3,1", "2,4", "4,3", "4,5", "10,11", "11,12", "12,10" }, out nodeMap,
                out edgeMap, specs.CreateShape, specs.RandomShapeSize);
            var cycles = g.FindCycles();
            if (cycles.Count > 0)
            {
                foreach (var cycle in cycles)
                {
                    var path = new GraphPath<Node, Edge>();
                    cycle.ToList().ForEach(path.AddNode);
                    this.Highlight(path, nodeMap, edgeMap, specs.HighlightBrush);
                }
            }
            diagram.Layout(LayoutType.Tree, new TreeLayoutSettings
                                                {
                                                    TreeLayoutType = TreeLayoutType.TreeRight,
                                                    VerticalSeparation = 50d,
                                                    HorizontalSeparation = 80d
                                                });
        }

        /// <summary>
        /// Creates a tree which grows symmetrically.
        /// </summary>
        /// <param name="diagram">The diagram.</param>
        /// <param name="specs">The specs.</param>
        private void BalancedRadialTree(RadDiagram diagram, GraphGenerationSpecifications specs)
        {
            this.diagram.Clear();
            Dictionary<Node, RadDiagramShape> nodeMap;
            Dictionary<Edge, RadDiagramConnection> edgeMap;

            // the algorithm to create a balanced tree is quite straighforward
            var g = this.diagram.CreateDiagram(GraphExtensions.CreateBalancedTree(), out nodeMap, out edgeMap, GraphExtensions.CreateShape, this.RandomSizeCheck.IsChecked.HasValue && this.RandomSizeCheck.IsChecked.Value);

            // the result is best displayed with the radial tree layout
            var settings = new TreeLayoutSettings
            {
                TreeLayoutType = TreeLayoutType.RadialTree,
                HorizontalSeparation = this.HorizontalSeparationSlider.Value,
                VerticalSeparation = this.VerticalSeparationSlider.Value,
                UnderneathHorizontalOffset = this.UnderneathHorizontalOffsetSlider.Value,
                UnderneathVerticalSeparation = this.UnderneathVerticalOffsetSlider.Value,
                StartRadialAngle = Math.PI,
                // use a specific angle rather than the full 360° to show one of the layout's (hidden) gem
                EndRadialAngle = 3.47 * Math.PI / 2
            };
            var center = g.FindNode(-1);
            settings.Roots.Add(nodeMap[center]);
            var layout = new TreeLayout();

            layout.Layout(this.diagram, settings);

            // center and size autimagically
            this.diagram.AutoFit();

            // you can colorize the shapes, if you wish
            // this.Colorize(g, nodeMap, center);
        }

        /// <summary>
        /// Creates a forest of balanced radial tree and applies a radial layout.
        /// </summary>
        /// <param name="diagram">The diagram.</param>
        /// <param name="specs">The specs.</param>
        private static void BalancedRadialForest(RadDiagram diagram, GraphGenerationSpecifications specs)
        {
            specs.TreeType = TreeLayoutType.RadialTree;
            specs.KeepComponentsInOneRadialLayout = true;
            diagram.BalancedRadialForest(specs);
        }

        /// <summary>
        /// Creates a forest of unbalanced trees and applies a radial layout.
        /// </summary>
        /// <param name="diagram">The diagram.</param>
        /// <param name="specs">The specs.</param>
        private void RandomRadialForest(RadDiagram diagram, GraphGenerationSpecifications specs)
        {
            this.diagram.Clear();
            Dictionary<Node, RadDiagramShape> nodeMap;
            Dictionary<Edge, RadDiagramConnection> edgeMap;
            var g = this.diagram.CreateDiagram(GraphExtensions.CreateRandomGraph(250, 4, true), out nodeMap, out edgeMap, GraphExtensions.CreateShape, this.RandomSizeCheck.IsChecked.HasValue && this.RandomSizeCheck.IsChecked.Value);
            var settings = new TreeLayoutSettings
            {
                TreeLayoutType = TreeLayoutType.RadialTree,
                HorizontalSeparation = this.HorizontalSeparationSlider.Value,
                VerticalSeparation = this.VerticalSeparationSlider.Value,
                UnderneathHorizontalOffset = this.UnderneathHorizontalOffsetSlider.Value,
                UnderneathVerticalSeparation = this.UnderneathVerticalOffsetSlider.Value,
                KeepComponentsInOneRadialLayout = true,
                RadialSeparation = 45d
            };
            var center = g.FindNode(0);
            settings.Roots.Add(nodeMap[center]);
            var layout = new TreeLayout();
            layout.Layout(this.diagram, settings);
            this.diagram.AutoFit();
            //Colorize(g, nodeMap, center);  
        }

        /// <summary>
        /// Creates a specific graph to show the shortest path between two nodes. 
        /// </summary>
        /// <param name="obj">The diagram.</param>
        /// <param name="specs">The specs.</param>
        private void ShortestPath(RadDiagram obj, GraphGenerationSpecifications specs)
        {
            this.diagram.Clear();
            Dictionary<Node, RadDiagramShape> nodeMap;
            Dictionary<Edge, RadDiagramConnection> edgeMap;
            var g = this.diagram.CreateDiagram(new List<string> { "1,2", "2,3", "1,4", "4,5", "5,3", "1,6", "6,7", "7,8", "8,3", "3,9", "9,10", "10,11" }, out nodeMap, out edgeMap, GraphExtensions.CreateShape, this.RandomSizeCheck.IsChecked.HasValue && this.RandomSizeCheck.IsChecked.Value);

            // Dijkstra's algorithm will give you the shortest path
            var path = g.DijkstraShortestPath(1, 11);
            if (path != null) this.Highlight(path, nodeMap, edgeMap, this.HighlighBrush);

            // the node map gives us the shape from a node and we want to have the "1" node at the root of the tree
            var nodeOne = g.FindNode(1);
            var shapeOne = nodeMap[nodeOne];
            var settings = new TreeLayoutSettings
            {
                TreeLayoutType = TreeLayoutType.TreeDown,
                VerticalSeparation = 50d,
                HorizontalSeparation = 80d,
            };
            // specifying the root (or roots if we have a forest) can be done using the Roots property like so
            settings.Roots.Add(shapeOne);
            diagram.Layout(LayoutType.Tree, settings);
        }

        /// <summary>
        /// Creates a random connected graph and displayes a (non-unique) spanning tree using Prim's algorithm.
        /// </summary>
        /// <param name="diagram">The diagram.</param>
        /// <param name="specs">The specs.</param>
        private void Prims(RadDiagram diagram, GraphGenerationSpecifications specs)
        {
            diagram.Clear();
            Dictionary<Node, RadDiagramShape> nodeMap;
            Dictionary<Edge, RadDiagramConnection> edgeMap;
            var randomConnectedGraph = GraphExtensions.CreateRandomConnectedGraph(10);
            var root = randomConnectedGraph.FindTreeRoot();
            var g = diagram.CreateDiagram(randomConnectedGraph, out nodeMap, out edgeMap, GraphExtensions.CreateShape, specs.RandomShapeSize);

            // making it undirected will reach all the nodes since the random graph is connected
            g.IsDirected = false;
            var tree = g.PrimsSpanningTree(root);
            if (tree != null) this.Highlight(tree, nodeMap, edgeMap, this.HighlighBrush);
            var settings = new TreeLayoutSettings
            {
                TreeLayoutType = TreeLayoutType.TreeDown,
                VerticalSeparation = 50d,
                HorizontalSeparation = 80d,
            };
            diagram.Layout(LayoutType.Tree, settings);
        }

        /// <summary>
        /// Creates a specific diagram and highlight the longest path found through the graph analysis.
        /// </summary>
        /// <param name="diagram">The diagram.</param>
        /// <param name="specs">The specs.</param>
        private void LongestPath(RadDiagram diagram, GraphGenerationSpecifications specs)
        {
            diagram.Clear();
            Dictionary<Node, RadDiagramShape> nodeMap;
            Dictionary<Edge, RadDiagramConnection> edgeMap;

            // this creates the specific graph 
            var g = this.diagram.CreateDiagram(new List<string> { "1,2", "2,3", "2,4", "3,5", "4,5", "5,6", "2,7", "7,8", "8,9", "9,10", "10,6", "20,21", "21,22", "20,25" }, out nodeMap, out edgeMap, GraphExtensions.CreateShape, this.RandomSizeCheck.IsChecked.HasValue && this.RandomSizeCheck.IsChecked.Value);

            // note that this works on disconnected graphs as well as on connected ones
            var path = g.FindLongestPath();

            // highlight the longest path if one is found
            if (path != null) this.Highlight(path, nodeMap, edgeMap, this.HighlighBrush);

            // use a layout which displays the result best
            diagram.Layout(LayoutType.Tree, new TreeLayoutSettings
            {
                TreeLayoutType = TreeLayoutType.TreeUp,
                VerticalSeparation = 80d,
                HorizontalSeparation = 50d
            });
        }



        private void CreateContainersButton_OnClick(object sender, RoutedEventArgs e)
        {
            diagram.Clear();
            this.containerSampleRoots = new List<IShape>();
            
            var directors = diagram.CreateContainerShape("Board of directors");
            var ceo = diagram.CreateShape("CEO");
            var cfo = diagram.CreateShape("CFO");
            var cio = diagram.CreateShape("CIO");
            GraphExtensions.ApplyColor(new SolidColorBrush(Colors.Green), ceo, cfo, cio);
            for (var i = 0; i < 5; i++)
            {
                var boardmember = diagram.CreateShape("M" + i);
                boardmember.Background = new SolidColorBrush(Colors.Purple);
                directors.AddItem(boardmember);
            }

            var chiefs = diagram.CreateContainerShape("Chiefs");
            var divisions = diagram.CreateContainerShape("Divisions");

            diagram.Connect(directors, chiefs);
            diagram.Connect(chiefs, divisions);
            chiefs.AddItems(new[] { ceo, cfo, cio });

            #region IT division
            var it = diagram.CreateContainerShape("IT");
            divisions.AddItem(it);
            
            var itBranch1 = this.diagram.CreateDivisionBranch("WPF");
            it.AddItems(itBranch1);
            this.containerSampleRoots.Add(itBranch1[0]); // the first item is the root of the branch by design 
            
            var itBranch2 = this.diagram.CreateDivisionBranch("HTML");
            it.AddItems(itBranch2);
            this.containerSampleRoots.Add(itBranch2[0]); 
            #endregion
            
            #region Finance division
            var finance = diagram.CreateContainerShape("Finance");
            divisions.AddItem(finance);
            for (var i = 0; i < 8; i++)
            {
                var financeWorker = diagram.CreateShape("F" + i);
                financeWorker.Background = new SolidColorBrush(Colors.Orange);
                finance.AddItem(financeWorker);
            }
            diagram.Connect(finance.Items[0] as IShape, finance.Items[1] as IShape);
            diagram.Connect(finance.Items[0] as IShape, finance.Items[2] as IShape);
            diagram.Connect(finance.Items[7] as IShape, finance.Items[5] as IShape);
            diagram.Connect(finance.Items[0] as IShape, finance.Items[5] as IShape);
            #endregion

            #region External
            var external = this.diagram.CreateContainerShape("Offices");
            diagram.Connect(chiefs, external);
            #region Spain office
            var spain = this.diagram.CreateContainerShape("Spain");
            external.AddItem(spain);
            for (var i = 0; i < 8; i++)
            {
                var spanish = diagram.CreateShape("S" + i);
                spanish.Background = new SolidColorBrush(Colors.Gray);
                spain.AddItem(spanish);
            }
            diagram.Connect(spain.Items[0] as IShape, spain.Items[1] as IShape);
            diagram.Connect(spain.Items[0] as IShape, spain.Items[2] as IShape);
            diagram.Connect(spain.Items[3] as IShape, spain.Items[2] as IShape);
            diagram.Connect(spain.Items[2] as IShape, spain.Items[5] as IShape);
            diagram.Connect(spain.Items[0] as IShape, spain.Items[5] as IShape);
            diagram.Connect(spain.Items[6] as IShape, spain.Items[5] as IShape);
            #endregion
            
            #region Austria office
            var austria = this.diagram.CreateContainerShape("Austria");
            external.AddItem(austria);
            for (var i = 0; i < 9; i++)
            {
                var spanish = diagram.CreateShape("S" + i);
                spanish.Background = new SolidColorBrush(Colors.Cyan);
                austria.AddItem(spanish);
            }
            diagram.Connect(austria.Items[0] as IShape, austria.Items[1] as IShape);
            diagram.Connect(austria.Items[4] as IShape, austria.Items[2] as IShape);
            diagram.Connect(austria.Items[2] as IShape, austria.Items[5] as IShape);
            diagram.Connect(austria.Items[4] as IShape, austria.Items[5] as IShape);
            diagram.Connect(austria.Items[6] as IShape, austria.Items[5] as IShape);
            #endregion
            
            #endregion
            this.containerSampleRoots.Add(directors);
        }

        #endregion

        private void IgnoreContainersCheck_OnClick(object sender, RoutedEventArgs e)
        {
            // the container layout only makes sense if containers are taken into account
            LayoutContainersCheck.IsEnabled = !IgnoreContainersCheck.IsChecked.Value;
        }

        private void LayoutContainersButton_OnClick(object sender, RoutedEventArgs e)
        {
           this.ApplyContainerLayout();
        }

        private void ApplyContainerLayout()
        {

            if (TreeLayout.IsChecked.Value)
            {
                var settings = new TreeLayoutSettings
                {
                    TreeLayoutType = TreeLayoutType.TipOverTree,
                    IgnoreContainers = this.IgnoreContainersCheck.IsChecked.Value,
                    LayoutContainerChildren = this.LayoutContainersCheck.IsChecked.Value,
                    ComponentsGridWidth = 15000
                };
                if (this.containerSampleRoots != null) settings.Roots.AddRange(this.containerSampleRoots);
                this.diagram.Layout(LayoutType.Tree, settings);
            }
            else
            {
                var settings = new SugiyamaSettings
                {
                    IgnoreContainers = this.IgnoreContainersCheck.IsChecked.Value,
                    LayoutContainerChildren = this.LayoutContainersCheck.IsChecked.Value,
                    ComponentsGridWidth = 15000
                };
                this.diagram.Layout(LayoutType.Sugiyama, settings);
            }
        }

    }
}