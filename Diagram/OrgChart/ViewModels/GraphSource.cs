using Telerik.Windows.Controls.Diagrams.Extensions.ViewModels;

namespace OrgChart.ViewModels
{
	public class GraphSource :  ObservableGraphSourceBase<Node, Link>
	{
		public void PopulateGraphSource(Node node)
		{
			this.AddNode(node);
			foreach (Node subNode in node.Children)
			{
				Link link = new Link(node, subNode);
				this.AddLink(link);
				this.PopulateGraphSource(subNode);
			}
		}
	}
}
