using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;

namespace CopyPasteFunctionality
{
	public class GridViewClipboardCustomBehavior
	{
		private readonly RadGridView grid = null;
		private readonly MyViewModel dataContext;

		public GridViewClipboardCustomBehavior(RadGridView grid)
		{
			this.grid = grid;
			this.dataContext = grid.DataContext as MyViewModel;
		}

		public static readonly DependencyProperty IsEnabledProperty
			= DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(GridViewClipboardCustomBehavior),
				new PropertyMetadata(new PropertyChangedCallback(OnIsEnabledPropertyChanged)));

		public static void SetIsEnabled(DependencyObject dependencyObject, bool enabled)
		{
			dependencyObject.SetValue(IsEnabledProperty, enabled);
		}

		public static bool GetIsEnabled(DependencyObject dependencyObject)
		{
			return (bool)dependencyObject.GetValue(IsEnabledProperty);
		}

		private static void OnIsEnabledPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
		{
			var grid = dependencyObject as RadGridView;
			if (grid != null)
			{
				var clipboardBehavior = new GridViewClipboardCustomBehavior(grid);
				if ((bool)e.NewValue)
				{
					clipboardBehavior.Attach();
				}
				else
				{
					clipboardBehavior.Detach();
				}
			}
		}	

		private void Attach()
		{
			if (grid != null)
			{
				grid.Copied += OnCopied;
				grid.CopyingCellClipboardContent += OnCopyingCellClipboardContent;
			}
		}

		void OnCopyingCellClipboardContent(object sender, GridViewCellClipboardEventArgs e)
		{
			if (!this.dataContext.ShouldCopySelectColumn && !(e.Cell.Column is GridViewDataColumn) && !(e.Cell.Column is GridViewExpressionColumn))
			{
				e.Cancel = true;
			}
		}

		void OnCopied(object sender, Telerik.Windows.RadRoutedEventArgs e)
		{			
			var productIDColumn = this.grid.Columns["ProductID"];
			if (!this.dataContext.ShouldCopySelectColumn && this.grid.ClipboardCopyMode.HasFlag(GridViewClipboardCopyMode.Header))
			{
				string originalText = Clipboard.GetData(DataFormats.UnicodeText).ToString();
				string updatedText = string.Empty;
				if (this.grid.SelectionMode == System.Windows.Controls.SelectionMode.Single)
				{
					updatedText = originalText.Remove(0, 1);
				}
				else
				{
					var originalColumnHeader = originalText.Split('\t').FirstOrDefault(t => t.Contains("CheckBox"));
					updatedText = originalText.Remove(0, originalColumnHeader.Length + 1);
				}
				Clipboard.SetData(DataFormats.UnicodeText, updatedText);
			}

			if (productIDColumn.IsVisible && this.grid.ClipboardCopyMode.HasFlag(GridViewClipboardCopyMode.Header))
			{
				var headerText = (productIDColumn.Header as TextBlock).Text;

				string originalText = Clipboard.GetData(DataFormats.UnicodeText).ToString();
				var originalColumnHeader = originalText.Split('\t').FirstOrDefault(t => t.Contains("TextBlock"));
				var updatedText = originalText.Replace(originalColumnHeader, headerText);
				Clipboard.SetData(DataFormats.UnicodeText, updatedText);
			}
		}

		private void Detach()
		{
			if (grid != null)
			{
				grid.Copied -= OnCopied;
				grid.CopyingCellClipboardContent -= OnCopyingCellClipboardContent;
			}
		}
	}
}
	
