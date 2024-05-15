using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using Telerik.Windows.Controls;
using Telerik.Windows.DragDrop;

namespace RestoredTilesToSpanMultipleRowsAndColumns
{
	public class MultipleRowsAndColumnsPanel : TileViewPanel
	{
		public int ColumnsCount { get; set; }
		public int RowsCount { get; set; }
		private bool isTileDragged;
		private RadTileView itemsOwner;
		private RadTileViewItem firstTile;
		private RadTileViewItem secondTile;
		private KeyValuePair<int, int> freeSlot;
		private Point lastMousePosition;

		private bool[,] populatedCells;
		private int[] rowOrder = new int[8] { -1, 0, 1, -1, 1, -1, 0, 1 };
		private int[] columnOrder = new int[8] { -1, -1, -1, 0, 0, 1, 1, 1 };

		public MultipleRowsAndColumnsPanel()
		{
			this.freeSlot = new KeyValuePair<int, int>(-1, -1);
			DragDropManager.AddDragOverHandler(this, new Telerik.Windows.DragDrop.DragEventHandler(this.OnDragOver));
		}

		protected override void OnInitialized(EventArgs e)
		{
			base.OnInitialized(e);

			populatedCells = new bool[this.RowsCount, this.ColumnsCount];
		}

		private void OnDragOver(object sender, Telerik.Windows.DragDrop.DragEventArgs e)
		{
			lastMousePosition = e.GetPosition(this);
		}

		private void ItemsOwner_TileDragEnded(object sender, TileViewDragEventArgs e)
		{
			Mouse.Capture(null);
			(e.DraggedItem as FrameworkElement).ReleaseMouseCapture();
			var columnWidth = this.DesiredSize.Width / this.ColumnsCount;
			var rowHeight = this.DesiredSize.Height / this.RowsCount;

			var column = (int)(this.lastMousePosition.X / columnWidth);
			var row = (int)(this.lastMousePosition.Y / rowHeight);
			var item = e.DraggedItem as RadTileViewItem;

			if (!this.TrySwapTiles(item, this.secondTile))
			{
				var currentRow = TileViewAttachedProperties.GetRow(item);
				var currentColumn = TileViewAttachedProperties.GetColumn(item);
				var rowSpan = TileViewAttachedProperties.GetRowSpan(item);
				var columnSpan = TileViewAttachedProperties.GetColumnSpan(item);
				this.MarkCells(currentRow, currentColumn, rowSpan, columnSpan, false);

				TileViewAttachedProperties.SetColumn(item, column);
				TileViewAttachedProperties.SetRow(item, row);

				this.FixOverlapping(row, column, rowSpan, columnSpan);
			}

			this.secondTile = null;
			this.isTileDragged = false;
			this.InvalidateMeasure();
		}

		private void ItemsOwner_TileDragStarted(object sender, TileViewDragEventArgs e)
		{
			this.isTileDragged = true;
			this.firstTile = e.DraggedItem as RadTileViewItem;
		}

		private void OnItemsOwnerTilePositionChanged(object sender, Telerik.Windows.RadRoutedEventArgs e)
		{
			if (!this.itemsOwner.IsLoaded || this.secondTile != null)
			{
				return;
			}

			this.secondTile = e.OriginalSource as RadTileViewItem;
		}

		protected override System.Windows.Size MeasureOverride(System.Windows.Size availableSize)
		{
			if (this.itemsOwner == null)
			{
				this.EnsureItemsOwner();
				base.MeasureOverride(availableSize);
			}
			var columnWidth = availableSize.Width / this.ColumnsCount;
			var rowHeight = availableSize.Height / this.RowsCount;

			foreach (var item in this.Children.OfType<RadTileViewItem>().Where(t => t.Visibility == Visibility.Visible))
			{
				var desiredColumnSpan = TileViewAttachedProperties.GetColumnSpan(item);
				var desiredRowSpan = TileViewAttachedProperties.GetRowSpan(item);
				var column = TileViewAttachedProperties.GetColumn(item);
				var row = TileViewAttachedProperties.GetRow(item);
				var columnSpan = Math.Max(1, Math.Min(this.ColumnsCount - column, desiredColumnSpan));

				var rowSpan = Math.Max(1, Math.Min(this.RowsCount - row, desiredRowSpan));


				item.Measure(new Size(columnSpan * columnWidth, rowSpan * rowHeight));

				this.MarkCells(row, column, rowSpan, columnSpan, true);
			}

			return availableSize;
		}

		protected override System.Windows.Size ArrangeOverride(System.Windows.Size finalSize)
		{
			var columnWidth = finalSize.Width / this.ColumnsCount;
			var rowHeight = finalSize.Height / this.RowsCount;

			foreach (var item in this.Children.OfType<RadTileViewItem>().Where(t => t.Visibility == Visibility.Visible))
			{
				var desiredColumnSpan = TileViewAttachedProperties.GetColumnSpan(item);
				var desiredRowSpan = TileViewAttachedProperties.GetRowSpan(item);
				var column = TileViewAttachedProperties.GetColumn(item);
				var row = TileViewAttachedProperties.GetRow(item);
				var columnSpan = Math.Max(1, Math.Min(this.ColumnsCount - column, desiredColumnSpan));
				var rowSpan = Math.Max(1, Math.Min(this.RowsCount - row, desiredRowSpan));
				item.Arrange(new Rect(new Point(column * columnWidth - this.HorizontalOffset, row * rowHeight - this.VerticalOffset), new Size(columnSpan * columnWidth, rowSpan * rowHeight)));
			}
			return finalSize;
		}

		private bool IsSlotAvailable(int row, int column, int rowSpan, int columnSpan)
		{
			for (int currentRow = row; currentRow < row + rowSpan; currentRow++)
			{
				for (int currentColumn = column; currentColumn < column + columnSpan; currentColumn++)
				{
					if (currentRow >= this.RowsCount || currentColumn >= this.ColumnsCount || populatedCells[currentRow, currentColumn])
					{
						return false;
					}
				}
			}
			return true;
		}

		private List<RadTileViewItem> GetOverlappingTiles(int row, int column, int rowSpan, int columnSpan)
		{
			var overlappingTiles = new List<RadTileViewItem>();
			for (int currentRow = row; currentRow < row + rowSpan; currentRow++)
			{
				for (int currentColumn = column; currentColumn < column + columnSpan; currentColumn++)
				{
					if ((currentRow == row && currentColumn == column) || (currentColumn >= this.ColumnsCount || currentRow >= this.RowsCount))
					{
						continue;
					}

					if (populatedCells[currentRow, currentColumn])
					{
						var tile = this.Children.OfType<RadTileViewItem>().
									FirstOrDefault(t => TileViewAttachedProperties.GetRow(t) == currentRow && TileViewAttachedProperties.GetColumn(t) == currentColumn);

						if (tile != null && !overlappingTiles.Contains(tile))
						{
							overlappingTiles.Add(tile);
						}
					}
				}
			}

			return overlappingTiles;
		}

		private bool TrySwapTiles(RadTileViewItem tile1, RadTileViewItem tile2)
		{
			if (tile1 == null || tile2 == null)
			{
				return false;
			}

			var fColumn = TileViewAttachedProperties.GetColumn(tile1);
			var fRow = TileViewAttachedProperties.GetRow(tile1);
			var fRowSpan = TileViewAttachedProperties.GetRowSpan(tile1);
			var fColumnSpan = TileViewAttachedProperties.GetColumnSpan(tile1);

			var sColumn = TileViewAttachedProperties.GetColumn(tile2);
			var sRow = TileViewAttachedProperties.GetRow(tile2);
			var sRowSpan = TileViewAttachedProperties.GetRowSpan(tile2);
			var sColumnSpan = TileViewAttachedProperties.GetColumnSpan(tile2);

			this.MarkCells(fRow, fColumn, fRowSpan, fColumnSpan, false);
			this.MarkCells(sRow, sColumn, sRowSpan, sColumnSpan, false);
			TileViewAttachedProperties.SetColumn(tile1, sColumn);
			TileViewAttachedProperties.SetRow(tile1, sRow);

			TileViewAttachedProperties.SetColumn(tile2, fColumn);
			TileViewAttachedProperties.SetRow(tile2, fRow);


			this.MarkCells(sRow, sColumn, fRowSpan, fColumnSpan, true);
			this.MarkCells(fRow, fColumn, sRowSpan, sColumnSpan, true);

			this.FixOverlapping(sRow, sColumn, fRowSpan, fColumnSpan);
			this.FixOverlapping(fRow, fColumn, sRowSpan, sColumnSpan);

			return true;
		}

		private void MarkCells(int row, int column, int rowSpan, int columnSpan, bool isPopulated)
		{
			for (int currentRow = row; currentRow < row + rowSpan; currentRow++)
			{
				for (int currentColumn = column; currentColumn < column + columnSpan; currentColumn++)
				{
					if (currentColumn >= this.ColumnsCount || currentRow >= this.RowsCount)
					{
						return;
					}
					populatedCells[currentRow, currentColumn] = isPopulated;
				}
			}
		}

		private void EnsureItemsOwner()
		{
			if (this.itemsOwner == null)
			{
				this.itemsOwner = ItemsControl.GetItemsOwner(this) as RadTileView;

				if (this.itemsOwner != null)
				{
					this.itemsOwner.TilePositionChanged += OnItemsOwnerTilePositionChanged;
					(this.itemsOwner.Items as INotifyCollectionChanged).CollectionChanged += MultipleRowsAndColumnsPanel_CollectionChanged;
					this.itemsOwner.TileDragStarted += ItemsOwner_TileDragStarted;
					this.itemsOwner.TileDragEnded += ItemsOwner_TileDragEnded;
				}
			}
		}

		private void MultipleRowsAndColumnsPanel_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			switch (e.Action)
			{
				case NotifyCollectionChangedAction.Add:
					break;
				case NotifyCollectionChangedAction.Move:
					break;
				case NotifyCollectionChangedAction.Remove:
					var removedItem = e.OldItems[0] as RadTileViewItem;
					var row = TileViewAttachedProperties.GetRow(removedItem);
					var column = TileViewAttachedProperties.GetColumn(removedItem);
					var rowSpan = TileViewAttachedProperties.GetRowSpan(removedItem);
					var columnSpan = TileViewAttachedProperties.GetColumnSpan(removedItem);

					this.MarkCells(row, column, rowSpan, columnSpan, false);
					this.InvalidateMeasure();
					break;
				case NotifyCollectionChangedAction.Replace:
					break;
				case NotifyCollectionChangedAction.Reset:
					break;
				default:
					break;
			}
		}

		private void FindEmptyFirstCell(int startingRow, int startingColumn, int rowSpan, int columnSpan, bool[,] searchedCells)
		{
			if (searchedCells[startingRow, startingColumn] || freeSlot.Key != -1)
			{
				return;
			}

			for (int i = 0; i < rowOrder.Length; i++)
			{
				int newRow = startingRow + rowOrder[i];
				int newColumn = startingColumn + columnOrder[i];
				if (newRow < 0 || newColumn < 0 || newRow >= this.RowsCount || newColumn >= this.ColumnsCount)
				{
					continue;
				}
				searchedCells[startingRow, startingColumn] = true;
				if (this.IsSlotAvailable(newRow, newColumn, rowSpan, columnSpan))
				{
					this.freeSlot = new KeyValuePair<int, int>(newRow, newColumn);
					return;
				}

				this.FindEmptyFirstCell(newRow, newColumn, rowSpan, columnSpan, searchedCells);
			}
		}

		private void FixOverlapping(int row, int column, int rowSpan, int columnSpan)
		{
			var overlappingTiles = GetOverlappingTiles(row, column, rowSpan, columnSpan);

			foreach (var tile in overlappingTiles)
			{
				var desiredRow = TileViewAttachedProperties.GetRow(tile);
				var desiredColumn = TileViewAttachedProperties.GetColumn(tile);

				var desiredRowSpan = TileViewAttachedProperties.GetRowSpan(tile);
				var desiredColumnSpan = TileViewAttachedProperties.GetColumnSpan(tile);

				this.MarkCells(desiredRow, desiredColumn, desiredRowSpan, desiredColumnSpan, false);
			}

			this.MarkCells(row, column, rowSpan, columnSpan, true);

			foreach (var tile in overlappingTiles)
			{
				var desiredRow = TileViewAttachedProperties.GetRow(tile);
				var desiredColumn = TileViewAttachedProperties.GetColumn(tile);

				var desiredRowSpan = TileViewAttachedProperties.GetRowSpan(tile);
				var desiredColumnSpan = TileViewAttachedProperties.GetColumnSpan(tile);

				this.FindEmptyFirstCell(desiredRow, desiredColumn, desiredRowSpan, desiredColumnSpan, new bool[this.RowsCount, this.ColumnsCount]);
				var slot = this.freeSlot;
				if (slot.Key != -1)
				{
					TileViewAttachedProperties.SetRow(tile, slot.Key);
					TileViewAttachedProperties.SetColumn(tile, slot.Value);

					this.MarkCells(slot.Key, slot.Value, desiredRowSpan, desiredColumnSpan, true);
				}
				this.freeSlot = new KeyValuePair<int, int>(-1, -1);

			}
		}
	}
}
