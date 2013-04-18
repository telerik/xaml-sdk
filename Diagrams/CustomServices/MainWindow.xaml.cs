using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CustomServices;
using Telerik.Windows.Controls;
using Telerik.Windows.Diagrams.Core;

namespace CustomServices_WPF
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private MyDragging newDraggingService;
		private MyRotation newRotationService;
		private MyResizing newResizingService;

		public MainWindow()
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
