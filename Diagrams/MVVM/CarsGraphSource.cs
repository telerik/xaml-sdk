using System.Windows;
using Telerik.Windows.Controls.Diagrams.Extensions.ViewModels;
using Telerik.Windows.Diagrams.Core;

namespace MVVM
{
	public class CarsGraphSource : SerializableGraphSourceBase<NodeViewModelBase, Link>
	{
		public CarsGraphSource()
		{
			Brand vwGroup = new Brand() { Content = "Volkswagen Group", Position = new Point(250, 100) };

			Brand bentley = new Brand() { Content = "Bentley", Position = new Point(250, 100) };
			Model continental = new Model() { Content = "Continental GT", Position = new Point(250, 100) };
			bentley.AddItem(continental);
			vwGroup.AddItem(bentley);

			Brand bugatti = new Brand() { Content = "Bugatti", Position = new Point(475, 100) };
			Model veyron = new Model() { Content = "Bugatti Veyron", Position = new Point(475, 100) };
			bugatti.AddItem(veyron);
			vwGroup.AddItem(bugatti);

			Brand vw = new Brand() { Content = "Volkswagen", Position = new Point(220, 400) };
			Model polo = new Model() { Content = "Polo", Position = new Point(220, 400) };
			Model golf = new Model() { Content = "Golf", Position = new Point(340, 400) };
			Model passat = new Model() { Content = "Passat", Position = new Point(240, 460) };
			vw.AddItem(polo);
			vw.AddItem(golf);
			vw.AddItem(passat);
			Link groupToVw = new Link() { Content = "Owns", Source = vwGroup, Target = vw };

			Brand audi = new Brand() { Content = "Audi", Position = new Point(520, 400) };
			Model r8 = new Model() { Content = "R8", Position = new Point(520, 400) };
			Model a4 = new Model() { Content = "A4", Position = new Point(640, 400) };
			Model a6 = new Model() { Content = "A6", Position = new Point(540, 460) };
			audi.AddItem(r8);
			audi.AddItem(a4);
			audi.AddItem(a6);
			Link groupToAudi = new Link() { Content = "Owns", Source = vwGroup, Target = audi };

			this.AddNode(vw);
			this.AddNode(audi);
			this.AddNode(vwGroup);

			this.AddLink(groupToVw);
			this.AddLink(groupToAudi);
		}

		public override void SerializeNode(NodeViewModelBase node, SerializationInfo info)
		{
			base.SerializeNode(node, info);
			if (node.Content != null)
				info["Content"] = node.Content.ToString();
		}

		public override NodeViewModelBase DeserializeNode(IShape shape, Telerik.Windows.Diagrams.Core.SerializationInfo info)
		{
			NodeViewModelBase node = null;
			if (shape is IContainerShape)
			{
				node = new Brand();

			}
			else
			{
				node = new Model();
			}
			if (info["Content"] != null)
				node.Content = info["Content"].ToString();
			if (info[this.NodeUniqueIdKey] != null)
			{
				var nodeUniquekey = info[this.NodeUniqueIdKey].ToString();
				if (!this.CachedNodes.ContainsKey(nodeUniquekey))
				{
					this.CachedNodes.Add(nodeUniquekey, node);
				}
			}

			return node;
		}

		public override string GetNodeUniqueId(NodeViewModelBase node)
		{
			if (node != null)
				return node.GetHashCode().ToString();

			return string.Empty;
		}

		public override object CreateNode(IShape shape)
		{
			var newItem = base.CreateNode(shape);
			if(shape.Content != null)
				(newItem as NodeViewModelBase).Content = shape.Content;

			return newItem;
		}
	}
}
