using System;
using System.Windows;
using System.Windows.Data;
using Telerik.Windows.Controls;

namespace BindingRowDetailsElementWidth
{
	public class BindingRowDetailsWidthBehavior
	{	
		private readonly RadGridView gridView = null;

		public static bool GetIsEnabled(DependencyObject obj)
		{
			return (bool)obj.GetValue(IsEnabledProperty);
		}

		public static void SetIsEnabled(DependencyObject obj, bool value)
		{
			obj.SetValue(IsEnabledProperty, value);
		}

		// Using a DependencyProperty as the backing store for IsEnabled.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty IsEnabledProperty =
			DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(BindingRowDetailsWidthBehavior), new PropertyMetadata(false, OnIsEnabledChanged));
		
		private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			RadGridView grid = d as RadGridView;

			if (grid != null)
			{
				var behavior = new BindingRowDetailsWidthBehavior(grid);
			}
		}

		public BindingRowDetailsWidthBehavior(RadGridView grid)
		{
			this.gridView = grid;
			if(this.gridView != null)
			{
				this.gridView.LoadingRowDetails+=new EventHandler<Telerik.Windows.Controls.GridView.GridViewRowDetailsEventArgs>(OnLoadingRowDetails);			
			}		
		}

		void  OnLoadingRowDetails(object sender, Telerik.Windows.Controls.GridView.GridViewRowDetailsEventArgs e)
		{
			var widthProxy = new WidthProxy();
			widthProxy.TargetElement = e.DetailsElement;
			widthProxy.SetBinding(WidthProxy.WidthProperty, new Binding("ActualWidth") { Source = sender as RadGridView });
		}
		
	}
}
