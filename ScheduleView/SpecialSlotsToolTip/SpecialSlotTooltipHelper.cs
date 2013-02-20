using System.Windows;
using System.Windows.Input;

namespace SpecialSlotsToolTip
{
	public class SpecialSlotTooltipHelper
	{
		public static bool GetForceSetIsHitTestVisible(DependencyObject obj)
		{
			return (bool)obj.GetValue(ForceSetIsHitTestVisibleProperty);
		}

		public static void SetForceSetIsHitTestVisible(DependencyObject obj, bool value)
		{
			obj.SetValue(ForceSetIsHitTestVisibleProperty, value);
		}

		// Using a DependencyProperty as the backing store for ForceSetIsHitTestVisible.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty ForceSetIsHitTestVisibleProperty =
			DependencyProperty.RegisterAttached("ForceSetIsHitTestVisible", typeof(bool), typeof(SpecialSlotTooltipHelper), new PropertyMetadata(false, OnForceSetIsHitTestVisibleChanged));

		private static void OnForceSetIsHitTestVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var fe = d as FrameworkElement;
			fe.IsHitTestVisible = (bool)e.NewValue;
		}

		public static bool GetIsAttached(DependencyObject obj)
		{
			return (bool)obj.GetValue(IsAttachedProperty);
		}

		public static void SetIsAttached(DependencyObject obj, bool? value)
		{
			obj.SetValue(IsAttachedProperty, value);
		}

		// Using a DependencyProperty as the backing store for IsAttached.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty IsAttachedProperty =
			DependencyProperty.RegisterAttached("IsAttached", typeof(bool), typeof(SpecialSlotTooltipHelper), new PropertyMetadata(false, OnIsAttachedChanged));

		private static void OnIsAttachedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if ((bool)e.NewValue)
			{
				(d as FrameworkElement).MouseLeftButtonDown += ClickTestHelper_MouseLeftButtonDown;
			}
			else
			{
				(d as FrameworkElement).MouseLeftButtonDown -= ClickTestHelper_MouseLeftButtonDown;
			}
		}

		private static void ClickTestHelper_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
		}
	}
}
