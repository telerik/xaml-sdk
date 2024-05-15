using System.Windows;
using Telerik.Windows.Diagrams.Core;

namespace Diagrams.PascalTriangle
{
	public partial class MainView
	{
		private const int RootXPosition = 400;
		private const int RootYPosition = 20;
		private const int XDelimiterWidth = 80;
		private const int YDelimiterHeight = 60;
		private const int XStartNegativeOffset = 40;

		public MainView()
		{
			InitializeComponent();
			this.diagram.GraphSource = CreatePascalTriangleGraphSource(7);
		}

		private static IGraphSource CreatePascalTriangleGraphSource(int levels)
		{
			var graph = new PascalTriangleGraphSource();
			for (var i = 0; i < levels; i++)
			{
				for (var j = 0; j < i + 1; j++)
				{
					var node = new PascalNode {
						Position = new Point {
							Y = RootYPosition + (i * YDelimiterHeight),
							X = RootXPosition - (i * XStartNegativeOffset) + (j * XDelimiterWidth)
						},
						PascalNumber = Binom(i, j),
						IsTextBoxType = j % 2 == 0
					};
					graph.InternalItems.Add(node);
					var currIndex = (i * (i + 1) / 2) + j;
					if (j == 0) graph.InternalEdges.Add(new PascalEdge { Source = graph.InternalItems[currIndex - i], Target = node });
					if (j == i && i != 0) graph.InternalEdges.Add(new PascalEdge { Source = graph.InternalItems[currIndex - i - 1], Target = node });
					if (0 < j && j < i)
					{
						graph.InternalEdges.Add(new PascalEdge { Source = graph.InternalItems[currIndex - i - 1], Target = node });
						graph.InternalEdges.Add(new PascalEdge { Source = graph.InternalItems[currIndex - i], Target = node });
					}
				}
			}
			return graph;
		}

		private static int Binom(int n, int k)
		{
			return FactN(n) / (FactN(k) * FactN(n - k));
		}

		private static int FactN(int n)
		{
			if (n == 0 || n == 1) return 1;
			var res = 1;
			for (var i = 1; i <= n; i++) res = res * i;
			return res;
		}
	}
}
