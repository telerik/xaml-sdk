using System;
using System.Windows;

namespace BindingRowDetailsElementWidth
{
	public class WidthProxy : FrameworkElement
	{
		public FrameworkElement TargetElement
		{
			get;
			set;
		}

		// Using a DependencyProperty as the backing store for TargetElement.
		public static readonly DependencyProperty TargetElementProperty =
		DependencyProperty.Register("TargetElement", typeof(FrameworkElement), typeof(WidthProxy), new PropertyMetadata(OnWidthChanged));

		public double Width
		{
			get
			{
				return (double)this.GetValue(WidthProperty);
			}
			set
			{
				this.SetValue(WidthProperty, value);
			}
		}

		public static readonly DependencyProperty WidthProperty =
		DependencyProperty.Register("Width", typeof(double), typeof(WidthProxy), new PropertyMetadata(OnWidthChanged));

		private static void OnWidthChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
		{
			((WidthProxy)obj).TargetElement.Width = (double)args.NewValue - 50.0;
		}
	}
}
