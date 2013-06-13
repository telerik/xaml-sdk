using System;
using System.Linq;
using Telerik.Windows.Controls.Diagrams;
using Telerik.Windows.Controls.Diagrams.Extensions.ViewModels;

namespace CustomSettingsPane.ViewModels
{
	public class GraphSource : SerializableGraphSourceBase<ShapeViewModel, Link>
	{
		static int idCounter = 0;
		public override string GetNodeUniqueId(ShapeViewModel node)
		{
			return node.ID;
		}

		public override ShapeViewModel DeserializeNode(Telerik.Windows.Diagrams.Core.IShape shape, Telerik.Windows.Diagrams.Core.SerializationInfo info)
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
				//node.BackgroundBrush = new SolidColorBrush(){ Color = Color.FromArgb(255, 0, 192, 255) };
				base.AddNode(node);
			}
		}

		public override object CreateNode(Telerik.Windows.Diagrams.Core.IShape shape)
		{
			return base.CreateNode(shape);
		}
	}
}
