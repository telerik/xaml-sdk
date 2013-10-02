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
            var shape = e.Entity as RadDiagramShape;
            if (shape != null)
            {
                e.SerializationInfo["MyGeometry"] = shape.Geometry.ToString();

                var myShape = shape.DataContext as MyShape;
                if (myShape != null)
                    e.SerializationInfo["DataContent"] = myShape.Header;
            }
        }

        private void RadDiagram_ShapeDeserialized(object sender, ShapeSerializationRoutedEventArgs e)
        {
            var shape = e.Shape as RadDiagramShape;
            if (shape != null)
            {
                shape.Geometry = GeometryParser.GetGeometry(e.SerializationInfo["MyGeometry"].ToString());
                shape.Content = e.SerializationInfo["DataContent"].ToString();
            }
        }
    }
}
