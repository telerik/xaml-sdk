using CustomToolboxDragDrop_WPF.ViewModels;
using System;
using System.Linq;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Diagrams;
using Telerik.Windows.Diagrams.Core;

namespace CustomToolboxDragDrop_WPF
{
    /// <summary>
    /// Interaction logic for Example.xaml
    /// </summary>
    public partial class Example : UserControl
    {
        public Example()
        {
            InitializeComponent();
            SerializationService.Default.ItemSerializing += Default_ItemSerializing;
        }

        void Default_ItemSerializing(object sender, SerializationEventArgs<IDiagramItem> e)
        {
            if (e.Entity is RadDiagramShape)
            {
                e.SerializationInfo["MyGeometry"] = (e.Entity as RadDiagramShape).Geometry.ToString();
                if ((e.Entity as RadDiagramShape).DataContext is MyShape)
                    e.SerializationInfo["DataContent"] = ((e.Entity as RadDiagramShape).DataContext as MyShape).Header;
            }
        }

        private void RadDiagram_ShapeDeserialized(object sender, ShapeSerializationRoutedEventArgs e)
        {
            if (e.Shape as RadDiagramShape != null)
            {
                (e.Shape as RadDiagramShape).Geometry = GeometryParser.GetGeometry(e.SerializationInfo["MyGeometry"].ToString());
                (e.Shape as RadDiagramShape).Content = e.SerializationInfo["DataContent"].ToString();
            }
        }
    }
}
