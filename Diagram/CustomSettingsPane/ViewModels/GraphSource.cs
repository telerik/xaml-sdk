using System;
using System.Linq;
using Telerik.Windows.Controls.Diagrams;
using Telerik.Windows.Controls.Diagrams.Extensions.ViewModels;
using Telerik.Windows.Diagrams.Core;

namespace CustomSettingsPane.ViewModels
{
	public class GraphSource : SerializableGraphSourceBase<ShapeViewModel, Link>
	{
		static int idCounter = 0;
		public override string GetNodeUniqueId(ShapeViewModel node)
		{
			return node.ID;
		}

		public override ShapeViewModel DeserializeNode(IShape shape, SerializationInfo info)
		{
			ShapeViewModel model = new ShapeViewModel()
			{
				 Geometry = GeometryParser.GetGeometry(info["MyGeometry"].ToString())
			};
			return model;
		}

		public override void AddNode(ShapeViewModel node)
		{
			if (node != null)
			{
				node.ID = (idCounter++) + "";
				base.AddNode(node);
			}
		}
	}
}
