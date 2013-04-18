using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Telerik.Windows.Controls;
using Telerik.Windows.Diagrams.Core;

namespace CustomServices
{
	public partial class MainPage : UserControl
	{
		private MyDragging newDraggingService;
		private MyRotation newRotationService;
		private MyResizing newResizingService;

		public MainPage()
		{
			InitializeComponent();

			this.InitializeNewServices();

			this.diagram.ServiceLocator.Register<IDraggingService>(this.newDraggingService);
			this.diagram.ServiceLocator.Register<IRotationService>(this.newRotationService);
			this.diagram.ServiceLocator.Register<IResizingService>(this.newResizingService);
		}

		private void InitializeNewServices()
		{
			this.newDraggingService = new MyDragging(this.diagram);
			Binding binding = new Binding("IsOn");
			binding.Source = this.newDraggingService;
			binding.Mode = BindingMode.TwoWay;
			this.toggleDrag.SetBinding(RadToggleButton.IsCheckedProperty, binding);
			binding = new Binding("IsRestrictedToBounds");
			binding.Source = this.newDraggingService;
			binding.Mode = BindingMode.TwoWay;
			this.IsRestrictedToBounds.SetBinding(RadToggleButton.IsCheckedProperty, binding);
			binding = new Binding("UseRotaitonBounds");
			binding.Source = this.newDraggingService;
			binding.Mode = BindingMode.TwoWay;
			this.useRotaitonBounds.SetBinding(RadToggleButton.IsCheckedProperty, binding);

			this.newRotationService = new MyRotation(this.diagram) { RotationStep = 45 };
			binding = new Binding("RotationStep");
			binding.Source = this.newRotationService;
			binding.Mode = BindingMode.TwoWay;
			this.rotationStep.SetBinding(TextBox.TextProperty, binding);

			this.newResizingService = new MyResizing(this.diagram);
			binding = new Binding("CanResizeWidth");
			binding.Source = this.newResizingService;
			binding.Mode = BindingMode.TwoWay;
			this.resizeWidth.SetBinding(RadToggleButton.IsCheckedProperty, binding);
			binding = new Binding("CanResizeHeight");
			binding.Source = this.newResizingService;
			binding.Mode = BindingMode.TwoWay;
			this.resizeHeight.SetBinding(RadToggleButton.IsCheckedProperty, binding);
		}

		private void SetRestrictRect(Rect rect)
		{
			if (this.newDraggingService != null)
				this.newDraggingService.DragAllowedArea = rect.InflateRect(-rect.Width / 4, -rect.Height / 4);
		}

		private void OnDiagramViewportChanged(object sender, PropertyEventArgs<Rect> e)
		{
			this.SetRestrictRect(e.NewValue);
			this.border.Width = (e.NewValue.Width / 2) * this.diagram.Zoom;
			this.border.Height = (e.NewValue.Height / 2) * this.diagram.Zoom;
		}
	}
}
