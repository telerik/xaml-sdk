using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Controls.Diagrams;

namespace CustomToolboxDragDrop_WPF.ViewModels
{
    public class MainViewModel
    {
        public ObservableCollection<MyGallery> Items { get; set; }
        public MainViewModel()
        {
            this.Items = new ObservableCollection<MyGallery>();
            //create and populate the first custom gallery
            MyGallery firstGallery = new MyGallery { Header = "First Gallery" };
            firstGallery.Shapes.Add(new MyShape
            {
                Header = "Shape 1.1",
                Geometry = ShapeFactory.GetShapeGeometry(CommonShapeType.CloudShape)
            });
            firstGallery.Shapes.Add(new MyShape
            {
                Header = "Shape 1.2",
                Geometry = ShapeFactory.GetShapeGeometry(CommonShapeType.EllipseShape)
            });
            firstGallery.Shapes.Add(new MyShape
            {
                Header = "Shape 1.3",
                Geometry = ShapeFactory.GetShapeGeometry(CommonShapeType.HexagonShape)
            });
            firstGallery.Shapes.Add(new MyShape
            {
                Header = "Shape 1.4",
                Geometry = ShapeFactory.GetShapeGeometry(CommonShapeType.PentagonShape)
            });
            firstGallery.Shapes.Add(new MyShape
            {
                Header = "Shape 1.5",
                Geometry = ShapeFactory.GetShapeGeometry(CommonShapeType.RightTriangleShape)
            });
            this.Items.Add(firstGallery);

            //create and populate the second custom gallery
            MyGallery secondGallery = new MyGallery { Header = "Second Gallery" };
            secondGallery.Shapes.Add(new MyShape
            {
                Header = "Shape 2.1",
                Geometry = ShapeFactory.GetShapeGeometry(FlowChartShapeType.CardShape)
            });
            secondGallery.Shapes.Add(new MyShape
            {
                Header = "Shape 2.2",
                Geometry = ShapeFactory.GetShapeGeometry(FlowChartShapeType.Database1Shape)
            });
            secondGallery.Shapes.Add(new MyShape
            {
                Header = "Shape 2.3",
                Geometry = ShapeFactory.GetShapeGeometry(FlowChartShapeType.CollateShape)
            });
            secondGallery.Shapes.Add(new MyShape
            {
                Header = "Shape 2.4",
                Geometry = ShapeFactory.GetShapeGeometry(FlowChartShapeType.DataShape)
            });
            secondGallery.Shapes.Add(new MyShape
            {
                Header = "Shape 2.5",
                Geometry = ShapeFactory.GetShapeGeometry(FlowChartShapeType.DisplayShape)
            });
            this.Items.Add(secondGallery);
        }
    }
}
