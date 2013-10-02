using System;
using System.Net;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;

namespace CopyPasteFunctionality
{
	public class GridViewClipboardCustomBehavior_SL
	{
		private readonly RadGridView grid = null;
		private MyViewModel dataContext;

		public GridViewClipboardCustomBehavior_SL(RadGridView grid)
		{
			this.grid = grid;
		}

		public static readonly DependencyProperty IsEnabledProperty
			= DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(GridViewClipboardCustomBehavior_SL),
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
				var clipboardBehavior = new GridViewClipboardCustomBehavior_SL(grid);
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
			this.dataContext = grid.DataContext as MyViewModel;
			if (this.dataContext != null)
			{
				if (!this.dataContext.ShouldCopySelectColumn && !(e.Cell.Column is GridViewDataColumn) && !(e.Cell.Column is GridViewExpressionColumn))
				{
					e.Cancel = true;
				}
			}
		}

		void OnCopied(object sender, Telerik.Windows.RadRoutedEventArgs e)
		{
			var productIDColumn = this.grid.Columns["ProductID"];
			this.dataContext = grid.DataContext as MyViewModel;
			if (this.dataContext != null && !this.dataContext.ShouldCopySelectColumn && this.grid.ClipboardCopyMode.HasFlag(GridViewClipboardCopyMode.Header))
			{
				string originalText = Telerik.Windows.Controls.Clipboard.GetText();
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
				Telerik.Windows.Controls.Clipboard.SetText(updatedText);
			}
			if (productIDColumn.IsVisible && this.grid.ClipboardCopyMode.HasFlag(GridViewClipboardCopyMode.Header))
			{
				var headerText = (productIDColumn.Header as TextBlock).Text;

				string originalText = Telerik.Windows.Controls.Clipboard.GetText();
				var originalColumnHeader = originalText.Split('\t').FirstOrDefault(t => t.Contains("TextBlock"));
				var updatedText = originalText.Replace(originalColumnHeader, headerText);
				Telerik.Windows.Controls.Clipboard.SetText(updatedText);
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
