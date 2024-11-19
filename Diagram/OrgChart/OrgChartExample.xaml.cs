using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using OrgChart.ViewModels;
using Telerik.Windows;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Diagrams;
using Telerik.Windows.Controls.Diagrams.Extensions;
using Telerik.Windows.Diagrams.Core;

namespace OrgChart
{
	public partial class OrgChartExample
	{
		private OrgChartViewModel viewModel;
		private TreeLayout treeLayout;

		public OrgChartExample()
		{
			InitializeComponent();
			this.GetViewModelAndBindForEvents();
			this.Loaded += this.OnOrgChartExampleLoaded;
			this.BindComboBoxes();

			// Sets the OrgTreeRouter as default router.
			this.diagram.RoutingService.Router = this.viewModel.Router;

			// Creates the Laoyou object which layouts the shapes.
			this.treeLayout = new TreeLayout();
			this.BindOrgTreeView();
		}

		private void BindOrgTreeView()
		{
			this.OrgTreeView.ItemsSource = this.viewModel.HierarchicalDataSource;
		}

		private void GetViewModelAndBindForEvents()
		{
			this.viewModel = this.RootGrid.Resources["ViewModel"] as OrgChartViewModel;
			this.viewModel.ChildTreeLayoutViewModel.LayoutSettingsChanged += (_, __) => this.LayoutOrgChart(false);
			this.viewModel.ChildrenExpandedOrCollapsed += this.OnViewModelChildrenExpandedOrCollapsed;
		}

		private void OnViewModelChildrenExpandedOrCollapsed(object sender, EventArgs e)
		{
			if (this.viewModel.ShouldLayoutAfterExpandCollapse == true)
			{
				this.LayoutOrgChart(false);
			}
			this.navigationPane.RefreshThumbnail();
		}

		private void TemplateChangeButtonClick(object sender, RoutedEventArgs e)
		{
			if (this.viewModel.ShouldLayoutAfterTemplateChange == true)
			{
				this.InvokeDispatchedLayout(false);
			}
		}

		private void OnOrgChartExampleLoaded(object sender, RoutedEventArgs e)
		{
			this.navigationPane.RefreshZoomSlider();

			this.diagram.SizeChanged += this.OnSizeChanged;
			this.BindSelectionChangedEvents();
			this.SetLayoutRoots();
			this.LayoutOrgChart(false);
		}

		private void OnSizeChanged(object sender, SizeChangedEventArgs e)
		{
			this.LayoutOrgChart(false);
			this.diagram.SizeChanged -= this.OnSizeChanged;
		}

		private void BindSelectionChangedEvents()
		{
			this.viewModel.CurrentLayoutTypeChanged += (_, __) => this.LayoutOrgChart(false);
		}

		private void InvokeDispatchedLayout(bool shouldAutoFit)
		{
			Action action = new Action(() => this.LayoutOrgChart(shouldAutoFit));
			this.Dispatcher.BeginInvoke(action, DispatcherPriority.Background);
		}

		private void BindComboBoxes()
		{
			this.employeesComboBox.ItemsSource = this.viewModel.GraphSource.InternalItems;
		}

		private void TreeLayoutButton_Click(object sender, RoutedEventArgs e)
		{
			this.LayoutOrgChart(false);
		}


		//Sets the Layout roots in the Layout Settings.
		private void SetLayoutRoots()
		{
			foreach (var item in this.viewModel.HierarchicalDataSource)
			{
				RadDiagramShape shape = this.diagram.ContainerGenerator.ContainerFromItem(item) as RadDiagramShape;
				this.viewModel.ChildTreeLayoutViewModel.CurrentLayoutSettings.Roots.Add(shape);
			}
		}

		// Suppresses the connection update from invoking too many times. Ensures the connectors and unvokes the Layout operation.
		private void LayoutOrgChart(bool shouldAutoFit)
		{
			// suspend auto update for all connections:
			this.diagram.Connections.ForEach(x => RadDiagramConnection.SetIsAutoUpdateSuppressed((RadDiagramConnection)x, true));
			this.EnsureConnectors();
			this.treeLayout.Layout(this.diagram, this.viewModel.ChildTreeLayoutViewModel.CurrentLayoutSettings);

			// unsuspend auto update for all connections & update:
			this.diagram.Connections.ForEach(x => RadDiagramConnection.SetIsAutoUpdateSuppressed((RadDiagramConnection)x, false));
			this.diagram.Connections.ForEach(x => x.Update());

			if (shouldAutoFit)
			{
				this.diagram.AutoFit(new Thickness(10), false);
			}
		}

		//Creates custom connectors needed for the TipOverTree layout.
		private void EnsureConnectors()
		{
			if (this.viewModel.CurrentTreeLayoutType == TreeLayoutType.TipOverTree)
			{
				var shapesWithIncomingLinks = this.diagram.Shapes.Where(x => x.IncomingLinks.Any()).ToList();
				shapesWithIncomingLinks.ForEach(y =>
				{
					if (y.Connectors.Count == 5)
					{
						var customConnector = new RadDiagramConnector { Offset = new Point(0.15, 1) };
						customConnector.Name = CustomConnectorPosition.TreeLeftBottom;
						y.Connectors.Add(customConnector);
					}
				});
			}
		}

		private void EmployeesComboBoxKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
		{
			if (e.Key == Key.Escape)
			{
				this.employeesComboBox.SelectedItem = null;
				this.employeesComboBox.Text = string.Empty;
			}
			else if (e.Key == Key.Enter)
			{
				var selectedEmployee = this.employeesComboBox.SelectedItem as Node;
				if (selectedEmployee != null)
				{
					this.TreeBringIntoView(selectedEmployee);
					this.DiagramBringIntoView(selectedEmployee);
					this.DiagramExpandNodeByPath(selectedEmployee.Path);
				}
			}
		}

		private void EmployeesComboBoxDropDownClosed(object sender, EventArgs e)
		{
			var selectedEmployee = this.employeesComboBox.SelectedItem as Node;
			if (selectedEmployee != null)
			{
				this.TreeBringIntoView(selectedEmployee);
				this.DiagramBringIntoView(selectedEmployee);
				this.DiagramExpandNodeByPath(selectedEmployee.Path);
			}
		}

		private void TreeBringIntoView(Node node)
		{
			this.OrgTreeView.SelectedItems.Clear();
			this.OrgTreeView.BringPathIntoView(node.Path);
		}

		/// <summary>
		/// Expands a diagram shape given a path.
		/// </summary>
		private void DiagramExpandNodeByPath(string path)
		{
			string[] pathParts = path.Split('|');
			if (pathParts.Length < 2) return;
			for (int i = 0; i < pathParts.Length - 1; i++)
			{
				var allLinks = this.viewModel.GraphSource.Links.ToList();
				var currLink = allLinks.FirstOrDefault(x => (x.Source as Node).FullName == pathParts[i] && (x.Target as Node).FullName == pathParts[i + 1]) as Link;
				if (currLink != null)
				{
					currLink.Visibility = Visibility.Visible;
					currLink.Source.Visibility = Visibility.Visible;
					currLink.Source.AreChildrenCollapsed = false;

					foreach (var item in currLink.Source.Children)
					{
						var link = this.viewModel.GraphSource.Links.FirstOrDefault(x => x.Source == currLink.Source && x.Target == item) as Link;
						link.Visibility = Visibility.Visible;
						item.Visibility = Visibility.Visible;
					}
				}
			}
			this.navigationPane.RefreshThumbnail();
		}

		// Brings a particular node intoview.
		private void DiagramBringIntoView(Node node)
		{
			this.diagram.SelectedIndex = -1;
			var shape = this.diagram.ContainerGenerator.ContainerFromItem(node) as RadDiagramShape;
			if (shape != null)
			{
				this.diagram.BringIntoView(shape, 1, true);
			}
			shape.IsSelected = true;
		}

		private void LayoutButtonClicked(object sender, RoutedEventArgs e)
		{
			this.LayoutOrgChart(false);
		}

		private void LayoutTypeButtonClick(object sender, RoutedEventArgs e)
		{
			RadGeometryButton button = sender as RadGeometryButton;
			Geometry geometry = button.Geometry.Clone();
			this.LayoutTypeDropDown.Geometry = geometry;

			string layoutType = button.Content.ToString();
			this.viewModel.CurrentTreeLayoutType = (TreeLayoutType)Enum.Parse(typeof(TreeLayoutType), layoutType, true);
		}

		private void OrgTreeViewItemClick(object sender, RadRoutedEventArgs e)
		{
			var item = e.OriginalSource as RadTreeViewItem;
			if (item != null)
			{
				Node node = item.DataContext as Node;
				this.DiagramBringIntoView(node);
				this.DiagramExpandNodeByPath(node.Path);
			}
		}
	}
}