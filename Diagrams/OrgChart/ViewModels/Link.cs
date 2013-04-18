using Telerik.Windows.Controls.Diagrams.Extensions.ViewModels;

namespace OrgChart.ViewModels
{
	public class Link : LinkViewModelBase<Node>
	{
		public Link(Node source, Node target)
			: base(source, target)
		{
		}
	}
}