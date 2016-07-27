using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RestoredTilesToSpanMultipleRowsAndColumns
{
	public class TileViewAttachedProperties
	{
		public static readonly DependencyProperty RowProperty =
			DependencyProperty.RegisterAttached("Row", typeof(int), typeof(TileViewAttachedProperties), new PropertyMetadata(0));

		public static readonly DependencyProperty ColumnProperty =
			DependencyProperty.RegisterAttached("Column", typeof(int), typeof(TileViewAttachedProperties), new PropertyMetadata(0));

		public static readonly DependencyProperty ColumnSpanProperty =
			DependencyProperty.RegisterAttached("ColumnSpan", typeof(int), typeof(TileViewAttachedProperties), new PropertyMetadata(1));

		public static readonly DependencyProperty RowSpanProperty =
			DependencyProperty.RegisterAttached("RowSpan", typeof(int), typeof(TileViewAttachedProperties), new PropertyMetadata(1));

		public static int GetRow(DependencyObject obj)
		{
			return (int)obj.GetValue(RowProperty);
		}

		public static void SetRow(DependencyObject obj, int value)
		{
			obj.SetValue(RowProperty, value);
		}

		public static int GetColumn(DependencyObject obj)
		{
			return (int)obj.GetValue(ColumnProperty);
		}

		public static void SetColumn(DependencyObject obj, int value)
		{
			obj.SetValue(ColumnProperty, value);
		}

		public static int GetColumnSpan(DependencyObject obj)
		{
			return (int)obj.GetValue(ColumnSpanProperty);
		}

		public static void SetColumnSpan(DependencyObject obj, int value)
		{
			obj.SetValue(ColumnSpanProperty, value);
		}

		public static int GetRowSpan(DependencyObject obj)
		{
			return (int)obj.GetValue(RowSpanProperty);
		}

		public static void SetRowSpan(DependencyObject obj, int value)
		{
			obj.SetValue(RowSpanProperty, value);
		}
	}
}
