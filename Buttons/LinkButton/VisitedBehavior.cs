using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Telerik.Windows.Controls;

namespace LinkButton
{
	public static class VisitedBehavior
	{
		public static readonly DependencyProperty IsEnabledProperty =
			DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(VisitedBehavior), new PropertyMetadata(false,OnIsEnabledPropertyChanged));

		public static bool GetIsEnabled(DependencyObject obj)
		{
			return (bool)obj.GetValue(IsEnabledProperty);
		}

		public static void SetIsEnabled(DependencyObject obj, bool value)
		{
			obj.SetValue(IsEnabledProperty, value);
		}

		private static void OnIsEnabledPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			var button = sender as RadButton;
			if (button != null)
			{
				button.AddHandler(RadButton.MouseLeftButtonUpEvent,new MouseButtonEventHandler(MouseLeftButtonUp),true);
				button.Unloaded += Button_Unloaded;
			}
		}
  
		private static void MouseLeftButtonUp(object sender, MouseEventArgs e)
		{
			var button = sender as RadButton;
			if (button != null)
			{
				VisualStateManager.GoToState(button, "Visited", false);
				button.RemoveHandler(RadButton.MouseLeftButtonUpEvent, new MouseButtonEventHandler(MouseLeftButtonUp));
			}
		}

		static void Button_Unloaded(object sender, RoutedEventArgs e)
		{
			var button = sender as RadButton;
			if (button != null)
			{
				button.RemoveHandler(RadButton.MouseLeftButtonUpEvent, new MouseButtonEventHandler(MouseLeftButtonUp));
			}
		}
	}
}
