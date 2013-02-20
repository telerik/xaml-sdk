using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Diagrams;
using Telerik.Windows.Diagrams.Core;

using Node = Telerik.Windows.Diagrams.Core.Node<object, object>;
using Edge = Telerik.Windows.Diagrams.Core.Edge<object, object>;
using Graph = Telerik.Windows.Diagrams.Core.GraphBase<Telerik.Windows.Diagrams.Core.Node<object, object>, Telerik.Windows.Diagrams.Core.Edge<object, object>>;

namespace Diagrams.Layout
{
	/// <summary>
	/// A collection of graph-related utilities. 
	/// Nothing spectacular in terms of algorithmics but useful is you want to experiment
	/// with graph analysis and graph layout.
	/// </summary>
	public static class GraphExtensions
	{
		private static readonly Random Rand = new Random(Environment.TickCount);

		/// <summary>
		/// Creates a random connected graph.
		/// </summary>
		/// <param name="nodesCount">The nodes count.</param>
		/// <param name="maxIncidence">The max incidence.</param>
		/// <param name="tree">if set to <c>true</c> the random graph will be effectively a tree.</param>
		/// <returns></returns>
		public static Graph CreateRandomConnectedGraph(int nodesCount, int maxIncidence = 4, bool tree = false)
		{
			var graph = new Graph();
			if (nodesCount <= 0) return graph;
			graph.AddNode(new Node(0, true));
			if (nodesCount >= 1)
			{
				// this creates a random tree
				for (var i = 1; i < nodesCount; i++)
				{
					var poolNode = graph.TakeRandomNode(null, maxIncidence);
					// failed to find one so the graph will have less nodes than specified
					if (poolNode == null) break;
					var newNode = new Node(i, true);
					graph.AddNode(newNode);
					graph.AddLink(newNode, poolNode);
				}

				if (!tree && nodesCount > 1)
				{
					var randomAdditions = Rand.Next(1, nodesCount);
					for (var i = 0; i < randomAdditions; i++)
					{
						var n1 = graph.TakeRandomNode(null, maxIncidence);
						var n2 = graph.TakeRandomNode(null, maxIncidence);
						if (n1 != null && n2 != null && !graph.AreConnected(n1, n2)) graph.AddLink(n1, n2);
					}
				}
			}
			return graph;
		}

		/// <summary>
		/// Creates a random graph with a specified amounts of components.
		/// </summary>
		/// <param name="numberOfComponent">The number of component.</param>
		/// <returns></returns>
		public static Graph CreateComponents(int numberOfComponent)
		{
			var graph = new Graph();
			if (numberOfComponent <= 0) return graph;
			for (var i = 0; i < numberOfComponent; i++)
			{
				var component = CreateRandomConnectedGraph(Rand.Next(2, 20));
				// renumber vertices otherwise graphs will overlap in the merge
				component.RenumberNodes(graph.Nodes.Count);
				graph = graph.Merge(component);
			}
			return graph;
		}
		/// <summary>
		/// Merges the given graph into the current graph.
		/// </summary>
		/// <param name="graph">The graph.</param>
		/// <param name="otherGraph">The graph to merge into the current one.</param>
		/// <returns></returns>
		public static Graph Merge(this Graph graph, Graph otherGraph)
		{
			var merged = new Graph(graph);
			// merge the nodes with different id
			var existingList = new List<Node>();
			foreach (var node in otherGraph.Nodes)
			{
				if (!merged.Nodes.Any(n => n.Id == node.Id))
				{
					merged.Nodes.Add(node);
					// augment the Links collection
					foreach (var item in node.Outgoing) merged.Links.Add(item);
				}
				else existingList.Add(node);
			}

			// handle the links from the nodes which were already present
			foreach (var node in existingList)
			{
				var existingNode = merged.FindNode(node.Id);
				foreach (var edge in node.Outgoing)
				{
					var i = edge.Source.Id;
					var j = edge.Sink.Id;

					// do not use the 'AreConnected' here since it will rely on the Outgoing collection and will, hence, always return true
					if (existingNode.AllLinks.Count(l => l.Source.Id == i && l.Sink.Id == j) == 0)
					{
						var n = merged.FindNode(i);
						var m = merged.FindNode(j);
						merged.AddLink(n, m);
					}
				}
			}
			return merged;
		}

		/// <summary>
		/// Takes a random node with incidence less than specified.
		/// </summary>
		/// <param name="graph">The graph.</param>
		/// <param name="node">The node which should not be returned; i.e. the random node should be in the complement of the given node.</param>
		/// <param name="incidenceLessThan">The incidence less than.</param>
		/// <returns></returns>
		public static Node TakeRandomNode(this Graph graph, Node node = null, int incidenceLessThan = 4)
		{
			if (graph.Nodes.Count == 0) return null;
			if (graph.Nodes.Count == 1)
			{
				var singleton = graph.Nodes[0];
				return singleton == node ? null : singleton;
			}
			var pool = graph.Nodes.Where(n => n.AllLinks.Count < incidenceLessThan && n != node).ToList();
			if (!pool.Any()) return null;
			return pool.ToList()[Rand.Next(pool.Count())];
		}

		/// <summary>
		/// Creates a random graph.
		/// </summary>
		/// <param name="nodesCount">The count.</param>
		/// <param name="maxIncidence">The maximum incidence of each node.</param>
		/// <param name="tree">if set to <c>true</c> [tree].</param>
		/// <returns></returns>
		public static Graph CreateRandomGraph(int nodesCount = 150, int maxIncidence = 4, bool tree = false)
		{
			var graph = new Graph();
			for (var i = 0; i < nodesCount; i++)
			{
				var node = new Node(i, true);
				graph.Nodes.Add(node);
				var otherNode = FetchNode(graph, maxIncidence);
				if (otherNode != null) graph.AddLink(node, otherNode);
			}
			if (!tree && nodesCount > 1)
			{
				var randomAdditions = Rand.Next(1, nodesCount);
				for (var i = 0; i < randomAdditions; i++)
				{
					var n1 = graph.TakeRandomNode(null, maxIncidence);
					var n2 = graph.TakeRandomNode(null, maxIncidence);
					if (n1 != null && n2 != null && !graph.AreConnected(n1, n2)) graph.AddLink(n1, n2);
				}
			}
			return graph;
		}

		/// <summary>
		/// A delegate through which you can define your own shape-creation logic.
		/// </summary>
		/// <param name="node">The node.</param>
		/// <param name="randomSize">if set to <c>true</c> a shape with random size should be created.</param>
		public delegate RadDiagramShape CreateShapeDelegate(Node node, bool randomSize);

		/// <summary>
		/// The default shape-creation logic in the sample.
		/// You can use the delegate to hook up your own logic if you wish.
		/// </summary>
		/// <param name="node">The node which gets represented by this shape.</param>
		/// <param name="randomSize">if set to <c>true</c> the size of the shape will be randomly chosen.</param>
		public static RadDiagramShape CreateShape(Node node, bool randomSize = false)
		{
			//this.RandomSizeCheck.IsChecked.HasValue 
			var width = randomSize ? Rand.Next(35, 200) : 30d;
			var height = randomSize ? Rand.Next(35, 200) : 30d;
			return new RadDiagramShape
			{
				Width = width,
				Height = height,
				Content = node.Id,
				Tag = node.Id,
				UseGlidingConnector = true,
				GlidingStyle = GlidingStyle.Ellipse,
				Geometry = ShapeFactory.GetShapeGeometry(CommonShapeType.EllipseShape),
				Position =  new Point(Rand.Next(800), Rand.Next(600))
			};
		}

		/// <summary>
		/// Creates a diagram or visual representation of the given incidence structure.
		/// </summary>
		/// <param name="diagram">The diagram.</param>
		/// <param name="g">The graph structure.</param>
		/// <param name="create">The create.</param>
		/// <returns></returns>
		public static Graph CreateDiagram(this Telerik.Windows.Controls.RadDiagram diagram, Graph g, CreateShapeDelegate create = null, bool randomSize = false)
		{
			if (diagram == null) throw new ArgumentNullException("diagram");
			if (g == null) throw new ArgumentNullException("g");
			diagram.Clear();

			var dic = new Dictionary<int, RadDiagramShape>();
			foreach (var node in g.Nodes)
			{
				var shape = create == null ? CreateShape(node, randomSize) : create(node, randomSize);
				diagram.AddShape(shape);
				dic.Add(node.Id, shape);
			}
			foreach (var con in g.Links.Select(link => diagram.AddConnection(dic[link.Source.Id], dic[link.Sink.Id]))) con.TargetCapType = CapType.Arrow1Filled;
			return g;
		}
		/// <summary>
		/// Creates a diagram or visual representation of the given incidence structure, returning in addition the association between the graph elements on the one hand
		/// and the visuals on the other.
		/// </summary>
		/// <param name="diagram">The diagram.</param>
		/// <param name="g">The graph to visualize.</param>
		/// <param name="nodeMap">The node map.</param>
		/// <param name="edgeMap">The edge map.</param>
		/// <returns></returns>
		/// <seealso cref="Parse"/>
		public static Graph CreateDiagram(this Telerik.Windows.Controls.RadDiagram diagram, Graph g, out Dictionary<Node, RadDiagramShape> nodeMap, out Dictionary<Edge, RadDiagramConnection> edgeMap, CreateShapeDelegate create, bool randomSize = false)
		{
			if (g == null) throw new ArgumentNullException("g");
			diagram.Clear();

			var dic = new Dictionary<int, RadDiagramShape>();
			nodeMap = new Dictionary<Node, RadDiagramShape>();
			edgeMap = new Dictionary<Edge, RadDiagramConnection>();
			foreach (var node in g.Nodes)
			{
				var shape = create(node, randomSize);


				nodeMap.Add(node, shape);
				diagram.AddShape(shape);
				dic.Add(node.Id, shape);
			}
			foreach (Edge link in g.Links)
			{
				var con = diagram.AddConnection(dic[link.Source.Id], dic[link.Sink.Id]) as RadDiagramConnection;
				edgeMap.Add(link, con);
				con.TargetCapType = CapType.Arrow1Filled;
			}
			return g;
		}
		/// <summary>
		/// Creates a diagram or visual representation of the given incidence structure, returning in addition the association between the graph elements on the one hand
		/// and the visuals on the other.
		/// </summary>
		/// <param name="diagram">The diagram.</param>
		/// <param name="structure">The incidence structure.</param>
		/// <param name="nodeMap">The node map.</param>
		/// <param name="edgeMap">The edge map.</param>
		/// <returns></returns>
		/// <seealso cref="Parse"/>
		public static Graph CreateDiagram(this Telerik.Windows.Controls.RadDiagram diagram, IEnumerable<string> structure, out Dictionary<Node, RadDiagramShape> nodeMap, out Dictionary<Edge, RadDiagramConnection> edgeMap, CreateShapeDelegate create, bool randomSize)
		{
			var g = Telerik.Windows.Diagrams.Core.GraphExtensions.Parse(structure);
			return CreateDiagram(diagram, g, out nodeMap, out edgeMap, create, randomSize);
		}


		/// <summary>
		/// Fetches a random node with an incidence (aka degree) less than the provided value.
		/// </summary>
		/// <param name="graph">The graph.</param>
		/// <param name="incidence">The incidence.</param>
		/// <returns></returns>
		private static Node FetchNode(this Graph graph, int incidence = 4)
		{
			//we don't try more than 50 times
			for (var i = 0; i < 50; i++)
			{
				var found = graph.Nodes[Rand.Next(graph.Nodes.Count)];
				//we keep the incidence below 4
				if ((found.Incoming.Count + found.Outgoing.Count) < incidence) return found;
			}
			return null;
		}

		/// <summary>
		/// Creates a balanced forest.
		/// </summary>
		/// <param name="levels">The levels.</param>
		/// <param name="siblingsCount">The siblings count.</param>
		/// <param name="treeCount">The tree count.</param>
		/// <returns></returns>
		public static Graph CreateBalancedForest(int levels = 4, int siblingsCount = 2, int treeCount = 5)
		{
			var counter = -1;
			var graph = new Graph() { IsDirected = true };
			if (levels <= 0) return graph;

			for (var k = 0; k < treeCount; k++)
			{
				var root = new Node(counter++, true);
				graph.AddNode(root);
				if (siblingsCount <= 0) return graph;
				var lastAdded = new List<Node> { root };
				for (var i = 0; i < levels; i++)
				{
					var news = new List<Node>();
					foreach (var node in lastAdded)
					{
						for (var j = 0; j < siblingsCount; j++)
						{
							var item = new Node(counter++, true);
							graph.AddNode(item);
							graph.AddLink(node, item);
							news.Add(item);
						}
					}
					lastAdded = news;
				}
			}

			return graph;
		}

		/// <summary>
		/// Creates a symmetrical, balanced tree.
		/// </summary>
		/// <param name="levels">The amount of levels.</param>
		/// <param name="siblingsCount">The siblings count or amount of children per node.</param>
		/// <remarks>Don't go wild above these default since things go easily boom. RadDiagram will just do it but the result is not meaningful.</remarks>
		public static Graph CreateBalancedTree(int levels = 3, int siblingsCount = 3)
		{
			var counter = -1;
			var graph = new Graph { IsDirected = true };
			if (levels <= 0) return graph;
			var root = new Node(counter++, true);
			graph.AddNode(root);
			if (siblingsCount <= 0) return graph;
			var lastAdded = new List<Node> { root };
			for (var i = 0; i < levels; i++)
			{
				var news = new List<Node>();
				foreach (var node in lastAdded)
				{
					for (var j = 0; j < siblingsCount; j++)
					{
						var item = new Node(counter++, true);
						graph.AddNode(item);
						graph.AddLink(node, item);
						news.Add(item);
					}
				}
				lastAdded = news;
			}

			return graph;
		}

		public static void CreateGraph(this RadDiagram diagram, GraphGenerationSpecifications specs, CreateShapeDelegate createShape)
		{
			diagram.Clear();
			if (specs.Connections)
			{
				var g = specs.Connected ? GraphExtensions.CreateRandomConnectedGraph(specs.NodeCount, 4, specs.TreeGraph) : GraphExtensions.CreateRandomGraph(specs.NodeCount, 4, specs.TreeGraph);
				diagram.CreateDiagram(g, GraphExtensions.CreateShape, specs.RandomShapeSize);
			}
			else
			{
				for (var i = 0; i < specs.NodeCount; i++)
				{
					var shape = createShape(new Node(i,false), specs.RandomShapeSize);
					diagram.AddShape(shape);
				}
			}
		}

		/// <summary>
		/// Creates a balanced radial forest.
		/// </summary>
		/// <param name="diagram">The diagram.</param>
		/// <param name="specs">The specs.</param>
		public static void BalancedRadialForest(this RadDiagram diagram, GraphGenerationSpecifications specs)
		{
			diagram.Clear();
			Dictionary<Node, RadDiagramShape> nodeMap;
			Dictionary<Edge, RadDiagramConnection> edgeMap;
			var g = diagram.CreateDiagram(CreateBalancedForest(), out nodeMap, out edgeMap, CreateShape, specs.RandomShapeSize);
			var settings = new TreeLayoutSettings
			{
				TreeLayoutType = specs.TreeType,
				HorizontalSeparation = specs.HorizontalSeparation,
				VerticalSeparation = specs.VerticalSeparation,
				UnderneathHorizontalOffset = specs.UnderneathHorizontalOffset,
				UnderneathVerticalSeparation = specs.UnderneathVerticalSeparation,
				KeepComponentsInOneRadialLayout = specs.KeepComponentsInOneRadialLayout
			};
			var center = g.FindNode(1);
			settings.Roots.Add(nodeMap[center]);
			var layout = new TreeLayout();
			layout.Layout(diagram, settings);
			diagram.AutoFit();
		}
	}
}