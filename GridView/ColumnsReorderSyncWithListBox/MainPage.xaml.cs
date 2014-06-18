using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Telerik.Windows.Controls;
using Telerik.Windows.DragDrop;

namespace ColumnsReorderSyncWithListBoxSL
{
	public partial class MainPage : UserControl
	{
		private MyViewModel viewModel;
		private int count = 0;

		public MainPage()
		{
			InitializeComponent();

			this.Loaded += MainPage_Loaded;

			DragDropManager.AddDragInitializeHandler(this.ColumnsListBox, OnDragInitialize);
			DragDropManager.AddDropHandler(this.ColumnsListBox, OnDrop);
			DragDropManager.AddGiveFeedbackHandler(this.ColumnsListBox, OnGiveFeedback);

			this.clubsGrid.ColumnReordered += clubsGrid_ColumnReordered;
		}

		void clubsGrid_ColumnReordered(object sender, GridViewColumnEventArgs e)
		{
			RebindListBox();
		}

		private void OnGiveFeedback(object sender, GiveFeedbackEventArgs e)
		{
			e.SetCursor(Cursors.Arrow);
			e.Handled = true;
		}

		private void OnDrop(object sender, Telerik.Windows.DragDrop.DragEventArgs e)
		{
			var item = e.OriginalSource as RadListBoxItem ?? (e.OriginalSource as FrameworkElement).ParentOfType<RadListBoxItem>();
			var listBox = sender as RadListBox;

			if (item != null && listBox != null)
			{
				var targetColumn = item.DataContext as GridViewColumn;
				var draggedColumn = DragDropPayloadManager.GetDataFromObject(e.Data, "DraggedColumn") as GridViewDataColumn;

				draggedColumn.DisplayIndex = targetColumn.DisplayIndex;

				RebindListBox();
			}
		}
  
		private void RebindListBox()
		{

			var sourceBinding = this.ColumnsListBox.GetValue(RadListBox.ItemsSourceProperty);
			this.ColumnsListBox.ClearValue(RadListBox.ItemsSourceProperty);
			this.ColumnsListBox.SetValue(RadListBox.ItemsSourceProperty, sourceBinding);
		}

		private void OnDragInitialize(object sender, DragInitializeEventArgs e)
		{
			var draggedColumn = ((e.OriginalSource as RadListBoxItem) ?? (e.OriginalSource as FrameworkElement).ParentOfType<RadListBoxItem>()).DataContext as GridViewColumn;

			var dragVisual = new DragVisual() { Content = draggedColumn.Header };
			var payload = DragDropPayloadManager.GeneratePayload(null);
			payload.SetData("DraggedColumn", draggedColumn);

			e.DragVisual = dragVisual;
			e.Data = payload;
			e.AllowedEffects = DragDropEffects.All;
		}

		void MainPage_Loaded(object sender, RoutedEventArgs e)
		{
			this.viewModel = this.Resources["MyViewModel"] as MyViewModel;

			for (int i = 0; i < 3; i++)
			{
				this.AddColumn();
			}
		}

		private void Button1_Click(object sender, RoutedEventArgs e)
		{
			AddColumn();
		}
  
		private void AddColumn()
		{
			GridViewDataColumn column = new GridViewDataColumn();

			column.Header = "Name" + count;
			column.DataMemberBinding = new System.Windows.Data.Binding("Name") { Mode = System.Windows.Data.BindingMode.TwoWay };

			column.DisplayIndex = count;
			
			count++;

			this.clubsGrid.Columns.Add(column);

			RebindListBox();
		}
	}
}