using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Diagrams;

namespace CustomSettingsPane.ViewModels
{
	public class MainViewModel : ViewModelBase
	{
		public ObservableCollection<CustomGallery> Items { get; set; }

		public GraphSource GraphSource { get; set; }

		public MainViewModel()
		{
		   this.LoadToolBoxGalleries();
		   this.AddToGalleryCommand = new DelegateCommand((param) => 
		   {
			   this.CreateToolBoxItemFromGeometry(param);
		   });
		   this.GraphSource = new GraphSource();
		}

		private void CreateToolBoxItemFromGeometry(object parameter)
		{			
			this.Items[(int)parameter].Shapes.Add(this.CopyShapeViewModel());
			this.Items[(int)parameter].IsSelected = true;
		}

		private ShapeViewModel CopyShapeViewModel()
		{
			Geometry clonedGeom = this.SelectedShapeModel.Geometry.Clone();
			ShapeViewModel model = new ShapeViewModel()
			{
				Geometry = clonedGeom,
				ShapeName = this.SelectedShapeModel.ShapeName,
			};
			return model;
		}

		public DelegateCommand AddToGalleryCommand
		{
			get;
			set;
		}

		private ShapeViewModel selectedShapeModel;
		public ShapeViewModel SelectedShapeModel
		{
			get { return this.selectedShapeModel; }
			set
			{
				if (this.selectedShapeModel != value)
				{
					this.selectedShapeModel = value;
					this.OnPropertyChanged("SelectedShapeModel");
				}
			}
		}
	
		private void LoadToolBoxGalleries()
		{
			this.Items = new ObservableCollection<CustomGallery>();
			//create and populate the first custom gallery
			CustomGallery firstGallery = new CustomGallery { Header = "First Gallery" };
			firstGallery.Shapes.Add(new ShapeViewModel
			{
				ShapeName = "Shape 1.1",
				Geometry = ShapeFactory.GetShapeGeometry(CommonShapeType.CloudShape)
			});
			firstGallery.Shapes.Add(new ShapeViewModel
			{
				ShapeName = "Shape 1.2",
				Geometry = ShapeFactory.GetShapeGeometry(CommonShapeType.EllipseShape)
			});
			firstGallery.Shapes.Add(new ShapeViewModel
			{
				ShapeName = "Shape 1.3",
				Geometry = ShapeFactory.GetShapeGeometry(CommonShapeType.HexagonShape)
			});
			firstGallery.Shapes.Add(new ShapeViewModel
			{
				ShapeName = "Shape 1.4",
				Geometry = ShapeFactory.GetShapeGeometry(CommonShapeType.PentagonShape)
			});
			firstGallery.Shapes.Add(new ShapeViewModel
			{
				ShapeName = "Shape 1.5",
				Geometry = ShapeFactory.GetShapeGeometry(CommonShapeType.RightTriangleShape)
			});
			this.Items.Add(firstGallery);

			//create and populate the second custom gallery
			CustomGallery secondGallery = new CustomGallery { Header = "Second Gallery" };
			secondGallery.Shapes.Add(new ShapeViewModel
			{
				ShapeName = "Shape 2.1",
				Geometry = ShapeFactory.GetShapeGeometry(FlowChartShapeType.CardShape)
			});
			secondGallery.Shapes.Add(new ShapeViewModel
			{
				ShapeName = "Shape 2.2",
				Geometry = ShapeFactory.GetShapeGeometry(FlowChartShapeType.Database1Shape)
			});
			secondGallery.Shapes.Add(new ShapeViewModel
			{
				ShapeName = "Shape 2.3",
				Geometry = ShapeFactory.GetShapeGeometry(FlowChartShapeType.CollateShape)
			});
			secondGallery.Shapes.Add(new ShapeViewModel
			{
				ShapeName = "Shape 2.4",
				Geometry = ShapeFactory.GetShapeGeometry(FlowChartShapeType.DataShape)
			});
			secondGallery.Shapes.Add(new ShapeViewModel
			{
				ShapeName = "Shape 2.5",
				Geometry = ShapeFactory.GetShapeGeometry(FlowChartShapeType.DisplayShape)
			});
			this.Items.Add(secondGallery);
			CustomGallery customGal = new CustomGallery() { Header = "Custom Shapes" };
			this.Items.Add(customGal);
		}
	}
}