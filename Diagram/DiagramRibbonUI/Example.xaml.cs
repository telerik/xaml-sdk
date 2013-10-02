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
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Diagrams;
using Telerik.Windows.Controls.Diagrams.Extensions;
using Telerik.Windows.Controls.Diagrams.Primitives;
using Telerik.Windows.Diagrams.Core;
using Telerik.Windows.DragDrop;

namespace DiagramFirstLookDemo
{
	/// <summary>
	/// Interaction logic for Example.xaml
	/// </summary>
	public partial class Example : UserControl
	{
		private readonly FileManager fileManager;
		private bool isRecursion;

		/// <summary>
		/// Initializes static members of the <see cref="MainView"/> class.
		/// </summary>
		static Example()
		{
			var saveBinding = new CommandBinding(DiagramCommands.Save, ExecuteSave, CanExecuteSave);
			var openBinding = new CommandBinding(DiagramCommands.Open, ExecuteOpen, CanExecuteOpen);

			CommandManager.RegisterClassCommandBinding(typeof(Example), saveBinding);
			CommandManager.RegisterClassCommandBinding(typeof(Example), openBinding);
		}
		public Example()
		{
			InitializeComponent();
			this.SamplesList1.DataContext = this.SamplesList.DataContext = new SamplesViewModel(this.diagram);
			this.diagram.ItemsChanged += this.DiagramItemsChanged;
			this.fileManager = new FileManager(this.diagram);
			DragDropManager.AddDragInitializeHandler(this.panelBar, ToolboxDragService.OnDragInitialized);

			//// This is needed so that the code in SL and WPF is the same
			//// for the Undo/Redo list bindings in the SplitButton DropDownContent.
			this.DataContext = this.diagram.UndoRedoService;
			this.panelBar.ItemsSource = new HierarchicalGalleryItemsCollection();
		}

		private void DiagramItemsChanged(object sender, DiagramItemsChangedEventArgs args)
		{
			foreach (var item in from shape in args.OldItems.OfType<IShape>() let incomingLinks = shape.IncomingLinks let outgoingLinks = shape.OutgoingLinks from item in incomingLinks.Union(outgoingLinks).ToList() select item)
			{
				this.diagram.RemoveConnection(item, !item.IsSelected);
			}
		}

		private void RedoSelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			this.UndoRedo(sender as System.Windows.Controls.ListBox, false);
		}

		private void UndoSelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			this.UndoRedo(sender as System.Windows.Controls.ListBox, true);
		}

		private void UndoRedo(System.Windows.Controls.ListBox listBox, bool isUndo)
		{
			if (listBox == null)
				return;

			if (!this.isRecursion)
			{
				var parentButton = listBox.ParentOfType<RadRibbonSplitButton>();
				if (parentButton != null && parentButton.IsOpen)
				{
					this.isRecursion = true;
					var startIndex = listBox.SelectedIndex;
					for (var i = 0; i <= startIndex; i++)
					{
						if (isUndo)
							this.diagram.UndoRedoService.Undo();
						else
							this.diagram.UndoRedoService.Redo();
					}
					this.isRecursion = false;
					parentButton.IsOpen = false;
				}
			}
			listBox.SelectedItem = null;
		}

		private static void CanExecuteSave(object sender, CanExecuteRoutedEventArgs e)
		{
			if (sender == null)
				throw new ArgumentNullException("sender");
			var owner = sender as Example;
			e.CanExecute = owner != null && owner.diagram != null && owner.diagram.Items.Count > 0;
		}

		private static void CanExecuteOpen(object sender, CanExecuteRoutedEventArgs e)
		{
			if (sender == null)
				throw new ArgumentNullException("sender");
			var owner = sender as Example;
			e.CanExecute = owner != null && owner.diagram != null;
		}

		private static void ExecuteSave(object sender, ExecutedRoutedEventArgs e)
		{			
			var owner = sender as Example;
			if (owner != null)
			{
				owner.fileManager.SaveToFile();
				// you can also use the iso;
				//owner.fileManager.SaveToFile(FileManager.FileLocation.IsolatedStorage);
				//owner.ToolBox.Focus();
			}
		}

		private static void ExecuteOpen(object sender, ExecutedRoutedEventArgs e)
		{
			var owner = sender as Example;
			if (owner != null)
			{
				owner.diagram.Clear();
				owner.fileManager.LoadFromFile();
			}
		}

		private void CellWidthSpinner_OnValueChanged(object sender, RadRangeBaseValueChangedEventArgs e)
		{
			if (diagram != null && CellWidthSpinner != null && CellWidthSpinner.Value.HasValue)
				BackgroundGrid.SetCellSize(diagram, new Size(CellWidthSpinner.Value.Value, CellHeightSpinner.Value.Value));
		}

		private void CellHeightSpinner_OnValueChanged(object sender, RadRangeBaseValueChangedEventArgs e)
		{
			if (diagram != null && CellHeightSpinner != null && CellHeightSpinner.Value.HasValue)
				BackgroundGrid.SetCellSize(diagram, new Size(CellWidthSpinner.Value.Value, CellHeightSpinner.Value.Value));
		}

		private void ZoomSpinner_OnValueChanged(object sender, RadRangeBaseValueChangedEventArgs e)
		{
			if (diagram != null && ZoomSpinner != null && ZoomSpinner.Value.HasValue)
				diagram.Zoom = ZoomSpinner.Value.Value / 100d;
		}

		private void RadColorSelector_SelectedColorChanged(object sender, EventArgs e)
		{
			RadColorSelector selector = sender as RadColorSelector;
			if (selector.SelectedColor != null && selector.SelectedColor is Color)
			{
				var color = selector.SelectedColor;
				diagram.Background = new SolidColorBrush(color);
			}
		}

		private void GridColorSelectorOnSelectionChanged(object sender, EventArgs e)
		{
			RadColorSelector selector = sender as RadColorSelector;
			if (selector.SelectedColor != null && selector.SelectedColor is Color)
			{
				var color = selector.SelectedColor;
				BackgroundGrid.SetLineStroke(diagram, new SolidColorBrush(color));
			}
		}

		private void OnExportToHtmlClick(object sender, RoutedEventArgs e)
		{
			HTMLExportHelper.CreateHTMLFile(this.diagram);
		}

		private void OnToolChecked(object sender, RoutedEventArgs e)
		{
			var button = sender as RadRibbonRadioButton;
			if (button != null && this.diagram != null)
			{
				if (button.Name == "ConnectionButton")
				{
					this.diagram.ActiveTool = MouseTool.ConnectorTool;
				}
				else if (button.Name == "TextButton")
				{
					this.diagram.ActiveTool = MouseTool.TextTool;
				}
				else if (button.Name == "PathButton")
				{
					this.diagram.ActiveTool = MouseTool.PathTool;
				}
				else if (button.Name == "PencilButton")
				{
					this.diagram.ActiveTool = MouseTool.PencilTool;
				}
				else
				{
					this.diagram.ActiveTool = MouseTool.PointerTool;
				}
			}
		}

		private void SamplesList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			this.Ribbon.IsBackstageOpen = false;
		}

		private void LayoutButton_Click(object sender, RoutedEventArgs e)
		{
			TreeLayoutSettings settings = new TreeLayoutSettings();
			settings.Roots.Add(diagram.SelectedItem as IShape);

			diagram.Layout(LayoutType.Tree, settings);
		}
  }
}