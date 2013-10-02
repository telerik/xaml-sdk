using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Diagrams.Core;

namespace CustomSettingsPane
{
	/// <summary>
	/// Interaction logic for Example.xaml
	/// </summary>
	public partial class Example : UserControl
	{
		public Example()
		{
			InitializeComponent();
			SerializationService.Default.ItemSerializing += DefaultSerializationService_ItemSerializing;
		}

		private void RadRadioButton_Click(object sender, RoutedEventArgs e)
		{
			this.diagram.ActiveTool = MouseTool.PointerTool;
		}

		private void RadRadioButton_Click_1(object sender, RoutedEventArgs e)
		{
			this.diagram.ActiveTool = MouseTool.PathTool;
		}

		private void RadRadioButton_Click_2(object sender, RoutedEventArgs e)
		{
			this.diagram.ActiveTool = MouseTool.PencilTool;
		}

		private void DefaultSerializationService_ItemSerializing(object sender, SerializationEventArgs<IDiagramItem> e)
		{
			if (e.Entity != null && e.Entity is RadDiagramShape)
			{
				var shape = e.Entity as RadDiagramShape;
				if (shape.Geometry != null)
				{
					e.SerializationInfo["MyGeometry"] = (e.Entity as RadDiagramShape).Geometry.ToString();
				}
			}
		}
	}
}
